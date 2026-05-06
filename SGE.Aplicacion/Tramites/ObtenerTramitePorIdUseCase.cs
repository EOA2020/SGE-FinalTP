using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;

namespace SGE.Aplicacion.Tramites;

public class ObtenerPorIdUseCase(ITramiteRepository tramiteRepository)
{
    public ObtenerPorIdResponse Ejecutar(ObtenerPorIdResquest request)
    {
        if(request.IdTramite == Guid.Empty)
            throw new AplicacionException("El id del tramite no puede estar vacio.");

        var tramite = tramiteRepository.ObtenerTramitePorId(request.IdTramite);

        if(tramite == null)
            throw new EntidadNoEncontradaException("El id del tramite no se encntro");

        return new ObtenerPorIdResponse(
        Id: tramite.Id,
        ExpedienteId: tramite.ExpedienteId,
        Etiqueta: tramite.Etiqueta,
        Contenido: tramite.Contenido.Valor, 
        FechaCreacion: tramite.FechaCreacion,
        FechaUltimaModificacion: tramite.FechaUltimaModificacion,
        UsuarioUltimoCambio: tramite.UsuarioUltimoCambio
    );
    }



}
