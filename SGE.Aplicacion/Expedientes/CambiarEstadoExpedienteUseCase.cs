using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class CambiarEstadoExpedienteUseCase(
    IExpedienteRepository expedienteRepository,
    IAutorizacionService autorizacionService,
    ITimeProvider timeProvider,
    IUnidadDeTrabajo uow
)
{

    public CambiarEstadoExpedienteResponse Ejecutar(CambiarEstadoExpedienteRequest request, Guid idUsuario)
    {
        //verificacion de que el id del expediente sea valido
        if(request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El Guid del expediente no puede estar vacio.");

        //verificamos que el id del usuario sea valido
        if( idUsuario == Guid.Empty)
            throw new AplicacionException("El Guid del usuario no puede estar vacio.");

        //verificamos que el usuario tenga permisos
        if(!autorizacionService.PoseeElPermiso(idUsuario, Permiso.ExpedienteModificacion))
            throw new AutorizacionException("El usuario no posee los permisos");

        //buscamos el expediente y verificamos que exista
        var expediente = expedienteRepository.ObtenerPorId(request.IdExpediente);
        if(expediente == null)
            throw new EntidadNoEncontradaException("El expediente no existe.");

        //varificamos que el estado sea valido y lo cambiamos
        var estadoString = request.EstadoNuevo.Trim();
        if(!Enum.TryParse<EstadoExpediente>(estadoString, true, out var estado))
            throw new AplicacionException($"Estado inválido: {request.EstadoNuevo}");

        expediente.CambiarEstado(estado, idUsuario, timeProvider.Fecha);

        //guardamos
        uow.GuardarCambios();

        //devolvemos una respuesta
        return new CambiarEstadoExpedienteResponse(expediente.Id);
    }
}
