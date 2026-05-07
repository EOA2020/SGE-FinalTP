using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Infraestructura;

/////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA AGREGAR UN EXPEDIENTE ////
////////////////////////////////////////////////////////////////////

//instanciamos el repositorio que vamos a inyectar en nuestro caso de uso
var repositorioExpediente = new ExpedienteRepository();

//instanciamos el servicio de autorizacion que vamos a inyectar
var autorizacionService = new AutorizacionService();

//creamos un usuario 
Guid idUsuario = Guid.NewGuid();
Console.WriteLine($"El id del usuario es: {idUsuario}");

//crear la instancia de el caso de uso - AgregarExpedienteUseCase e inyectamos la dependencia
var agregarExpediente = new AgregarExpedienteUseCase(repositorioExpediente, autorizacionService);

//creamos nuestra peticion para crear un archivo
var agregarExpedienteRequest = new AgregarExpedienteRequest(
    "Nuevo expediente", //caratula
    idUsuario //usuario que creo el expediente
);

//creamos la variable donde nuestro caso de uso no vas a retornar una respuesta
var agregarExpedienteResponse = agregarExpediente.Ejecutar(agregarExpedienteRequest);

//verificamos que en nuestro agregarExpedienteResponse (respuesta) este el id del expediente agregado
Console.WriteLine(agregarExpedienteResponse.IdExpediente);

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////

//clase que va a simular ser nuestro servicio de autorizacion
public class AutorizacionService: IAutorizacionService
{
    public bool PoseeElPermiso(Guid idUsuario, Permiso permiso)
    {
        return true;
    }
}