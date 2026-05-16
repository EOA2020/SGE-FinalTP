using System;

namespace SGE.Aplicacion.Tramites;

public record class EliminarTramiteRequest(Guid IdTramite, Guid IdUsuario)
{

}
