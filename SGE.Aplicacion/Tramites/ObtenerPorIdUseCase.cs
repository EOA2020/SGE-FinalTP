using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;

namespace SGE.Aplicacion.Tramites;

public class ObtenerTramitePorIdUseCase(ITramiteRepository tramiteRepository)
{
    private readonly ITramiteRepository _tramiteRepository = tramiteRepository;

    public ObtenerPorIdResponse Ejecutar(ObtenerPorIdResquest request)
    {
        //verificamos que el id del tramiteno este vacio
        if(request.IdTramite == Guid.Empty)
            throw new AplicacionException("El id del tramite no puede estar vacio.");

        //obtenemos el tramite
        var tramite = _tramiteRepository.ObtenerPorId(request.IdTramite);

        //verificamos que no este vacio
        if(tramite == null)
            throw new EntidadNoEncontradaException("El id del tramite no se encontro");

        //lo retornamos
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
