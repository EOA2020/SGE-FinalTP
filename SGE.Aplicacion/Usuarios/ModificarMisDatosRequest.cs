namespace SGE.Aplicacion.Usuarios;

public record class ModificarMisDatosRequest(
    string? Nombre,
    string? Contrasena,
    string? Correo
);
