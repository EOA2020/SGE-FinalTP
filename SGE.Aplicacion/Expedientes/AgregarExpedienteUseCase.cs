using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class AgregarExpedienteUseCase(
    IExpedienteRepository expedienteRepository,
    IAutorizacionService autorizacionService,
    ITimeProvider timeProvider,
    IUnidadDeTrabajo uow
)
{
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request, Guid idUsuario)
    {   
        //verificamos que exista un id del usuario que crea el expediente
        if(idUsuario == Guid.Empty)
            throw new AplicacionException("El id del usuario no puede estar vacio");

        //verificamos que el usuario este autorizado
        if(!autorizacionService.PoseeElPermiso(idUsuario, Permiso.ExpedienteAlta))
            throw new AutorizacionException($"El usuario de id {idUsuario} no posee los permisos para agregar un expediente.");

        //creamos los values objects que se encargaran de validacion de formato/rango
        var caratula = new CaratulaVO(request.Caratula);

        //creamos una entidad que tendra su propio guid por que nace con ella
        var expediente = new Expediente(
            caratula,
            idUsuario,
            timeProvider.Fecha,
            timeProvider.Fecha
        );

        //le pasamos el expediente a el repositorio que se encargara de la persistencia
        expedienteRepository.AgregarExpediente(expediente);

        //guardamos los cambios
        uow.GuardarCambios();

        //retornamos una respuesta
        return new AgregarExpedienteResponse(expediente.Id);
    }
}
