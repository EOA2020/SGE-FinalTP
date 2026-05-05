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

    public ModificarCaratulaExpedienteUseCase(IExpedienteRepository expedienteRepository, IAutorizacionService autorizacionService)
    {
        //inyectamos la instancia
        _expedienteRepository = expedienteRepository;
        _autorizacionService = autorizacionService;
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
        var caratula = new Caratula(request.Caratula);

        //le pasamos todo al repositorio
        _expedienteRepository.ModificarCaratula(request.IdExpediente, caratula, request.IdUsuario);

        //retornamos un respuesta
        return new ModificarCaratulaExpedienteResponse(request.IdExpediente);
    }
}
