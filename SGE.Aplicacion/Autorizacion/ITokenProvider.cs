using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Autorizacion;

public interface ITokenProvider
{
    string GenerarToken(Usuario usuario);
}
