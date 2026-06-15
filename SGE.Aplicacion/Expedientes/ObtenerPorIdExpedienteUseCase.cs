using SGE.Aplicacion.Comun;
namespace SGE.Aplicacion.Expedientes;

public class ObtenerPorIdExpedienteUseCase(
    IExpedienteRepository expedienteRepository
)
{
    public ObtenerPorIdExpedienteResponse Ejecutar(ObtenerPorIdExpedienteRequest request)
    {
        //verificamos que el id del expediente que viene en el expediente sea valido
        if(request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El Guid no puede estar vacio.");

        //le pasamos nuestro id del expediente al repositorio
        var expediente = expedienteRepository.ObtenerPorId(request.IdExpediente);

        //verificamos que exista el expediente sino lanzamos una exception
        if(expediente == null)
            throw new EntidadNoEncontradaException($"El expediente de id {request.IdExpediente} no se encontro!");

        //en caso de que exista, transformamos el expediente en un ExpedienteDTO
        var expedienteDto = new ExpedienteDTO(
            expediente.Id,
            expediente.Caratula.Valor,
            expediente.FechaCreacion,
            expediente.FechaUltimaModificacion,
            expediente.UsuarioUltimoCambio,
            expediente.Estado.ToString()
        );

        //retornamos nuestro dto
        return new ObtenerPorIdExpedienteResponse(expedienteDto);
    }
}
