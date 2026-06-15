using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;

public class RegistrarUsuarioUseCase(
    IUsuarioRepository usuarioRepository,
    IUnidadDeTrabajo uow
)
{
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        CorreoElectronicoVO correo;
        try
        {
            correo = CorreoElectronicoVO.Parse(request.Correo);
        }
        catch (DominioException)
        {
            throw new AplicacionException("EL formato del correo es incorrecto");
        }
        var usuarioDB = usuarioRepository.ObtenerPorEmail(correo);
        if(usuarioDB != null)
            throw new AplicacionException("El correo ya se encuentra registrado.");

        if(string.IsNullOrWhiteSpace(request.Contrasena) || request.Contrasena.Length < 6)
            throw new AplicacionException("La contrasena minimo debe tener 6 caracteres.");

        var permisos = new List<Permiso>();

        //hasheamos la contrasena
        var contrasenaHash = HashHelper.GenerarSHA256(request.Contrasena);

        var usuario = new Usuario(
            request.Nombre,
            correo,
            contrasenaHash,
            permisos
        );

        usuarioRepository.AgregarUsuario(usuario);

        //guardamos cambios
        uow.GuardarCambios();

        return new RegistrarUsuarioResponse();
    }
}
