using System;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;

namespace SGE.Infraestructura;

public class TramiteRepository : ITramiteRepository
{
    //Agrega tramite al archivo
    public void AgregarTramite(Tramite tramite)
    {
        using var context = new DataContext();
        context.Tramites.Add(tramite);
        context.SaveChanges();     
    }
    
    //elimina tramite del archivo a partir de su id
    public void EliminarTramite(Guid idTramite)
    {
        using var context = new DataContext();
        var tramite = context.Tramites.Find(idTramite);

        if(tramite == null)
            throw new RepositorioException($"El tramite con id: {idTramite} no existe.");

        context.Tramites.Remove(tramite);
        context.SaveChanges();
    }

    public void ModificarTramite(Tramite tramite)
    {
        using var context = new DataContext();
        var tramiteDB = context.Tramites.Find(tramite.Id);

        if(tramiteDB == null)
            throw new RepositorioException($"El tramite con id: {tramite.Id} no existe.");

        context.Entry(tramiteDB).CurrentValues.SetValues(tramite);
        context.SaveChanges();
    }

    //Recorre el archivo buscando los tramites con expedienteID, los devuelve en una lista, si no encuentra deuelve lista vacia
    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        using var context = new DataContext();

        return context.Tramites
                      .Where(t => t.ExpedienteId == expedienteId)
                      .ToList();
    }

    //Busca un tramite especifico por su id y devuelve el tramite, sino devuelve null
    public Tramite? ObtenerPorId(Guid idTramite)
    {
        using var context = new DataContext();
        return context.Tramites.Find(idTramite);
    }
}
