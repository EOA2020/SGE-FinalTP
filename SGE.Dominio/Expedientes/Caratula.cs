using SGE.Dominio.Comun;
namespace SGE.Dominio.Expedientes;

//object value que es un string que valida que la caratula
//no sea null o este vacia
public record class Caratula
{
    public string Valor { get; }

    public Caratula(string valor)
    {
        if(string.IsNullOrWhiteSpace(valor))
            throw new DominioException("La caratula del expediente no pueder estar vacio.");

        Valor = valor;
    }
}
