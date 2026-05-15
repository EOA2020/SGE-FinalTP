using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Comun;
using SGE.Infraestructura;

//instanciamos el repositorio que vamos a inyectar en nuestro caso de uso
var repositorioExpediente = new ExpedienteTxtRepository();

//instanciamos el servicio de autorizacion que vamos a inyectar
var autorizacionService = new AutorizacionService();
//creamos un usuario 
Guid idUsuario = Guid.NewGuid();

/////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA AGREGAR UN EXPEDIENTE ////
////////////////////////////////////////////////////////////////////

Console.WriteLine($"El id del usuario es: {idUsuario}");

//crear la instancia de el caso de uso - AgregarExpedienteUseCase e inyectamos la dependencia
var agregarExpediente = new AgregarExpedienteUseCase(repositorioExpediente, autorizacionService);

//creamos nuestra peticion para crear un archivo
var agregarExpedienteRequest = new AgregarExpedienteRequest(
    "Nuevo expediente", //caratula
    idUsuario //usuario que creo el expediente
);

//usamos un try catch para captar errores
try
{
    //creamos la variable donde nuestro caso de uso no vas a retornar una respuesta
    var agregarExpedienteResponse = agregarExpediente.Ejecutar(agregarExpedienteRequest);

    //verificamos que en nuestro agregarExpedienteResponse (respuesta) este el id del expediente agregado
    Console.WriteLine(agregarExpedienteResponse.IdExpediente);
}
catch (AplicacionException e)
{   
    Console.WriteLine(e.Message);
}

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA OBTENER TODOS LOS EXPEDIENTE /////
///////////////////////////////////////////////////////////////////////////

//creamos la instancia del caso de uso - ObtenerTodosExpedienteUseCase e inyectamos dependencias
var obtenerTodosExpedientes = new ObtenerTodosExpedienteUseCase(repositorioExpediente);

//creamos la variable response donde nos va a retornar todos los expedientes
var listaExpedientes = obtenerTodosExpedientes.Ejecutar();

//recorremos la lista
foreach(var e in listaExpedientes.Expedientes)
{
    Console.WriteLine($"id: {e.Id} | caratula: {e.Caratula} | Fecha de Creacion: {e.FechaCreacion} |");
    Console.WriteLine($"Fecha de Ultima Actualizacion: {e.FechaUltimaModificacion} | Ultimo usuario: {e.UsuarioUltimoCambio} | Estado: {e.Estado}");
    Console.WriteLine("///////////////////////////////////////////////////////////////////////////////////////////////////");
}

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA OBTENER UN EXPEDIENTE POR ID /////
///////////////////////////////////////////////////////////////////////////

var obtenerExpedientePorId = new ObtenerPorIdExpedienteCaseUse(repositorioExpediente);
var obtenerExpedientePorIdResponse = new ObtenerPorIdExpedienteRequest(Guid.Parse("e1fe34b9-c05f-4f54-b14d-b890d67c4acf"));

var expedienteId = obtenerExpedientePorId.Ejecutar(obtenerExpedientePorIdResponse);
Console.WriteLine($"Caratula del expediente 'e1fe34b9-c05f-4f54-b14d-b890d67c4acf' encontrado: {expedienteId.Expediente.Caratula}");

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
