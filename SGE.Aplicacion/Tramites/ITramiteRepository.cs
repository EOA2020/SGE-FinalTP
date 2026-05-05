using System;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public interface ITramiteRepository
{}
    void Agregar(Tramite tramite);
    void Modificar(Tramite tramite);
    void Eliminar(Tramite tramite);
    Tramite? ObtenerPorId(Guid idTramite);
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId); 
    IEnumerable<Tramite> ObtenerTodos();
}
