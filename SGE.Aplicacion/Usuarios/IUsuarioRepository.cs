using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;

public interface IUsuarioRepository
{
    void AgregarUsuario(Usuario usuario);
    Usuario? ObtenerPorEmail(CorreoElectronicoVO correo);
    Usuario? ObtenerPorId(Guid id);
    IEnumerable<Usuario> ObtenerTodos();
}
