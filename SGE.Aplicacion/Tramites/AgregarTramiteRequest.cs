using System;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public record class AgregarTramiteRequest(Guid ExpedienteId, string Contenido, Guid IdUsuario)
{

}
