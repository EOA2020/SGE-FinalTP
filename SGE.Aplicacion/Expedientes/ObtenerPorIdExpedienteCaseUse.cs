using SGE.Aplicacion.Comun;
namespace SGE.Aplicacion.Expedientes;

public class ObtenerPorIdExpedienteCaseUse
{
    //creamos una variable de tipo IExpedienteRepository, que nos permitira inyectar cualquier instacia
    //de repositorio que use la interfaz IExpedienteRepository.
    private readonly IExpedienteRepository _expedienteRepository;

    public ObtenerPorIdExpedienteCaseUse(IExpedienteRepository expedienteRepository)
    {
        //inyectamos la instancia
        _expedienteRepository = expedienteRepository;
    }

    //funcion que se encarga de crear un nuevo expediente, recibe una peticion
    //con un id, que nos sirve para obtener el expediente que cumpla con este id
    public ObtenerPorIdExpedienteResponse Ejecutar(ObtenerPorIdExpedienteRequest request)
    {
        //verificamos que el id del expediente que viene en el expediente sea valido
        if(request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El Guid no puede estar vacio.");

        //le pasamos nuestro id del expediente al repositorio
        var expediente = _expedienteRepository.ObtenerPorId(request.IdExpediente);

        //verificamos que exista el expediente sino lanzamos una exception
        if(expediente == null)
            throw new EntidadNoEncontradaException($"El expediente de id {request.IdExpediente} no se encontro!");

        //en caso de que exista, transformamos el expediente en un ExpedienteDTO
        var expedienteDto = new ExpedienteDTO(
            expediente.Id,
            expediente.CaractulaExp.Valor,
            expediente.FechaCreacion,
            expediente.FechaUltimaModificacion,
            expediente.UsuarioUltimoCambio,
            expediente.Estado.ToString()
        );

        //retornamos nuestro dto
        return new ObtenerPorIdExpedienteResponse(expedienteDto);
    }
}
