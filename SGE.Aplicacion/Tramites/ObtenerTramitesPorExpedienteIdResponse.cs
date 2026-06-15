using System;

namespace SGE.Aplicacion.Tramites;

public record class ObtenerTramitesPorExpedienteIdResponse(
    IEnumerable<TramiteDTO> Tramites
);
