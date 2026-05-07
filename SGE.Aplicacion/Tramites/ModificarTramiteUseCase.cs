using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ModificarTramiteUseCase(ITramiteRepository tramiteRepository, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionExpediente)
{
    
public ModificarTramiteResponse Ejecutar(ModificarTramiteRequest request)
    {
        //verificamos que el id del usuario no este vacio
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El id no puede estar vacio");

        //verificamos que tenga permisos
        if(!autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.TramiteModificacion))
            throw new AutorizacionException("El usuario debe tener permiso");
        
        //obtenemos el tramite
        var tramite = tramiteRepository.ObtenerPorId(request.TramiteId);

        //verificamos que exista
        if (tramite == null)
            throw new EntidadNoEncontradaException("El trámite que intenta modificar no existe");

        //creamos el nuevo contenido
        var contenido = new ContenidoTramite(request.Contenido);

        //modifcamos el contenido
        tramite.ModificarContenido(contenido, request.IdUsuario);
 
        //actualizamos el tramite con sus nuevos datos
        tramiteRepository.ModificarTramite(tramite);

        //actualizamos el ultimo expediente
        actualizacionExpediente.ActualizarEstadoExpediente(request.IdUsuario,tramite.ExpedienteId);

        //retornamos una respuesta
        return new ModificarTramiteResponse();
    }


}