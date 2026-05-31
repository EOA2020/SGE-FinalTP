using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class ObtenerTodosExpedienteUseCase
{
    //creamos una variable de tipo IExpedienteRepository, que nos permitira inyectar cualquier instacia
    //de repositorio que use la interfaz IExpedienteRepository.
    private readonly IExpedienteRepository _expedienteRepository;

    public ObtenerTodosExpedienteUseCase(IExpedienteRepository expedienteRepository)
    {
        //inyectamos la instancia
        _expedienteRepository = expedienteRepository;
    }

    public ObtenerTodoExpedienteResponse Ejecutar()
    {
        //obtenemos una lista de entidades desde el repositorio
        var expedientes = _expedienteRepository.ObtenerTodos();

        //cremos la lista de dtos
        var dtos = new List<ExpedienteDTO>();

        //mapeamos manualmente
        foreach(var e in expedientes)
        {
            //transformacion de Entidad -> DTO
            var dto = new ExpedienteDTO(
                e.Id,
                e.Caratula.Valor,
                e.FechaCreacion,
                e.FechaUltimaModificacion,
                e.UsuarioUltimoCambio,
                e.Estado.ToString()
            );
            dtos.Add(dto);
        }

        //retornamos la respuesta
        return new ObtenerTodoExpedienteResponse(dtos);
    }
}
