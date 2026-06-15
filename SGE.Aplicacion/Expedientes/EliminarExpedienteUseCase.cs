using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Tramites;

namespace SGE.Aplicacion.Expedientes;

public class EliminarExpedienteUseCase(
    IExpedienteRepository expedienteRepository,
    IAutorizacionService autorizacionService,
    ITramiteRepository tramiteRepository,
    IUnidadDeTrabajo uow
)
{
    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request, Guid idUsuario)
    {
        //verificar que el id del expediente sea valido
        if(request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El Guid del expediente no puede estar vacio.");

        //verificamos que el id del usuario sea valido y tenga permisos
        if(idUsuario == Guid.Empty)
            throw new AplicacionException("El Guid del usuario no puede estar vacio.");

        if(!(autorizacionService.PoseeElPermiso(idUsuario, Permiso.ExpedienteBaja) &&
            autorizacionService.PoseeElPermiso(idUsuario, Permiso.TramiteBaja)))
            throw new AutorizacionException("el usuario no posee permisos.");

        //borramos todos los tramites del expediente
        var tramites = tramiteRepository.ObtenerPorExpedienteId(request.IdExpediente);
        foreach (var t in tramites)
        {
            tramiteRepository.EliminarTramite(t.Id);
        }

        //eliminamos el expediente
        expedienteRepository.EliminarExpediente(request.IdExpediente);

        //guardamos los cambios
        uow.GuardarCambios();

        //retornamos una respuesta
        return new EliminarExpedienteResponse(request.IdExpediente);
    }
}
