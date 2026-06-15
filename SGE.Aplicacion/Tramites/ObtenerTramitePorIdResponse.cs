using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public record class ObtenerTramitePorIdResponse(
    TramiteDTO Tramite
);