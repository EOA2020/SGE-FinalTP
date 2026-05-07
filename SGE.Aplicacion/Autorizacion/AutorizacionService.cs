namespace SGE.Aplicacion.Autorizacion;

public class AutorizacionService: IAutorizacionService
{
    public bool PoseeElPermiso(Guid idUsuario, Permiso permiso)
    {
        return true;
    }
}
