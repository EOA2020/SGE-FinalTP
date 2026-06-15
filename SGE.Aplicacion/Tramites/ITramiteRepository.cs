using SGE.Dominio.Tramites;
namespace SGE.Aplicacion.Tramites;

public interface ITramiteRepository
{
    void AgregarTramite(Tramite tramite);
    Tramite? ObtenerPorId(Guid idTramite);
    void EliminarTramite(Guid idTramite);
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId); 
}
