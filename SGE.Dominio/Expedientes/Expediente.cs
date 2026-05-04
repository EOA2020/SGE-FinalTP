using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;
namespace SGE.Dominio.Expedientes;

public class Expediente
{
    public Guid Id { get; }
    public Caratula CaractulaExp { get; private set; }
    public DateTime FechaCreacion { get; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }
    public EstadoExpediente Estado { get; private set; }

    //constructor publico que sirve solo para el momento que se crea un nuevo
    //expediente.
    public Expediente(Caratula caratula, Guid usuarioUltimoCambio)
    :this(Guid.NewGuid(), caratula, usuarioUltimoCambio, DateTime.Now,
    DateTime.Now, EstadoExpediente.RecienIniciado){}

    //constructor privado que sirve para el metedo reconstruir.
    private Expediente(Guid id, Caratula caratula, Guid usuarioUltimoCambio, DateTime fechaCreacion,
    DateTime fechaUltimaModificacion, EstadoExpediente estado)
    {
        if(id == Guid.Empty)
            throw new DominioException("El ID del producto no pueder ser un Guid vacio.");
        
        if(usuarioUltimoCambio == Guid.Empty)
            throw new DominioException("El ID del usuario no pueder ser un Guid vacio.");

        Id = id;
        UsuarioUltimoCambio = usuarioUltimoCambio;
        CaractulaExp = caratula ?? throw new DominioException("La caratula es obligatoria.");
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        Estado = estado;
    }

    //Factory Method (para la reconstrucion de un expediente).
    public static Expediente Reconstruir(Guid id, Caratula caratula, Guid usuarioUltimoCambio, DateTime fechaCreacion,
    DateTime fechaUltimaModificacion, EstadoExpediente estado)
    {
        return new Expediente(id, caratula, usuarioUltimoCambio, fechaCreacion, fechaUltimaModificacion, estado);   
    }

    //metodo que permite modificar la caratula.
    public void ModificarCaratula(Caratula nuevaCaratula, Guid idUsuario)
    {
        //comprobamos que el id del usuari que realiza la modificacion no este vacio
        if(idUsuario == Guid.Empty) 
            throw new DominioException("el ID del usuario no puede ser un Guid vacio.");

        UsuarioUltimoCambio = idUsuario;
        CaractulaExp = nuevaCaratula ?? throw new DominioException("La caratula no puede estar vacia.");
        FechaUltimaModificacion = DateTime.Now;
    }

    //metodo que devuelve un bool si el estado del expediente cambio segun la etiqueta del tramite
    public bool ActualizarEstado(EtiquetaTramite? ultimaEtiqueta, Guid idUsuario)
    {
        //verificar si el ID del usuario esta vacio
        if(idUsuario == Guid.Empty) throw new DominioException("El ID del usuario no puede ser un Guid vacio.");

        //guardamos el estado anterior para comparar con el nuevo y saber si cambio
        var estadoAnterior = Estado;

        //si el expediente se queda sin tramite el estado vuelve a recien iniciado
        //si no cambiamos segun la etiqueta del tramite
        if(ultimaEtiqueta == null)
        {
            Estado = EstadoExpediente.RecienIniciado;
        }
        else
        {
            switch (ultimaEtiqueta)
            {
                case EtiquetaTramite.Resolucion:
                    Estado = EstadoExpediente.ConResolucion; break;
                case EtiquetaTramite.PaseAEstudio:
                    Estado = EstadoExpediente.ParaResolver; break;
                case EtiquetaTramite.PaseAlArchivo:
                    Estado = EstadoExpediente.Finalizado; break;
            }
        }

        //si cambio registramos quien lo hizo
        if(estadoAnterior != Estado)
        {
            UsuarioUltimoCambio = idUsuario;
            return true;
        }

        return false;
    }

    //metodo que permite al usuario cambiar el estado (flujo natural / cambio de estado manual)
    public void CambiarEstado(EstadoExpediente nuevoEstado, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
            throw new DominioException("El ID del usuario no puede ser un Guid vacio.");
        
        UsuarioUltimoCambio = idUsuario;
        Estado = nuevoEstado;
    }
}
