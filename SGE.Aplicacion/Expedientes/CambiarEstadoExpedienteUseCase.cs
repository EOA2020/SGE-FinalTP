using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public class CambiarEstadoExpedienteUseCase
{
    private readonly IExpedienteRepository _expedienteRepository;
    private readonly IAutorizacionService _autorizacionService;

    public CambiarEstadoExpedienteUseCase(
        IExpedienteRepository expedienteRepository,
        IAutorizacionService autorizacionService
    )
    {
        _expedienteRepository = expedienteRepository;
        _autorizacionService = autorizacionService;
    }

    public CambiarEstadoExpedienteResponse Ejecutar(CambiarEstadoExpedienteRequest request)
    {
        //verificacion de que el id del expediente sea valido
        if(request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El Guid del expediente no puede estar vacio.");

        //verificamos que el id del usuario sea valido
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El Guid del usuario no puede estar vacio.");

        //verificamos que el usuario tenga permisos
        if(!_autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteModificacion))
            throw new AutorizacionException("El usuario no posee los permisos");

        //buscamos el expediente y verificamos que exista
        var expediente = _expedienteRepository.ObtenerPorId(request.IdExpediente);
        if(expediente == null)
            throw new EntidadNoEncontradaException("El expediente no existe.");

        //varificamos que el estado sea valido y lo cambiamos
        var estadoString = request.EstadoNuevo.Trim();
        if(!Enum.TryParse<EstadoExpediente>(estadoString, true, out var estado))
            throw new AplicacionException($"Estado inválido: {request.EstadoNuevo}");

        expediente.CambiarEstado(estado, request.IdUsuario);

        //persistimos
        _expedienteRepository.ModificarExpediente(expediente);

        //devolvemos una respuesta
        return new CambiarEstadoExpedienteResponse(expediente.Id);
    }
}
