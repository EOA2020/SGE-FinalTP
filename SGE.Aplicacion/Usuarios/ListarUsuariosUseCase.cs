using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;

namespace SGE.Aplicacion.Usuarios;

public class ListarUsuariosUseCase(
    IUsuarioRepository usuarioRepository
)
{
    public ListarUsuariosResponse Ejecutar(ListarUsuariosRequest request, Guid idUsuario)
    {
        var usuario = usuarioRepository.ObtenerPorId(idUsuario);
        if(usuario == null)
            throw new AplicacionException($"No existe un usuario con id: {idUsuario}");
        
        if(!usuario.EsAdministrador)
            throw new AutorizacionException("El usuario no cuenta con los permisos.");

        var usuarios = usuarioRepository.ObtenerTodos();
        var usuariosDtos = new List<UsuarioDTO>();

        foreach(var u in usuarios)
        {
            var usuarioDto = new UsuarioDTO(
                u.Id,
                u.Nombre,
                u.CorreoElectronico.ToString(),
                u.EsAdministrador,
                u.Permisos
            );

            usuariosDtos.Add(usuarioDto);
        }
        
        return new ListarUsuariosResponse(usuariosDtos);
    }
}
