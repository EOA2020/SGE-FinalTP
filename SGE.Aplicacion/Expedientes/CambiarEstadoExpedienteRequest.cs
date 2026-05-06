namespace SGE.Aplicacion.Expedientes;

public record class CambiarEstadoExpedienteRequest(
    Guid IdExpediente,
    string EstadoNuevo,
    Guid IdUsuario
);
