using SGE.Dominio.Comun;
namespace SGE.Dominio.Tramites;

public class Tramite
{
    public Guid Id { get; private set; }
    public Guid ExpedienteId { get; private set; }
    public EtiquetaTramite Etiqueta { get; private set; }
    public ContenidoVO Contenido { get; private set; } = null!;
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }

    //constructor vacio que usa EF
    protected Tramite(){}

    public Tramite(Guid expedienteId, ContenidoVO contenido,
    DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid idUsuario)
    {   
        if(idUsuario == Guid.Empty)
            throw new DominioException("El ID del usuario no pueder ser un Guid vacio.");
        
        if(expedienteId == Guid.Empty)
            throw new DominioException("El ID del expediente asociado no pueder ser un Guid vacio.");

        if(fechaCreacion > DateTime.Now)
            throw new DominioException("La fecha no puede ser mayor a la fecha actual.");

        if(fechaUltimaModificacion < fechaCreacion)
            throw new DominioException("La fecha de modificacion no puede ser menor a la fecha de creacion!"); 

        Id = Guid.NewGuid();
        ExpedienteId = expedienteId;
        UsuarioUltimoCambio = idUsuario;
        Contenido = contenido;
        Etiqueta = EtiquetaTramite.EscritoPresentado;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
    }

    //modificar el contenido del tramite
    public void ModificarContenido(ContenidoVO nuevoContenido, Guid idUsuario, DateTime fechaModificacion)
    {
        if(idUsuario == Guid.Empty)
            throw new DominioException("El ID del usuario no puede ser vacío.");

        if(fechaModificacion < FechaCreacion)
            throw new DominioException("La fecha no modificacion no puede ser menor a la fecha de creacion.");

        Contenido = nuevoContenido ?? throw new DominioException("El contenido no puede ser nulo.");
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = fechaModificacion; 
    }

}
