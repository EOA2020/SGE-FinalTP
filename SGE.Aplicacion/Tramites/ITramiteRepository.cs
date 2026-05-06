using SGE.Dominio.Tramites;
namespace SGE.Aplicacion.Tramites;

public interface ITramiteRepository
{
    void AgregarTramite(Tramite tramite);
    void ModificarTramite(Tramite tramite);
    Tramite? ObtenerTramitePorId(Guid idTramite);
    void EliminarTramite(Guid idTramite);
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId); 
    IEnumerable<Tramite> ObtenerTodos();
}
