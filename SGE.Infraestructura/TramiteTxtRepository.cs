using System;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;

namespace SGE.Infraestructura;

public class TramiteTxtRepository : ITramiteRepository
{
    private readonly string _archivo= "tramites.txt";


    //Agrega tramite al archivo
    public void AgregarTramite(Tramite tramite)
    {
        
        using var sw= new StreamWriter(this._archivo, true);
        sw.WriteLine(tramite.Id);
        sw.WriteLine(tramite.ExpedienteId);
        sw.WriteLine(tramite.Etiqueta);
        sw.WriteLine(tramite.Contenido);
        sw.WriteLine(tramite.FechaCreacion);
        sw.WriteLine(tramite.FechaUltimaModificacion);
        sw.WriteLine(tramite.UsuarioUltimoCambio);        
    }
    
    //elimina tramite del archivo a partir de su id
    public void EliminarTramite(Guid idTramite)
    {
        //si no se encuentra archivo dispara excepcion
        if (!File.Exists(this._archivo)) throw new RepositorioException("El archivo de tramites no existe!");  

        var lineas= File.ReadAllLines(this._archivo);
        Boolean ok= false;
        //recorre de 7 campos en 7 campos. Primer campo corresponde a id
        for (int i=0; i<lineas.Length && !ok; i += 7)
        {
            //si tiene mismo id de tramite se modifica idUltimoCambio realizando borrado logico
            if (idTramite.Equals(Guid.Parse((string)lineas[i])))
            {
                //Si tramite ya fue borrado tira excepcion
                if (lineas[i+6].Equals("***")) throw new RepositorioException("El tramite no existe!"); 

                //Sino se realiza borrado logico
                lineas[i+6]="***";
                File.WriteAllLines(this._archivo, lineas);

                ok= true;;
            }
        }

        //Si no se encontro tramite arroja excepcion
        if (!ok) throw new RepositorioException("El tramite no existe!");
    }

    public void ModificarTramite(Tramite tramite)
    {
        //si no se encuentra archivo dispara excepcion
        if (!File.Exists(this._archivo)) throw new RepositorioException("El archivo de tramites no existe!");        
        
        var lineas= File.ReadAllLines(this._archivo);
        Boolean ok= false;
        //recorre de tramite en tramite (tramite= 7 campos)
        for (int i=0; i<lineas.Length; i+=7)
        {
            if (tramite.Id.Equals(Guid.Parse((string)lineas[i])) && !lineas[i+6].Equals("***"))
            {
                //Si tramite ya fue borrado tira excepcion
                if (lineas[i+6].Equals("***")) throw new RepositorioException("El tramite no existe!"); 

                //Modifica cada campo por los nuevos valores
                lineas[i]= tramite.Id.ToString();
                lineas[i+1]= tramite.ExpedienteId.ToString();
                lineas[i+2]= tramite.Etiqueta.ToString();
                lineas[i+3]= tramite.Contenido.ToString();
                lineas[i+4]= tramite.FechaCreacion.ToString();
                lineas[i+5]= tramite.FechaUltimaModificacion.ToString();
                lineas[i+6]= tramite.UsuarioUltimoCambio.ToString();

                //Guarda los cambios en el archivo
                File.WriteAllLines(this._archivo, lineas);
                ok= true;
            }
        }

        if (!ok) throw new RepositorioException ("El tramite no existe!");
    }

    //Crea Tramite a partir de una lista de datos
    private Tramite ObtenerTramite(List<object> datos)
    {
        datos[0]= Guid.Parse((string)datos[0]);
        datos[1]= Guid.Parse((string)datos[1]);
        datos[2]= Enum.Parse<EtiquetaTramite>((string)datos[2]);
        datos[3]= new ContenidoTramite ((string)datos[3]);
        datos[4]= DateTime.Parse((string)datos[4]);
        datos[5]= DateTime.Parse((string)datos[5]);
        datos[6]= Guid.Parse((string)datos[6]);

        return Tramite.Reconstruir((Guid)datos[0], (Guid)datos[1], (EtiquetaTramite)datos[2], (ContenidoTramite)datos[3], (DateTime)datos[4], (DateTime)datos[5], (Guid)datos[6]);  
    }

    //Recorre el archivo buscando los tramites con expedienteID, los devuelve en una lista, si no encuentra deuelve lista vacia
    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        var tramites= new List<Tramite> ();
        //Si archivo no existe devueve lista vacia
        if (!File.Exists(this._archivo))
        {
            return tramites;
        }
        var lineas= File.ReadAllLines(this._archivo);
        //recorre a partir de la posicion 1 entrando en campo idExpediente de cada tramite
        for (int i=0; i<lineas.Length; i += 7)
        {
            if (expedienteId.Equals(Guid.Parse((string)lineas[i + 1])) && !lineas[i+6].Equals("***"))
            {
                //lista de objetos para guardar los campos
                var datos= new List<object>();
                for (int j= i; j<=(i+6); j++)
                {
                    datos.Add(lineas[j]);
                }
                //se obtiene el Tramite y se agrega a la lista de tramites
                tramites.Add(this.ObtenerTramite(datos));
            }
        }
        return tramites;
    }

    //Busca un tramite especifico por su id y devuelve el tramite, sino devuelve null
    public Tramite? ObtenerPorId(Guid idTramite)
    {
        var lineas= File.ReadAllLines(this._archivo);
       
        //recorre a partir de la posicion entrando en campo id de cada tramite
        for (int i=0; i<lineas.Length; i += 7)
        {
            if (idTramite.Equals(Guid.Parse((string)lineas[i])) && !lineas[i+6].Equals("***"))
            {
                //lista de objetos para guardar los campos
                var datos= new List<object>();
                for (int j= i; j<=(i+6); j++)
                {
                    datos.Add(lineas[j]);
                }
                //se obtiene el Tramite y se retorna
                return this.ObtenerTramite(datos);
            }
        }
        return null;
    }
}
