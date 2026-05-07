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
        var lineas= File.ReadAllLines(this._archivo);
        Boolean ok= false;
        //recorre de 7 campos en 7 campos. Primer campo corresponde a id
        for (int i=0; i<lineas.Length && !ok; i += 7)
        {
            //si tiene mismo id de tramite se modifica idUltimoCambio realizando borrado logico
            if (idTramite.Equals(Guid.Parse((string)lineas[i])))
            {
                lineas[i+6]="***";
                File.WriteAllLines(this._archivo, lineas);

                ok= true;;
            }
        }

        if (!ok) throw new RepositorioException("El tramite no existe!");
    }

    public void ModificarTramite(Tramite tramite)
    {
        var lineas= File.ReadAllLines(this._archivo);
        Boolean ok= false;
        //recorre de tramite en tramite (tramite= 7 campos)
        for (int i=0; i<lineas.Length; i+=7)
        {
            if (tramite.Id.Equals(Guid.Parse((string)lineas[i])))
            {
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
            }
        }

        if (!ok) throw new RepositorioException ("El tramite no existe!");
    }

    private Tramite ObtenerTramite(List<object> datos)
    {
        datos[0]= Guid.Parse((string)datos[0]);
        datos[1]= Guid.Parse((string)datos[1]);
        datos[2]= Enum.Parse<EtiquetaTramite>((string)datos[2]);
    }
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
        for (int i=1; i<lineas.Length; i += 7)
        {
            if (expedienteId.Equals(Guid.Parse((string)lineas[i])))
            {
                var datos= new List<object>();
                for (int j= i-1; j<(i+5); j++)
                {
                    datos.Add(lineas[j]);
                }
                tramites.Add(this.ObtenerTramite(datos));
            }
        }


    }

    public Tramite? ObtenerPorId(Guid idTramite)
    {
        throw new NotImplementedException();
    }
}
