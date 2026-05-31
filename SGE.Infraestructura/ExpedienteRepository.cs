using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;


namespace SGE.Infraestructura;

public class ExpedienteRepository: IExpedienteRepository
{
    public void AgregarExpediente(Expediente expediente)
    {
        using var context = new DataContext();
        context.Expedientes.Add(expediente);
        context.SaveChanges();        
    }

    public void EliminarExpediente(Guid id)
    {
        using var context = new DataContext();
        var expediente = context.Expedientes.Find(id);  

        //si el expediente es null arrojamos una exception
        if(expediente == null)
            throw new RepositorioException($"No existe un expediente con id: {id}");

        context.Expedientes.Remove(expediente);
        context.SaveChanges();
    }

    public void ModificarExpediente(Expediente expediente)
    {
        using var context = new DataContext();

        var expedienteDB = context.Expedientes.Find(expediente.Id);

        //si el expediente no existe lanzamos una exception
        if(expedienteDB == null)
            throw new RepositorioException($"No existe un expediente con id: {expediente.Id}");

        context.Entry(expedienteDB).CurrentValues.SetValues(expediente);
        context.SaveChanges();
    }

    public Expediente? ObtenerPorId(Guid id)
    {
        using var context = new DataContext();
        return context.Expedientes.Find(id);       
    }

    public List<Expediente> ObtenerTodos()
    {
        using var context = new DataContext();
        return context.Expedientes.ToList();
    }    
}
