using System.Collections;
namespace SGE.Aplicacion.Usuarios;

public record class UsuarioDTO(
    Guid Id,
    string Nombre,
    string Correo,
    bool EsAdministrador,
    IEnumerable permisos
);
