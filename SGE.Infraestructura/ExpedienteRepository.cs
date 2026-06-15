using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;


namespace SGE.Infraestructura;

public class ExpedienteRepository: IExpedienteRepository
{
    public void AgregarExpediente(Expediente expediente)
    {
        using var context = new DataContext();
        context.Expedientes.Add(expediente);      
    }

    public void EliminarExpediente(Guid id)
    {
        using var context = new DataContext();
        var expediente = context.Expedientes.Find(id);  

        //si el expediente es null arrojamos una exception
        if(expediente == null)
            throw new RepositorioException($"No existe un expediente con id: {id}");

        context.Expedientes.Remove(expediente);
    }

    public Expediente? ObtenerPorId(Guid id)
    {
        using var context = new DataContext();
        return context.Expedientes.Find(id);       
    }

    public IEnumerable<Expediente> ObtenerTodos()
    {
        using var context = new DataContext();
        return context.Expedientes.ToList();
    }    
}
