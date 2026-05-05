namespace SGE.Aplicacion.Expedientes;

public record class ObtenerTodoExpedienteResponse(
    IEnumerable<ExpedienteDTO> Expedientes
);