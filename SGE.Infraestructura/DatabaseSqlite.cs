namespace SGE.Infraestructura;

public class DatabaseSqlite
{
    public static void Inicializar()
    {
        using var context = new DataContext();
        if(context.Database.EnsureCreated()) Console.WriteLine("Base de datos creada...");
    }
}
