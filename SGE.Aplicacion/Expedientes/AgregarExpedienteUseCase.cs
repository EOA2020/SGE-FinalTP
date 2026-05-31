using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class AgregarExpedienteUseCase
{
    //creamos una variable de tipo IExpedienteRepository, que nos permitira inyectar cualquier instacia
    //de repositorio que use la interfaz IExpedienteRepository.
    private readonly IExpedienteRepository _expedienteRepository;
    private readonly IAutorizacionService _autorizacionService;
    private readonly ITimeProvider _timeProvider;

    public AgregarExpedienteUseCase(
        IExpedienteRepository expedienteRepository,
        IAutorizacionService autorizacionService,
        ITimeProvider timeProvider)
    {
        //inyectamos la instancia
        _expedienteRepository = expedienteRepository;
        _autorizacionService = autorizacionService;
        _timeProvider = timeProvider;
    }

    //funcion que se encarga de crear un nuevo expediente, recibe una peticion
    //para agregar un expediente
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {   
        //verificamos que exista un id del usuario que crea el expediente
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El id del usuario no puede estar vacio");

        //verificamos que el usuario este autorizado
        if(!_autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.TramiteAlta))
            throw new AutorizacionException($"El usuario de id {request.IdUsuario} no posee los permisos para agregar un expediente.");

        //creamos los values objects que se encargaran de validacion de formato/rango
        var caratula = new CaratulaOV(request.Caratula);

        //creamos una entidad que tendra su propio guid por que nace con ella
        var expediente = new Expediente(
            caratula,
            request.IdUsuario,
            _timeProvider.Fecha,
            _timeProvider.Fecha
        );

        //le pasamos el expediente a el repositorio que se encargara de la persistencia
        _expedienteRepository.AgregarExpediente(expediente);

        //retornamos una respuesta
        return new AgregarExpedienteResponse(expediente.Id);
    }
}
