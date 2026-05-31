using SGE.Dominio.Comun;
namespace SGE.Dominio.Expedientes;

//object value que es un string que valida que la caratula
//no sea null o este vacia
public record class CaratulaOV
{
    public string Valor { get; }

    public CaratulaOV(string valor)
    {
        if(string.IsNullOrWhiteSpace(valor))
            throw new DominioException("La caratula del expediente no pueder estar vacio.");

        Valor = valor;
    }
}
