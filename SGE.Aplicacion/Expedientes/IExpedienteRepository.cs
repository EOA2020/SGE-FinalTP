using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public interface IExpedienteRepository
{
    //agregar el interfaz
    void AgregarExpediente(Expediente expediente);

    //obtener un expediente por su id
    Expediente? ObtenerPorId(Guid id);

    //obtener todos los expedientes
    List<Expediente> ObtenerTodos();

    //modificar la caratula de un expediente
    void ModificarCaratula(Guid id, Caratula caratula, Guid idUsuario);

    //modificar el estado de un expediente de forma manual 
    void ModificarEstado(Expediente expediente);

    //eliminar un expediente
    void EliminarExpediente(Guid id);
}
