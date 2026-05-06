using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Tramites;

namespace SGE.Aplicacion.Expedientes;

public class EliminarExpedienteUseCase
{
    private readonly IExpedienteRepository _expedienteRepository;
    private readonly ITramiteRepository _tramiteRepository;
    private readonly IAutorizacionService _autorizacionService;

    public EliminarExpedienteUseCase(
        IExpedienteRepository expedienteRepository,
        ITramiteRepository tramiteRepository,
        IAutorizacionService autorizacionService
    )
    {
        _expedienteRepository = expedienteRepository;
        _tramiteRepository = tramiteRepository;
        _autorizacionService = autorizacionService;
    }

    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request)
    {
        //verificar que el id del expediente sea valido
        if(request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El Guid del expediente no puede estar vacio.");

        //verificamos que el id del usuario sea valido y tenga permisos
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El Guid del usuario no puede estar vacio.");

        if(!_autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteBaja))
            throw new AutorizacionException("el usuario no posee permisos.");

        //borramos todos los tramites del expediente
        var tramites = _tramiteRepository.ObtenerPorExpedienteId(request.IdExpediente);
        foreach (var t in tramites)
        {
            _tramiteRepository.EliminarTramite(t.Id);
        }

        //eliminamos el expediente
        _expedienteRepository.EliminarExpediente(request.IdExpediente);

        //retornamos una respuesta
        return new EliminarExpedienteResponse(request.IdExpediente);
    }
}
