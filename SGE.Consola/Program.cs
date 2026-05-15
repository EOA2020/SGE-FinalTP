using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Comun;
using SGE.Infraestructura;
using SGE.Dominio.Comun;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Comun;

//instanciamos el repositorio que vamos a inyectar en nuestro caso de uso
var repositorioExpediente = new ExpedienteTxtRepository();

//instanciamos el servicio de autorizacion que vamos a inyectar
var autorizacionService = new AutorizacionService();
//creamos un usuario 
Guid idUsuario = Guid.NewGuid();

/////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA AGREGAR UN EXPEDIENTE ////
////////////////////////////////////////////////////////////////////

//crear la instancia de el caso de uso - AgregarExpedienteUseCase e inyectamos la dependencia
Console.WriteLine("/////////////////////////////////////////////////////////////////////");
Console.WriteLine("////// PRUEBA UNITARIA - CASO DE USO PARA AGREGAR UN EXPEDIENTE ////");
Console.WriteLine("////////////////////////////////////////////////////////////////////");
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
    Console.WriteLine($"Id del expediente agreagado: {agregarExpedienteResponse.IdExpediente}");
}
catch(AplicacionException e)
{
    Console.WriteLine(e.Message);
}
catch(DominioException e)
{
    Console.WriteLine(e.Message);
}
catch(RepositorioException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();

/////////////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA OBTENER TODOS LOS EXPEDIENTE /////
///////////////////////////////////////////////////////////////////////////

Console.WriteLine("/////////////////////////////////////////////////////////////////////");
Console.WriteLine("////// PRUEBA UNITARIA - CASO DE USO PARA OBTENER TODOS LOS EXPEDIENTE ////");
Console.WriteLine("////////////////////////////////////////////////////////////////////");
//creamos la instancia del caso de uso - ObtenerTodosExpedienteUseCase e inyectamos dependencias
var obtenerTodosExpedientes = new ObtenerTodosExpedienteUseCase(repositorioExpediente);

//creamos la variable response donde nos va a retornar todos los expedientes
var listaExpedientes = obtenerTodosExpedientes.Ejecutar();

//recorremos la lista
foreach(var e in listaExpedientes.Expedientes)
{
    Console.WriteLine($"id: {e.Id} | caratula: {e.Caratula} | Fecha de Creacion: {e.FechaCreacion} |");
    Console.WriteLine($"Fecha de Ultima Actualizacion: {e.FechaUltimaModificacion} | Ultimo usuario: {e.UsuarioUltimoCambio} | Estado: {e.Estado}");
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
}

Console.WriteLine();
Console.WriteLine();

/////////////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA OBTENER UN EXPEDIENTE POR ID //////
/////////////////////////////////////////////////////////////////////////////

Console.WriteLine("/////////////////////////////////////////////////////////////////////");
Console.WriteLine("////// PRUEBA UNITARIA - CASO DE USO PARA OBTENER UN EXPEDIENTE POR ID ////");
Console.WriteLine("////////////////////////////////////////////////////////////////////");
Console.WriteLine("El id a buscar es: 'e1fe34b9-c05f-4f54-b14d-b890d67c4acf'");
var obtenerExpedientePorId = new ObtenerPorIdExpedienteCaseUse(repositorioExpediente);
var obtenerExpedientePorIdResponse = new ObtenerPorIdExpedienteRequest(Guid.Parse("e1fe34b9-c05f-4f54-b14d-b890d67c4acf"));

try
{
    var expedienteId = obtenerExpedientePorId.Ejecutar(obtenerExpedientePorIdResponse);
    Console.WriteLine($"id: {expedienteId.Expediente.Id} | caratula: {expedienteId.Expediente.Caratula} | Fecha de Creacion: {expedienteId.Expediente.FechaCreacion} |");
    Console.WriteLine($"Fecha de Ultima Actualizacion: {expedienteId.Expediente.FechaUltimaModificacion} | Ultimo usuario: {expedienteId.Expediente.UsuarioUltimoCambio} | Estado: {expedienteId.Expediente.Estado}");
}
catch(AplicacionException e)
{
    Console.WriteLine(e.Message);
}
catch(DominioException e)
{
    Console.WriteLine(e.Message);
}
catch(RepositorioException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();

/////////////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA MODIFICAR UN EXPEDIENTE - CARATULA Y ESTADO /////
///////////////////////////////////////////////////////////////////////////

Console.WriteLine("/////////////////////////////////////////////////////////////////////");
Console.WriteLine("////// PRUEBA UNITARIA - CASO DE USO PARA MODIFICAR UN EXPEDIENTE - CARATULA Y ESTADO ////");
Console.WriteLine("////////////////////////////////////////////////////////////////////");
Console.WriteLine("El id expediente a modificar: '40e780ea-0327-47c7-a71b-b8efec64c1f8'");
var modificarExpediente = new ModificarCaratulaExpedienteUseCase(repositorioExpediente, autorizacionService);
var cambiarEstado = new CambiarEstadoExpedienteUseCase(repositorioExpediente, autorizacionService);
var modicarExpedienteRequest = new ModificarCaratulaExpedienteRequest(Guid.Parse("40e780ea-0327-47c7-a71b-b8efec64c1f8"), "cambio de caratula", idUsuario);
var cambiarEstadoRequest = new CambiarEstadoExpedienteRequest(Guid.Parse("1ebb58b7-4599-43d4-8f19-61667461cee5"), "ConResolucion", idUsuario);

try
{
    var expedienteModificado1 = modificarExpediente.Ejecutar(modicarExpedienteRequest);
    var expedienteModificado2 = cambiarEstado.Ejecutar(cambiarEstadoRequest);
    Console.WriteLine($"El expediente con id {expedienteModificado1.IdExpediente} se le cambio la caratula");
    Console.WriteLine($"El expediente con id {expedienteModificado2.IdExpediente} se le cambio el estado");
}
catch(AplicacionException e)
{
    Console.WriteLine(e.Message);
}
catch(DominioException e)
{
    Console.WriteLine(e.Message);
}
catch(RepositorioException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();


/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////

//instanciamos el repositorio que vamos a inyectar en nuestro caso de uso
var repositorioTramite = new TramiteTxtRepository();

//instanciamos el servicio de actualizacion de estado de expediente que vamos a inyectar
var actualizacionEstadoExpediente = new ActualizacionEstadoExpedienteService(repositorioTramite,repositorioExpediente);

/////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA AGREGAR UN TRAMITE ////////
/////////////////////////////////////////////////////////////////////

//crear la instancia de el caso de uso - AgregarTramiteUseCase e inyectamos la dependencia
var agregarTramite = new AgregarTramiteUseCase(repositorioTramite,autorizacionService,actualizacionEstadoExpediente);

//creamos nuestra peticion para crear un archivo
var agregarTramiteRequest = new AgregarTramiteRequest(
    Guid.Parse("e1fe34b9-c05f-4f54-b14d-b890d67c4acf"), //id del expediente
    "Nuevo contenido",  //cotenido
    idUsuario //usuario que creo el expediente
);

//usamos un try catch para captar errores
try
{
    //creamos la variable donde nuestro caso de uso no vas a retornar una respuesta
    var agregarTramiteResponse = agregarTramite.Ejecutar(agregarTramiteRequest);

    //verificamos que en nuestro agregarTramiteResponse (respuesta) este el id del tramite agregado
    Console.WriteLine($"el id del tramite es: {agregarTramiteResponse.IdTramite}");
}
catch (AplicacionException e)
{   
    Console.WriteLine(e.Message);
}
catch(DominioException e)
{
    Console.WriteLine(e.Message);
}
catch(RepositorioException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}
