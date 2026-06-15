using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class ModificarCaratulaExpedienteUseCase(
    IExpedienteRepository expedienteRepository,
    IAutorizacionService autorizacionService,
    ITimeProvider timeProvider,
    IUnidadDeTrabajo uow
)
{
    public ModificarCaratulaExpedienteResponse Ejecutar(ModificarCaratulaExpedienteRequest request, Guid idUsuario)
    {
        //verificar que el id del expediente no este vacio
        if(request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El id del expediente no puede estar vacio.");

        //verificamos que el id del usuario no este vacio
        if(idUsuario == Guid.Empty)
            throw new AplicacionException("El id del usario no puede estar vacio.");

        //verificamos que el usuario tenga los permisos necesarios para realizar la modificacion
        if(!autorizacionService.PoseeElPermiso(idUsuario, Permiso.ExpedienteModificacion))
            throw new AutorizacionException("El usuario no cuenta con los permisos para modificar un expediente");

        //pasamos a value object los datos del request
        var caratula = new CaratulaVO(request.Caratula);

        //traemos el expediente
        var expediente = expedienteRepository.ObtenerPorId(request.IdExpediente);

        //verificar que exista
        if(expediente == null)
            throw new EntidadNoEncontradaException("El expediente no existe.");

        //el dominio se encarga de modificar la caratula
        expediente.ModificarCaratula(caratula, idUsuario, timeProvider.Fecha);

        //guardamos los cambios
        uow.GuardarCambios();

        //retornamos una respuesta
        return new ModificarCaratulaExpedienteResponse(expediente.Id);
    }
}
