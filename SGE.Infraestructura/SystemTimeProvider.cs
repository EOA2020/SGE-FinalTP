using SGE.Aplicacion.Comun;

namespace SGE.Infraestructura;

public class SystemTimeProvider: ITimeProvider
{
    public DateTime Fecha => DateTime.Now;
}
