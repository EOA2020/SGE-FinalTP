using System;

namespace SGE.Aplicacion.Tramites;

public record class ModificarTramiteRequest(
    Guid TramiteId, 
    string Contenido
);

