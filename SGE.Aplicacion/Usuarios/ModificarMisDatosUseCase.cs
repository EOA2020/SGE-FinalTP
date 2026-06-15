using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;

public class ModificarMisDatosUseCase(
    IUsuarioRepository usuarioRepository,
    IUnidadDeTrabajo uow
)
{
    public ModificarMisDatosResponse Ejecutar(ModificarMisDatosRequest request, Guid idUsuario)
    {
        var usuario = usuarioRepository.ObtenerPorId(idUsuario);
        if(usuario == null)
            throw new AutorizacionException("El id del usuario a modificar no es valido.");

        if(request.Correo != null)
        {
            try
            {
                var correoNuevo = CorreoElectronicoVO.Parse(request.Correo);
                usuario.ActualizarCorreo(correoNuevo);
            }
            catch (DominioException)
            {
                throw new AplicacionException("El formato del correo es incorrecto.");
            }
        }

        if(request.Nombre != null)
        {
            if(string.IsNullOrWhiteSpace(request.Nombre))
                throw new DominioException("El nombre del usuario no puede estar vacio.");
            usuario.ActualizarNombre(request.Nombre);
        }

        if(request.Contrasena != null)
        {
            var nuevaContrasena = HashHelper.GenerarSHA256(request.Contrasena);
            try
            {
                usuario.ActualizarContrasena(nuevaContrasena);
            }
            catch (DominioException)
            {
                throw new AplicacionException("La contrasena no puede estar vacia.");
            }
        }

        uow.GuardarCambios();

        return new ModificarMisDatosResponse(usuario.Id);
    }
}
