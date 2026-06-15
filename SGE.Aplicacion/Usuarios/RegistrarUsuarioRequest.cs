namespace SGE.Aplicacion.Usuarios;

public record class RegistrarUsuarioRequest(
    string Nombre,
    string Correo,
    string Contrasena
);
