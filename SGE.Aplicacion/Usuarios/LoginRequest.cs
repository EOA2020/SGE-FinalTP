namespace SGE.Aplicacion.Usuarios;

public record class LoginRequest(
    string Correo,
    string Contrasena
);
