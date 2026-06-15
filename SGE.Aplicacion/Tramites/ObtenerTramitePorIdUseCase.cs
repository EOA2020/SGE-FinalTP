using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;

namespace SGE.Aplicacion.Tramites;

public class ObtenerTramitePorIdUseCase(
    ITramiteRepository tramiteRepository
)
{
    public ObtenerTramitePorIdResponse Ejecutar(ObtenerTramitePorIdResquest request)
    {
        //verificamos que el id del tramite no este vacio
        if(request.IdTramite == Guid.Empty)
            throw new AplicacionException("El id del tramite no puede estar vacio.");

        //obtenemos el tramite
        var tramite = tramiteRepository.ObtenerPorId(request.IdTramite);

        //verificamos que no este vacio
        if(tramite == null)
            throw new EntidadNoEncontradaException("El id del tramite no se encontro");

        //lo retornamos
        
        var tramiteDto = new TramiteDTO(
            tramite.Id,
            tramite.ExpedienteId,
            tramite.Etiqueta.ToString(),
            tramite.Contenido.Valor,
            tramite.FechaCreacion,
            tramite.FechaUltimaModificacion,
            tramite.UsuarioUltimoCambio
        );

        return new ObtenerTramitePorIdResponse(tramiteDto);
    }



}
