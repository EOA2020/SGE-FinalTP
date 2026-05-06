using System;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public record class ObtenerPorIdResponse(
    Guid Id,
    Guid ExpedienteId,
    EtiquetaTramite Etiqueta, 
    string Contenido,         
    DateTime FechaCreacion,
    DateTime FechaUltimaModificacion,
    Guid UsuarioUltimoCambio
)
{

}
