using System;
using System.Data.Common;
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
            throw new Exception("El expediente no existe.");

        //traemos todos los trámites
        var todosLosTramites = _tramiteRepository.ObtenerTodos();

        DateTime fechaMasReciente = DateTime.MinValue;
        EtiquetaTramite? ultimaEtiqueta = null;

        //buscamos el último tramite del expediente
        foreach(var tramite in todosLosTramites)
        {
            if (tramite.ExpedienteId == idExpediente)
            {
                if (tramite.FechaCreacion > fechaMasReciente)
                {
                    fechaMasReciente = tramite.FechaCreacion;
                    ultimaEtiqueta = tramite.Etiqueta;
                }
            }
        }

        bool cambio = expediente.ActualizarEstado(ultimaEtiqueta, idUsuario);
        
        if (cambio) _expedienteRepository.ModificarExpediente(expediente);
    }

}
