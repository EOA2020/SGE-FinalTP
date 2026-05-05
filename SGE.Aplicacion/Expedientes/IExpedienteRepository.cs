using System;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public interface IExpedienteRepository
{
    //agregar el interfaz
    void AgregarExpediente(Expediente expediente);

    //obtener un expediente por su id
    Expediente? ObtenerPorId(Guid id);

    //obtener todos los expedientes
    IEnumerable<Expediente> ObtenerTodos();

    //modificar un expediente
    void ModificarExpediente(Expediente expediente);

    //eliminar un expediente
    void EliminarExpediente(Guid id);
}
