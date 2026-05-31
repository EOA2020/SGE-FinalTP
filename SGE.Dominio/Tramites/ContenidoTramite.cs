using SGE.Dominio.Comun;
namespace SGE.Dominio.Tramites;

//Object Value que nos permite validar que el contenido
//del tramite no este en null o vacio.
public record class ContenidoTramite
{
    public string Valor { get; private init; } = "";

    public ContenidoTramite(string valor)
    {
        //si esta vacio o null lanzamos una exception de tipo Dominio
        if(string.IsNullOrWhiteSpace(valor))
            throw new DominioException("El contenido del tramite no pueder estar vacio.");

        //si cumple la validacion, guardamos el valor
        Valor = valor;
    }
}
