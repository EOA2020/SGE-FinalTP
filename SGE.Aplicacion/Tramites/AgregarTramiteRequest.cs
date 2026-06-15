namespace SGE.Aplicacion.Tramites;

public record class AgregarTramiteRequest(
    Guid ExpedienteId,
    string Contenido
);
