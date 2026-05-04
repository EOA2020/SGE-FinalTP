using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public record class AgregarRequest(
    Caratula Caratula,
    Guid IdUsuario
);
