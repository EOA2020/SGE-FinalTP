using System;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class AgregarTramiteUseCase(ActualizacionEstadoExpedienteService _actualizacionExpediente)
{
    private readonly ITramiteRepository _tramiteRepository;

    public AgregarTramiteUseCase(ITramiteRepository tramiteRepository)
    {
        _tramiteRepository = tramiteRepository;
    }

    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request)
    {
        if(!IAutorizacionService.PoseeElPermiso(request.idUsuario)
            throw new Exception("El usuario debe tener permiso");
        
        
        
    }

}
