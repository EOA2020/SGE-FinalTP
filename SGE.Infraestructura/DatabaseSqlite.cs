namespace SGE.Infraestructura;

public class DatabaseSqlite
{
    public static void Inicializar()
    {
        try
        {
            using var context = new DataContext();

            Console.WriteLine("Creando BD...");

            var creada = context.Database.EnsureCreated();

            Console.WriteLine($"Creada: {creada}");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
