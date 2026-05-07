using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;


namespace SGE.Infraestructura;

public class ExpedienteRepository: IExpedienteRepository
{

    private readonly string _archivo= "Expediente.txt";

    public void AgregarExpediente(Expediente expediente)
    {
        //Creacion de objeto para recorrer archivo. Variable de append, si true agrega al final, sino sobreescribe
        using var sw= new StreamWriter(this._archivo, true);
        
        //guardamos cada dato en una linea nueva
        sw.WriteLine(expediente.Id);
        sw.WriteLine(expediente.CaractulaExp.Valor);
        sw.WriteLine(expediente.UsuarioUltimoCambio);
        sw.WriteLine(expediente.FechaCreacion);
        sw.WriteLine(expediente.FechaUltimaModificacion);        
        sw.WriteLine(expediente.Estado);        
    }

    public void EliminarExpediente(Guid id)
    {
        //Creacion de objeto que contiene todas las lineas del archivo, para recorrerlo
        var lineas= File.ReadAllLines(this._archivo);
        
        Boolean ok=false;
        //Se recorre el archivo revisando el campo id y luego incrementando al proximo registro
        for (int i=0; i<lineas.Length && !ok; i += 6)
        {
            //Si id de registro igual a id buscado se realiza borrado logico
            if (id.Equals(Guid.Parse(lineas[i])))
            {
                //se sobreescribe campo UsuarioUltimoCambio
                lineas[i+2]= "***";
                ok= true;
            }
        }

        //si no esta lanzo excepcion
        if (!ok) throw new RepositorioException("El expediente no existe!");      
    }

    public void ModificarExpediente(Expediente expediente)
    {
        var lineas= File.ReadAllLines(this._archivo);
        Boolean ok= false;
        //si no se alcanza el final y no se encontro
        for (int i=0; i<lineas.Length & !ok; i += 6)
        {
            //si se encontro modifico los campos del archivo
            if (expediente.Id.Equals(Guid.Parse(lineas[i])))
            {
                lineas[i]= expediente.Id.ToString() ;
                lineas[i++]= expediente.CaractulaExp.Valor;
                lineas[i++]= expediente.UsuarioUltimoCambio.ToString();
                lineas[i++]= expediente.FechaCreacion.ToString();
                lineas[i++]= expediente.FechaUltimaModificacion.ToString();                
                lineas[i]= expediente.Estado.ToString();         

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
            if (id.Equals(Guid.Parse(lineas[i])))
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
        int i=0;
        //convierto datos en lista a su tipo
        datos[i]= Guid.Parse((string)datos[i++]);
        datos[i]= new Caratula((string)datos[i++]);
        datos[i]= Guid.Parse((string)datos[i++]);
        datos[i]=  DateTime.Parse((string)datos[i++]);
        datos[i]= DateTime.Parse((string)datos[i++]);
        datos[i]= (EstadoExpediente)Enum.Parse(typeof (EstadoExpediente), (string)datos[i]);

        //devuelvo expediente reconstruido
        return Expediente.Reconstruir((Guid)datos[0], (Caratula)datos[1], (Guid)datos[2],(DateTime)datos[3],(DateTime)datos[4],(EstadoExpediente)datos[5]);
    }
    public List<Expediente> ObtenerTodos()
    {
        List<Expediente> resultado= new List<Expediente>();

        //si archivo no existe devuelve lista vacia
        if (!File.Exists(this._archivo))
        {
            return resultado;
        }
        using var sr = new StreamReader(this._archivo);
        while (!sr.EndOfStream)
        {
           /*
            //Se van leyendo las lineas con datos y se guarda c/u en un var
            var  idStr= sr.ReadLine() ?? "";
            var caratulaStr= sr.ReadLine() ?? "";
            var usuarioUltimoCambioStr= sr.ReadLine() ?? "";
            var fechaCreacionStr= sr.ReadLine() ?? "2000-01-01";
            var fechaUltimaModificacionStr= sr.ReadLine() ?? "2000-01-01";            
            var estadoStr = sr.ReadLine() ?? "";

            //Se convierte
            /*
            var id= Guid.Parse(idStr);
            var caratula= new Caratula(caratulaStr);
            var UsuarioUltimoCambio= Guid.Parse(usuarioUltimoCambioStr);
            var fechaCreacion=  DateTime.Parse(fechaCreacionStr);
            var fechaUltimaModificacion= DateTime.Parse(fechaUltimaModificacionStr);          
            var estado= (EstadoExpediente)Enum.Parse(typeof (EstadoExpediente), estadoStr);
            */

            
            /*
            List<object> datos= new List<object>();
            for (int i=0; i < 6; i++)
            {
                datos.Add()
            }

            //armamos el Expediente
           // var expediente= Expediente.Reconstruir(id, caratula, UsuarioUltimoCambio, fechaCreacion, fechaUltimaModificacion, estado);
            //agregamos expediente a la lista
            resultado.Add(expediente);*/
        }
        //Devolvemos la lista de expedientes
        return resultado;

        
    }

}
