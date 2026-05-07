using System;

namespace SGE.Aplicacion.Tramites;

public record class ObtenerPorExpedienteIdResponse(IEnumerable<TramiteDTO> Tramites)
{

}
