using SGE.Dominio.Tramites;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;


namespace SGE.Aplicacion.Tramites;

public class AgregarTramiteUseCase(
    IExpedienteRepository expedienteRepository,
    ITramiteRepository tramiteRepository, 
    IAutorizacionService autorizacionService, 
    ActualizacionEstadoExpedienteService actualizacionExpediente,
    ITimeProvider timeProvider,
    IUnidadDeTrabajo uow
)
{
   
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request, Guid idUsuario)
    {
        //se verifica que el id del usuario no este vacio
        if(idUsuario == Guid.Empty)
            throw new AplicacionException("El id no puede estar vacio");

        //se verifica que el usuario tenga permiso
        if(!autorizacionService.PoseeElPermiso(idUsuario, Permiso.TramiteAlta))
            throw new AutorizacionException("El usuario debe tener permiso");

        //verificamos que exista el expediente
        if(expedienteRepository.ObtenerPorId(request.ExpedienteId) == null)
            throw new EntidadNoEncontradaException("El id del expediente debe ser valido para crear un tramite");
            
        //creamos el nuevo contenido
        var contenido = new ContenidoVO(request.Contenido); 
        
        //creamos el nuevo tramite
        var tramite = new Tramite(
            request.ExpedienteId,
            contenido, 
            timeProvider.Fecha, 
            timeProvider.Fecha,
            idUsuario
        );

        //lo agregamos
        tramiteRepository.AgregarTramite(tramite);

        //actualizamos el ultimo expediente
        actualizacionExpediente.ActualizarEstadoExpediente(tramite.UsuarioUltimoCambio, tramite.ExpedienteId);

        //guardamos los cambios
        uow.GuardarCambios();

        //retornamos una respuesta
        return new AgregarTramiteResponse(tramite.Id);   
    }

}
