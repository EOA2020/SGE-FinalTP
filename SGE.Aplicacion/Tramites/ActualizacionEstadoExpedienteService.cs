using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ActualizacionEstadoExpedienteService(ITramiteRepository _tramiteRepository, IExpedienteRepository _expedienteRepository)
{
    public void ActualizarEstadoExpediente(Guid idUsuario, Guid idExpediente)
    {
        //obtenemos el expediente
        var expediente = _expedienteRepository.ObtenerPorId(idExpediente);

        //verificamos que exista
        if (expediente == null) 
            throw new EntidadNoEncontradaException("El expediente no existe.");

        //obtenemos sus tramites
        var tramites = _tramiteRepository.ObtenerPorExpedienteId(idExpediente);

        //definimmos una variable con una fecha minima
        DateTime fechaMasReciente = DateTime.MinValue;

        //definimos 
        EtiquetaTramite? ultimaEtiqueta = null;

        //buscamos el último tramite del expediente y extraemos su etiqueta
        foreach(var tramite in tramites)
        {
            if (tramite.FechaCreacion > fechaMasReciente)
            {
                fechaMasReciente = tramite.FechaCreacion;
                ultimaEtiqueta = tramite.Etiqueta;
             }
            
        }

        //evaluamos si su estado cambio
        bool cambio = expediente.ActualizarEstado(ultimaEtiqueta, idUsuario);
        
        //si cambio, modifcamos el expediente
        if (cambio) _expedienteRepository.ModificarExpediente(expediente);
    }

}
