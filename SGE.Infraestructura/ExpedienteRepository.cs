using System;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;


namespace SGE.Infraestructura;

public class ExpedienteRepository: IExpedienteRepository
{

    private readonly string _archivo= "Expediente.txt";

    void AgregarExpediente(Expediente expediente)
    {
        using var sw= new StreamWriter(this._archivo, true);
        sw.WriteLine(expediente.Id);
        sw.WriteLine(expediente.CaractulaExp.Valor);
        sw.WriteLine(expediente.FechaCreacion);
        sw.WriteLine(expediente.FechaUltimaModificacion);
        sw.WriteLine(expediente.UsuarioUltimoCambio);
        sw.WriteLine(expediente.Estado);        
    }

    public void EliminarExpediente(Guid id)
    {
        throw new NotImplementedException();
    }

    public void ModificarExpediente(Expediente expediente)
    {
        throw new NotImplementedException();
    }

    public Expediente? ObtenerPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Expediente> ObtenerTodos()
    {
        List<Expediente> resultado= new List<Expediente>();
        if (!File.Exists(this._archivo))
        {
            return resultado;
        }
        using var sr = new StreamReader(this._archivo);
        
        while (!sr.EndOfStream)
        {
            var  idStr= sr.ReadLine() ?? "";
            var caratulaStr= sr.ReadLine() ?? "";
            var fechaCreacionStr= sr.ReadLine() ?? "2000-01-01";
            var fechaUltimaModificacionStr= sr.ReadLine() ?? "2000-01-01";
            var usuarioUltimoCambioStr= sr.ReadLine() ?? "";
            var estadoStr = sr.ReadLine() ?? "";

            var id= Guid.Parse(idStr);
            var caratula= new Caratula(caratulaStr);
            var UsuarioUltimoCambio= Guid.Parse(usuarioUltimoCambioStr);
            var fechaCreacion=  DateTime.Parse(fechaCreacionStr);
            var fechaUltimaModificacion= DateTime.Parse(fechaUltimaModificacionStr);
            
            var estado= (EstadoExpediente)Enum.Parse(typeof (EstadoExpediente), estadoStr);

            var expediente= Expediente.Reconstruir(id, caratula, UsuarioUltimoCambio, fechaCreacion, fechaUltimaModificacion, estado);

            resultado.Add(expediente);
        }

        return resultado;

        
    }


}
