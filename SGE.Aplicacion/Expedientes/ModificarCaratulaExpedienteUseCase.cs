using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class ModificarCaratulaExpedienteUseCase
{
    //creamos una variable de tipo IExpedienteRepository, que nos permitira inyectar cualquier instacia
    //de repositorio que use la interfaz IExpedienteRepository.
    private readonly IExpedienteRepository _expedienteRepository;
    private readonly IAutorizacionService _autorizacionService;
    private readonly ITimeProvider _timeProvider;

    public ModificarCaratulaExpedienteUseCase(
        IExpedienteRepository expedienteRepository,
        IAutorizacionService autorizacionService,
        ITimeProvider timeProvider)
    {
        //inyectamos la instancia
        _expedienteRepository = expedienteRepository;
        _autorizacionService = autorizacionService;
        _timeProvider = timeProvider;
    }

    public ModificarCaratulaExpedienteResponse Ejecutar(ModificarCaratulaExpedienteRequest request)
    {
        //verificar que el id del expediente no este vacio
        if(request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El id del expediente no puede estar vacio.");

        //verificamos que el id del usuario no este vacio
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El id del usario no puede estar vacio.");

        //verificamos que el usuario tenga los permisos necesarios para realizar la modificacion
        if(!_autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteModificacion))
            throw new AutorizacionException("El usuario no cuenta con los permisos para modificar un expediente");

        //pasamos a value object los datos del request
        var caratula = new CaratulaOV(request.Caratula);

        //traemos el expediente
        var expediente = _expedienteRepository.ObtenerPorId(request.IdExpediente);

        //verificar que exista
        if(expediente == null)
            throw new EntidadNoEncontradaException("El expediente no existe.");

        //el dominio se encarga de modificar la caratula
        expediente.ModificarCaratula(caratula, request.IdUsuario, _timeProvider.Fecha);

        //persistir
        _expedienteRepository.ModificarExpediente(expediente);

        return new ModificarCaratulaExpedienteResponse(expediente.Id);
    }
}
