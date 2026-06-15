using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;

public class LoginUseCase(
    IUsuarioRepository usuarioRepository,
    ITokenProvider tokenProvider
)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {
        CorreoElectronicoVO correoIngresado;
        try
        {
            correoIngresado = CorreoElectronicoVO.Parse(request.Correo);
        }
        catch (DominioException)
        {
            throw new AutorizacionException("El formato del correo o la contrasena son incorrectos.");
        }

        var usuario = usuarioRepository.ObtenerPorEmail(correoIngresado);
        if(usuario == null)
            throw new AutorizacionException("El correo es incorrecto");
        
        var contrasenaIngresada = HashHelper.GenerarSHA256(request.Contrasena);
        if(usuario.ContrasenaHash != contrasenaIngresada)
            throw new AutorizacionException("La contrasena ingresada es incorrecta.");

        var token = tokenProvider.GenerarToken(usuario);

        return new LoginResponse(token);
    }
}
