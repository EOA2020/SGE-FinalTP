using System;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Autorizacion;


namespace SGE.Aplicacion.Tramites;

public class AgregarTramiteUseCase
{
    private readonly ITramiteRepository _tramiteRepository;
    private readonly IAutorizacionService _autorizacionService;
    private readonly ActualizacionEstadoExpedienteService _actualizacionExpediente;

    public AgregarTramiteUseCase(ITramiteRepository tramiteRepository,  IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionExpediente)
    {
        _tramiteRepository = tramiteRepository;
        _autorizacionService = autorizacionService;
        _actualizacionExpediente = actualizacionExpediente;
    }

    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request)
    {
        if(request.IdUsuario == Guid.Empty)
            throw new Exception("El id no puede estar vacio");

        if(!_autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.TramiteAlta))
            throw new Exception("El usuario debe tener permiso");

        var contenido = new ContenidoTramite(request.Contenido); 
        
        var tramite = new Tramite(request.ExpedienteId, contenido, request.IdUsuario);

        _tramiteRepository.AgregarTramite(tramite);

        _actualizacionExpediente.ActualizarEstadoExpediente(tramite.UsuarioUltimoCambio,tramite.ExpedienteId);

        return new AgregarTramiteResponse(tramite.Id);   
    }

}
