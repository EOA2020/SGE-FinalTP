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
    ITimeProvider timeProvider)
{
   
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request)
    {
        //se verifica que el id del usuario no este vacio
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El id no puede estar vacio");

        //se verifica que el usuario tenga permiso
        if(!autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.TramiteAlta))
            throw new AutorizacionException("El usuario debe tener permiso");

        //verificamos que exista el expediente
        if(expedienteRepository.ObtenerPorId(request.ExpedienteId) == null)
            throw new EntidadNoEncontradaException("El id del expediente debe ser valido para crear un tramite");
            
        //creamos el nuevo contenido
        var contenido = new ContenidoTramite(request.Contenido); 
        
        //creamos el nuevo tramite
        var tramite = new Tramite(
            request.ExpedienteId,
            contenido, request.IdUsuario, 
            timeProvider.Fecha, 
            timeProvider.Fecha
        );

        //lo agregamos
        tramiteRepository.AgregarTramite(tramite);

        //actualizamos el ultimo expediente
        actualizacionExpediente.ActualizarEstadoExpediente(tramite.UsuarioUltimoCambio, tramite.ExpedienteId);

        //retornamos una respuesta
        return new AgregarTramiteResponse(tramite.Id);   
    }

}
