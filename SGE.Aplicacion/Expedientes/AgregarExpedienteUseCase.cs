using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class AgregarExpedienteUseCase
{
    private readonly IExpedienteRepository _expedienteRepository;

    public AgregarExpedienteUseCase(IExpedienteRepository expedienteRepository)
    {
        _expedienteRepository = expedienteRepository;
    }

    public AgregarRequest Ejecutar(AgregarRequest request)
    {   
        if(request.IdUsuario == Guid.Empty)
            throw new Exception("El id no puede estar vacio");

        if(!PoseePermiso(request.IdUsuario))
            throw new Exception("El id no puede estar vacio");

        var expediente = new Expediente(
            request.Caratula,
            request.IdUsuario
        );

        _expedienteRepository.AgregarExpediente(expediente);

        return new AgregarResponse(expediente.Id);
    }
}
