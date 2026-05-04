using SGE.Dominio.Comun;
namespace SGE.Dominio.Tramites;

public class Tramite
{
    public Guid Id { get; }
    public Guid ExpedienteId { get; }
    public EtiquetaTramite Etiqueta { get; private set; }
    public ContenidoTramite Contenido { get; private set; }
    public DateTime FechaCreacion { get; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }

    //constructor publico que sirve solo para el momento que se crea un nuevo
    //tramite.
    public Tramite(Guid expedienteId, ContenidoTramite contenido, Guid idUsuario)
    :this(Guid.NewGuid(), expedienteId, EtiquetaTramite.EscritoPresentado, contenido, DateTime.Now, 
    DateTime.Now, idUsuario){}

    //constructor privado que sirve para el metedo reconstruir.
    private Tramite(Guid id, Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido,
    DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid idUsuario)
    {
        //lanzamos una excepcion de tipo dominio si no se cumplen cierta logica de negocio
        if(id == Guid.Empty)
            throw new DominioException("El ID del producto no pueder ser un Guid vacio.");
        
        if(idUsuario == Guid.Empty)
            throw new DominioException("El ID del usuario no pueder ser un Guid vacio.");
        
        if(expedienteId == Guid.Empty)
            throw new DominioException("El ID del expediente asociado no pueder ser un Guid vacio."); 

        Id = id;
        ExpedienteId = expedienteId;
        UsuarioUltimoCambio = idUsuario;
        Contenido = contenido ?? throw new DominioException("El contenido no puede estar vacio.");
        Etiqueta = etiqueta;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
    }
}
