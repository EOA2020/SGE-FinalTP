using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ActualizacionEstadoExpedienteService(ITramiteRepository _tramiteRepository, IExpedienteRepository _expedienteRepository)
{
    public void ActualizarEstadoExpediente(Guid idUsuario, Guid idExpediente)
    {
        //traemos el expediente real desde el repositorio
        var expediente = _expedienteRepository.ObtenerPorId(idExpediente);

        if (expediente == null) 
            throw new EntidadNoEncontradaException("El expediente no existe.");

        //traemos solo los tramites del expediente
        var tramitesDelExpediente = _tramiteRepository.ObtenerPorExpedienteId(idExpediente);

        DateTime fechaMasReciente = DateTime.MinValue;
        EtiquetaTramite? ultimaEtiqueta = null;

        //buscamos el último tramite del expediente
        foreach(var tramite in tramitesDelExpediente)
        {
            if (tramite.FechaCreacion > fechaMasReciente)
            {
                fechaMasReciente = tramite.FechaCreacion;
                ultimaEtiqueta = tramite.Etiqueta;
             }
            
        }

        bool cambio = expediente.ActualizarEstado(ultimaEtiqueta, idUsuario);
        
        if (cambio) _expedienteRepository.ModificarExpediente(expediente);
    }

}
