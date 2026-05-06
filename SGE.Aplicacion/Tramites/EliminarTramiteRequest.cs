using System;

namespace SGE.Aplicacion.Tramites;

public record class EliminarTramiteUseCaseRequest(Guid IdTramite, Guid IdUsuario)
{

}
