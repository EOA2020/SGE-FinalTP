using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class ObtenerTodosExpedienteUseCase(
    IExpedienteRepository expedienteRepository
)
{
    public ObtenerTodosExpedienteResponse Ejecutar(ObtenerTodosExpedienteRequest request)
    {
        //obtenemos una lista de entidades desde el repositorio
        var expedientes = expedienteRepository.ObtenerTodos();

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
        return new ObtenerTodosExpedienteResponse(dtos);
    }
}
