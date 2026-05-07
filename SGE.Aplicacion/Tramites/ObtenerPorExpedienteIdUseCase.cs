using System;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ObtenerPorExpedienteIdUseCase(ITramiteRepository tramiteRepository, IExpedienteRepository expedienteRepository)
{
    public ObtenerPorExpedienteIdResponse Ejecutar(ObtenerPorExpedienteIdRequest request)
    {
        //verificamos que el expediente no este vacio
        if (request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El id del expediente no puede estar vacio");

        //obtenemos el expediente
        var expediente = expedienteRepository.ObtenerPorId(request.IdExpediente);

        //verificamos que exista
        if(expediente == null)
            throw new EntidadNoEncontradaException("El expediente no existe");

        //obtenemos la lista de sus tramites
        var tramites = tramiteRepository.ObtenerPorExpedienteId(request.IdExpediente);

        //creamos la lista que se retornara protegiendo nuestra clase Tramite
        var tramitesDTO = new List<TramiteDTO>();

        //recorremos los elementos para agregarlos a nuestra lista
        foreach (var tramite in tramites)
        {
            var dto = new TramiteDTO(
                Id: tramite.Id,
                ExpedienteId: tramite.ExpedienteId,
                Etiqueta: tramite.Etiqueta.ToString(), 
                Contenido: tramite.Contenido.Valor,    
                FechaCreacion: tramite.FechaCreacion,
                FechaUltimaModificacion: tramite.FechaUltimaModificacion,
                UsuarioUltimoCambio: tramite.UsuarioUltimoCambio
        );
        tramitesDTO.Add(dto);
    }
       //retornamos la lista
        return new ObtenerPorExpedienteIdResponse(tramitesDTO);
    }
}
