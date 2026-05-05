namespace SGE.Aplicacion.Comun;

public class AplicacionException: Exception
{
    public AplicacionException(){}

    public AplicacionException(string? message): base(message){}

    public AplicacionException(string? message, Exception? innerException): base(message, innerException){}
}
