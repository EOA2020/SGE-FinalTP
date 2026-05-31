using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public record class AgregarRequest(
    CaratulaOV Caratula,
    Guid IdUsuario
);
