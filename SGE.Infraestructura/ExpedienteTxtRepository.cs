using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;


namespace SGE.Infraestructura;

public class ExpedienteTxtRepository: IExpedienteRepository
{

    private readonly string _archivo= "Expediente.txt";

    public void AgregarExpediente(Expediente expediente)
    {
        //si no se encuentra archivo dispara excepcion
        if (!File.Exists(this._archivo)) throw new RepositorioException("El archivo de expedientes no existe!");

        //Creacion de objeto para recorrer archivo. Variable de append, si true agrega al final, sino sobreescribe
        using var sw= new StreamWriter(this._archivo, true);
        
        //guardamos cada dato en una linea nueva
        sw.WriteLine(expediente.Id);
        sw.WriteLine(expediente.Caratula.Valor);
        sw.WriteLine(expediente.UsuarioUltimoCambio);
        sw.WriteLine(expediente.FechaCreacion);
        sw.WriteLine(expediente.FechaUltimaModificacion);        
        sw.WriteLine(expediente.Estado);        
    }

    public void EliminarExpediente(Guid id)
    {
        //si no se encuentra archivo dispara excepcion
        if (!File.Exists(this._archivo)) throw new RepositorioException("El archivo de expedientes no existe!");

        //Creacion de objeto que contiene todas las lineas del archivo, para recorrerlo
        var lineas= File.ReadAllLines(this._archivo);
        
        Boolean ok=false;
        //Se recorre el archivo revisando el campo id y luego incrementando al proximo registro
        for (int i=0; i<lineas.Length && !ok; i += 6)
        {
            //Si id de registro igual a id buscado se realiza borrado logico
            if (id.Equals(Guid.Parse(lineas[i])))
            {
               //si registro ya fue borrado de forma logica arroja excepcion
               if (lineas[i + 2].Equals("***")) throw new RepositorioException ($"El expediente con id {id} no existe");
                
                //se sobreescribe campo UsuarioUltimoCambio
                lineas[i+2]= "***";
                File.WriteAllLines(this._archivo, lineas);
                ok= true;                
            }
        }

        //si no esta lanza excepcion
        if (!ok) throw new RepositorioException("El expediente no existe!");      
    }

    public void ModificarExpediente(Expediente expediente)
    {
        //si no se encuentra archivo dispara excepcion
        if (!File.Exists(this._archivo)) throw new RepositorioException("El archivo de expedientes no existe!");

        var lineas= File.ReadAllLines(this._archivo);
        Boolean ok= false;
        //si no se alcanza el final y no se encontro
        for (int i=0; i<lineas.Length && !ok; i += 6)
        {
            //si se encontro modifico los campos
            if (expediente.Id.Equals(Guid.Parse(lineas[i])))
            {
                //Si expediente fue borrado lanza excepcion
                if (lineas[i + 2].Equals("***")) throw new RepositorioException ($"El expediente con id {expediente.Id} no existe");

                lineas[i]= expediente.Id.ToString() ;
                lineas[i+1]= expediente.Caratula.Valor;
                lineas[i+2]= expediente.UsuarioUltimoCambio.ToString();
                lineas[i+3]= expediente.FechaCreacion.ToString();
                lineas[i+4]= expediente.FechaUltimaModificacion.ToString();                
                lineas[i+5]= expediente.Estado.ToString();     

                //Modifico el archivo con los cambios realizados
                File.WriteAllLines(this._archivo, lineas);
                ok=true;       
            }
        }
        
        //si no esta lanzo excepcion (devuelvo)
        if (!ok) throw new RepositorioException("El expediente no existe!");
    
    }

    public Expediente? ObtenerPorId(Guid id)
    {
        var lineas= File.ReadAllLines(this._archivo);

        for (int i=0; i<lineas.Length; i += 6)
        {
            if (id.Equals(Guid.Parse(lineas[i])) && !lineas[i+2].Equals("***"))
            {
                //guardo contenido de campos en lista de objetos
                List<object> datos= new List<object> ();
                for (int j=0; j<6; j++)
                {
                    datos.Add(lineas[i+j]);
                }           

                return this.ObtenerExpediente(datos);
            }
        }        
        return null;
    }

    private Expediente ObtenerExpediente(List<object> datos)
    {
        //convierto datos en lista a su tipo
        datos[0]= Guid.Parse((string)datos[0] ?? "");                                                   //id
        datos[1]= new CaratulaOV((string)datos[1] ?? "");                                                 //caratula
        datos[2]= Guid.Parse((string)datos[2] ?? "");                                                   //usuarioUltimosCambios
        datos[3]=  DateTime.Parse((string)datos[3] ?? "2000-01-01");                                    //fechaCreacion
        datos[4]= DateTime.Parse((string)datos[4] ?? "2000-01-01");                                     //fechaUltimaModificacion
        datos[5]= Enum.Parse<EstadoExpediente>((string)datos[5] ?? "");                                 //estado

        //devuelvo expediente reconstruido
        return Expediente.Reconstruir((Guid)datos[0], (CaratulaOV)datos[1], (Guid)datos[2],(DateTime)datos[3],(DateTime)datos[4],(EstadoExpediente)datos[5]);
    }

    public List<Expediente> ObtenerTodos()
    {
        List<Expediente> resultado= new List<Expediente>();

        //si archivo no existe devuelve lista vacia
        if (!File.Exists(this._archivo))
        {
            return resultado;
        }
        
        var lineas=  File.ReadAllLines(this._archivo);
        for (int i=0; i< lineas.Length; i+=6)
        {
            //Si no fue borrado, se obtiene el registro
            if (!lineas[i + 2].Equals("***"))
            {
                List<object> datos= new List<object> ();
                for (int j=0; j<6; j++)
                {
                    datos.Add(lineas[i+j]);
                } 

                var expediente= this.ObtenerExpediente(datos);
            
                //agregamos expediente a la lista
                resultado.Add(expediente);                
            }
        }
        //Devolvemos la lista de expedientes
        return resultado; 
    }    
}
