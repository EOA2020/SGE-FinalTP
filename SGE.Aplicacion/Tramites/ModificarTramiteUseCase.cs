using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ModificarTramiteUseCase(
    ITramiteRepository tramiteRepository, 
    IAutorizacionService autorizacionService, 
    ActualizacionEstadoExpedienteService actualizacionExpediente,
    ITimeProvider timeProvider,
    IUnidadDeTrabajo uow
)
{
    
public ModificarTramiteResponse Ejecutar(ModificarTramiteRequest request, Guid idUsuario)
    {
        //verificamos que el id del usuario no este vacio
        if(idUsuario == Guid.Empty)
            throw new AplicacionException("El id no puede estar vacio");

        //verificamos que tenga permisos
        if(!autorizacionService.PoseeElPermiso(idUsuario, Permiso.TramiteModificacion))
            throw new AutorizacionException("El usuario debe tener permiso");
        
        //obtenemos el tramite
        var tramite = tramiteRepository.ObtenerPorId(request.TramiteId);

        //verificamos que exista
        if (tramite == null)
            throw new EntidadNoEncontradaException("El trámite que intenta modificar no existe");

        //creamos el nuevo contenido
        var contenido = new ContenidoVO(request.Contenido);

        //modifcamos el contenido
        tramite.ModificarContenido(contenido, idUsuario, timeProvider.Fecha);

        //actualizamos el ultimo expediente
        actualizacionExpediente.ActualizarEstadoExpediente(
            idUsuario,
            tramite.ExpedienteId
        );

        //guardamos cambios
        uow.GuardarCambios();

        //retornamos una respuesta
        return new ModificarTramiteResponse(request.TramiteId);
    }
}