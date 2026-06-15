using System.Collections;
using SGE.Dominio.Comun;

namespace SGE.Dominio.Usuarios;

public class Usuario
{
    public Guid Id { get; private set; } 
    public string Nombre { get; private set; } = "";
    public CorreoElectronicoVO CorreoElectronico { get; private set; } = null!;
    public string ContrasenaHash { get; private set; } = "";
    public bool EsAdministrador { get; private set; }
    public IEnumerable Permisos { get; private set; } = null!;

    protected Usuario(){} //constructor vacio que usara EF

    public Usuario(string nombre, CorreoElectronicoVO correo, string contrasenaHash,
    IEnumerable permisos)
    {
        if(string.IsNullOrWhiteSpace(nombre))
            throw new DominioException("El nombre del usuario no puede estar vacio.");

        if(string.IsNullOrWhiteSpace(contrasenaHash))
            throw new DominioException("La contrasena no puede estar vacia.");

        Id = Guid.NewGuid();
        Nombre = nombre;
        CorreoElectronico = correo;
        ContrasenaHash = contrasenaHash;
        EsAdministrador = false;
        Permisos = permisos;    
    }

    public void ActualizarNombre(string nombre)
    {
        if(string.IsNullOrWhiteSpace(nombre))
            throw new DominioException("El nombre del usuario no puede estar vacio.");

        Nombre = nombre;
    }

    public void ActualizarContrasena(string contrasena)
    {
        if(string.IsNullOrWhiteSpace(contrasena))
            throw new DominioException("La contrasena no puede estar vacia.");

        ContrasenaHash = contrasena;
    }

    public void ActualizarPermisos(IEnumerable permisos)
        => Permisos = permisos;

    public void ActualizarCorreo(CorreoElectronicoVO correo)
        => CorreoElectronico = correo;

    public void ActualizarAdministrador(bool esAdministrador)
        => EsAdministrador = esAdministrador;

}
