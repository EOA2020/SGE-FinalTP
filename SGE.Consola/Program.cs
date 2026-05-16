using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Comun;
using SGE.Infraestructura;
using SGE.Dominio.Comun;
using EliminarExpedienteUseCase = SGE.Aplicacion.Expedientes.EliminarExpedienteUseCase;

/// ----------------------------- EXPEDIENTES --------------------------------- ///

//instanciamos el repositorio que vamos a inyectar en nuestro caso de uso
var repositorioExpediente = new ExpedienteTxtRepository();
var repositorioTramite = new TramiteTxtRepository();

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

//dto para probar el exception al agregar un expediente
var agregarExpedienteRequestError = new AgregarExpedienteRequest(
    "",
    idUsuario
);

//usamos un try catch para captar errores
try
{
    //creamos la variable donde nuestro caso de uso no vas a retornar una respuesta
    var agregarExpedienteResponse = agregarExpediente.Ejecutar(agregarExpedienteRequest);

    //verificamos que en nuestro agregarExpedienteResponse (respuesta) este el id del expediente agregado
    Console.WriteLine($"Id del expediente agreagado: {agregarExpedienteResponse.IdExpediente}");

    //prueba de errores
    var agregarExpedienteResponseError = agregarExpediente.Ejecutar(agregarExpedienteRequestError);
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
var obtenerPorIdExpediente = new ObtenerPorIdExpedienteCaseUse(repositorioExpediente);
var obtenerPorIdExpedienteRequest = new ObtenerPorIdExpedienteRequest(Guid.Parse("e1fe34b9-c05f-4f54-b14d-b890d67c4acf"));

try
{
    var expedienteId = obtenerPorIdExpediente.Ejecutar(obtenerPorIdExpedienteRequest);
    Console.WriteLine($"id: {expedienteId.Expediente.Id} | caratula: {expedienteId.Expediente.Caratula} | Fecha de Creacion: {expedienteId.Expediente.FechaCreacion} |");
    Console.WriteLine($"Fecha de Ultima Actualizacion: {expedienteId.Expediente.FechaUltimaModificacion} | Ultimo usuario: {expedienteId.Expediente.UsuarioUltimoCambio} | Estado: {expedienteId.Expediente.Estado}");

    //prueba de error
    var expedienteIdError = obtenerExpedientePorId.Ejecutar(obtenerExpedientePorIdErrorResponse);
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
}
catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}
catch(Exception e)
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
////// PRUEBA UNITARIA - CASO DE USO ELIMINAR EXPEDIENTE               /////
///////////////////////////////////////////////////////////////////////////

Console.WriteLine("/////////////////////////////////////////////////////////////////////");
Console.WriteLine("////// PRUEBA UNITARIA - CASO DE USO ELIMINAR EXPEDIENTE               /////");
Console.WriteLine("////////////////////////////////////////////////////////////////////");

var eliminarExpediente = new EliminarExpedienteUseCase(repositorioExpediente, repositorioTramite, autorizacionService);
var eliminarExpedienteRequest = new EliminarExpedienteRequest(Guid.Parse("af86880c-d392-4cb9-a84c-3fde35adbc49"), idUsuario);

try
{
    var eliminarExpedienteResponse = eliminarExpediente.Ejecutar(eliminarExpedienteRequest);
    Console.WriteLine($"Se elimino el expediente de id: {eliminarExpedienteResponse.IdExpediente}");
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


////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

Console.WriteLine(" ----------------------------- TRAMITES --------------------------------- ");

//instanciamos el servicio de actualizacion de estado de expediente que vamos a inyectar
var actualizacionEstadoExpediente = new ActualizacionEstadoExpedienteService(repositorioTramite,repositorioExpediente);

/////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA AGREGAR UN TRAMITE ////////
/////////////////////////////////////////////////////////////////////
/// 
Console.WriteLine("//////////////////////////////////////////////////////////");
Console.WriteLine("/ PRUEBA UNITARIA  - CASO DE USO PARA AGREGAR UN TRAMITE /");
Console.WriteLine("//////////////////////////////////////////////////////////");

//crear la instancia de el caso de uso - AgregarTramiteUseCase e inyectamos la dependencia
var agregarTramite = new AgregarTramiteUseCase(repositorioTramite,autorizacionService,actualizacionEstadoExpediente);

//creamos nuestra peticion para crear un archivo
var agregarTramiteRequest = new AgregarTramiteRequest(
    Guid.Parse("489c8a87-6a4f-47f0-ba99-39458658865e"), //id del expediente
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
}catch(DominioException e)
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
catch(DominioException e)
{
    Console.WriteLine(e.Message);
}
catch(RepositorioException e)
{
    Console.WriteLine(e.Message);
}
catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}
Console.WriteLine();
Console.WriteLine();

////////////////////////////////////////////////////////////////////////////////////
// PRUEBA UNITARIA - CASO DE USO PARA OBTENER TODOS LOS TRAMITES DE UN EXPEDIENTE //
////////////////////////////////////////////////////////////////////////////////////

Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////");
Console.WriteLine("/ PRUEBA UNITARIA - CASO DE USO PARA OBTENER TODOS LOS TRAMITES DE UN EXPEDIENTE /");
Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////");

//creamos la instancia del caso de uso - ObtenerTodosTramiteUseCase e inyectamos dependencias
var obtenerPorExpedienteId = new ObtenerPorExpedienteIdUseCase(repositorioTramite,repositorioExpediente);

var obtenerExpedientePorIdRequest = new ObtenerPorExpedienteIdRequest(Guid.Parse("a41b4b91-599e-456e-a2aa-2ea038dd73cf"));

//creamos la variable response donde nos va a retornar todos los tramites
var listaTramites = obtenerPorExpedienteId.Ejecutar(obtenerExpedientePorIdRequest);

//recorremos la lista
foreach(var t in listaTramites.Tramites)
{
    Console.WriteLine($"id: {t.Id} | id del expediente: {t.ExpedienteId} | etiqueta: {t.Etiqueta} | contenido: {t.Contenido} |");
    Console.WriteLine($" Fecha de Creacion: {t.FechaCreacion} | Fecha de Ultima Actualizacion: {t.FechaUltimaModificacion} | Ultimo usuario: {t.UsuarioUltimoCambio} ");
    Console.WriteLine("-------------------------------------------------------------------------------------------------------");
}

Console.WriteLine();
Console.WriteLine();

/////////////////////////////////////////////////////////////////////////////
/////// PRUEBA UNITARIA - CASO DE USO PARA OBTENER UN TRAMITE POR ID ////////
/////////////////////////////////////////////////////////////////////////////

Console.WriteLine("//////////////////////////////////////////////////////////////////////");
Console.WriteLine("//// PRUEBA UNITARIA - CASO DE USO PARA OBTENER UN TRAMITE POR ID ////");
Console.WriteLine("//////////////////////////////////////////////////////////////////////");
Console.WriteLine("El id a buscar es: 'e1fe34b9-c05f-4f54-b14d-b890d67c4acf'");

var obtenerPorId = new ObtenerTramitePorIdUseCase(repositorioTramite);
var obtenerPorIdRequest = new ObtenerPorIdResquest(Guid.Parse("93d11fed-7506-458f-86e2-70264a37f8b8"));

try
{
    var t = obtenerPorId.Ejecutar(obtenerPorIdRequest);
    Console.WriteLine($"id: {t.Id} | id del expediente: {t.ExpedienteId} | etiqueta: {t.Etiqueta} | contenido: {t.Contenido} |");
    Console.WriteLine($" Fecha de Creacion: {t.FechaCreacion} | Fecha de Ultima Actualizacion: {t.FechaUltimaModificacion} | Ultimo usuario: {t.UsuarioUltimoCambio} ");
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
}catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}
Console.WriteLine();
Console.WriteLine();

////////////////////////////////////////////////////////////////////////
/////// PRUEBA UNITARIA - CASO DE USO PARA MODIFICAR UN TRAMITE ////////
////////////////////////////////////////////////////////////////////////

Console.WriteLine("/////////////////////////////////////////////////////////////////////");
Console.WriteLine("/////// PRUEBA UNITARIA - CASO DE USO PARA MODIFICAR UN TRAMITE /////");
Console.WriteLine("/////////////////////////////////////////////////////////////////////");

Console.WriteLine("El id tramite a modificar: '93d11fed-7506-458f-86e2-70264a37f8b8'");

var modificarTramite = new ModificarTramiteUseCase(repositorioTramite, autorizacionService,actualizacionEstadoExpediente);
var modicarTramiteRequest = new ModificarTramiteRequest(Guid.Parse("93d11fed-7506-458f-86e2-70264a37f8b8"), "cambio de contenido", idUsuario);


try
{
    var t = modificarTramite.Ejecutar(modicarTramiteRequest);
    Console.WriteLine($"El tramite con id {t.IdTramite} se le cambio el contenido");
    
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
}catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();

////////////////////////////////////////////////////////////////////////
/////// PRUEBA UNITARIA - CASO DE USO PARA ELIMINAR UN TRAMITE /////////
////////////////////////////////////////////////////////////////////////

Console.WriteLine("////////////////////////////////////////////////////////////////////");
Console.WriteLine("/////// PRUEBA UNITARIA - CASO DE USO PARA ELIMINAR UN TRAMITE /////");
Console.WriteLine("////////////////////////////////////////////////////////////////////");

Console.WriteLine("El id tramite a eliminar: 'e72a193e-63be-4fd2-a835-8f3f004cf9e2'");

var eliminarTramite = new EliminarTramiteUseCase(repositorioTramite,autorizacionService,actualizacionEstadoExpediente);
var eliminarTramiteRequest = new EliminarTramiteRequest(Guid.Parse("e72a193e-63be-4fd2-a835-8f3f004cf9e2"),idUsuario);

try
{
    var t = eliminarTramite.Ejecutar(eliminarTramiteRequest);
    Console.WriteLine($" Se elimino el tramite {t.IdTramite}");
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
}catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();

//////////////////////////////////////////////////////////////////////////////////
///////// PRUEBA UNITARIA FALLIDA - CASO DE USO PARA ELIMINAR UN TRAMITE /////////
//////////////////////////////////////////////////////////////////////////////////

Console.WriteLine("////////////////////////////////////////////////////////////////////////////");
Console.WriteLine("/////// PRUEBA UNITARIA FALLIDA - CASO DE USO PARA ELIMINAR UN TRAMITE /////");
Console.WriteLine("////////////////////////////////////////////////////////////////////////////");

var eliminarTramiteInexistente = new EliminarTramiteUseCase(repositorioTramite,autorizacionService,actualizacionEstadoExpediente);
var eliminarTramiteInexistenteRequest = new EliminarTramiteRequest(Guid.NewGuid(),idUsuario);

try
{
    var t = eliminarTramiteInexistente.Ejecutar(eliminarTramiteInexistenteRequest);
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
}catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();

//////////////////////////////////////////////////////////////////////////////////
///////// PRUEBA UNITARIA FALLIDA - CASO DE USO PARA MODIFICAR UN TRAMITE ////////
//////////////////////////////////////////////////////////////////////////////////

Console.WriteLine("/////////////////////////////////////////////////////////////////////");
Console.WriteLine("/////// PRUEBA UNITARIA - CASO DE USO PARA MODIFICAR UN TRAMITE /////");
Console.WriteLine("/////////////////////////////////////////////////////////////////////");

var modificarTramiteInexistente = new ModificarTramiteUseCase(repositorioTramite, autorizacionService,actualizacionEstadoExpediente);
var modicarTramiteInexistenteRequest = new ModificarTramiteRequest(Guid.Parse("93d11fed-7506-458f-86e2-70264a37f8b8"),"", idUsuario);


try
{
    var t = modificarTramiteInexistente.Ejecutar(modicarTramiteInexistenteRequest);
    
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
}catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();

/////////////////////////////////////////////////////////////////////////////
/////// PRUEBA UNITARIA FALLIDA - CASO DE USO PARA OBTENER UN TRAMITE POR ID ////////
/////////////////////////////////////////////////////////////////////////////

Console.WriteLine("//////////////////////////////////////////////////////////////////////");
Console.WriteLine("//// PRUEBA UNITARIA - CASO DE USO PARA OBTENER UN TRAMITE POR ID ////");
Console.WriteLine("//////////////////////////////////////////////////////////////////////");
Console.WriteLine("El id a buscar es: 'e1fe34b9-c05f-4f54-b14d-b890d67c4acf'");

var obtenerPorIdInexistente = new ObtenerTramitePorIdUseCase(repositorioTramite);
var obtenerPorIdInexistenteRequest = new ObtenerPorIdResquest(Guid.NewGuid());

try
{
    var t = obtenerPorIdInexistente.Ejecutar(obtenerPorIdInexistenteRequest);
    Console.WriteLine($"id: {t.Id} | id del expediente: {t.ExpedienteId} | etiqueta: {t.Etiqueta} | contenido: {t.Contenido} |");
    Console.WriteLine($" Fecha de Creacion: {t.FechaCreacion} | Fecha de Ultima Actualizacion: {t.FechaUltimaModificacion} | Ultimo usuario: {t.UsuarioUltimoCambio} ");
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
}catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();

////////////////////////////////////////////////////////////////////////////////////
// PRUEBA UNITARIA - CASO DE USO PARA OBTENER TODOS LOS TRAMITES DE UN EXPEDIENTE //
////////////////////////////////////////////////////////////////////////////////////

Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////");
Console.WriteLine("/ PRUEBA UNITARIA FALLIDA - CASO DE USO PARA OBTENER TODOS LOS TRAMITES DE UN EXPEDIENTE /");
Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////");

//creamos la instancia del caso de uso - ObtenerTodosTramiteUseCase e inyectamos dependencias
var obtenerPorExpedienteIdInexistente = new ObtenerPorExpedienteIdUseCase(repositorioTramite,repositorioExpediente);

var obtenerExpedientePorIdInexistenteRequest = new ObtenerPorExpedienteIdRequest(Guid.NewGuid());

//creamos la variable response donde nos va a retornar todos los tramites

//recorremos la lista
try
{
    var listaTramitesInexistente = obtenerPorExpedienteIdInexistente.Ejecutar(obtenerExpedientePorIdInexistenteRequest);
    foreach(var t in listaTramitesInexistente.Tramites)
    {
        Console.WriteLine($"id: {t.Id} | id del expediente: {t.ExpedienteId} | etiqueta: {t.Etiqueta} | contenido: {t.Contenido} |");
        Console.WriteLine($" Fecha de Creacion: {t.FechaCreacion} | Fecha de Ultima Actualizacion: {t.FechaUltimaModificacion} | Ultimo usuario: {t.UsuarioUltimoCambio} ");
        Console.WriteLine("-------------------------------------------------------------------------------------------------------");
    }
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
}catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine();

/////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA FALLIDA - CASO DE USO PARA AGREGAR UN TRAMITE ////////
/////////////////////////////////////////////////////////////////////

Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////");
Console.WriteLine("/ PRUEBA UNITARIA FALLIDA - CASO DE USO PARA AGREGAR UN TRAMITE /");
Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////");

//crear la instancia de el caso de uso - AgregarTramiteUseCase e inyectamos la dependencia
var agregarTramiteInexistente = new AgregarTramiteUseCase(repositorioTramite,autorizacionService,actualizacionEstadoExpediente);

//creamos nuestra peticion para crear un archivo
var agregarTramiteInexistenteRequest = new AgregarTramiteRequest(
    Guid.Parse("e1fe34b9-c05f-4f54-b14d-b890d67c4acf"), //id del expediente
    "",  //cotenido
    idUsuario //usuario que creo el expediente
);

//usamos un try catch para captar errores
try
{
    //creamos la variable donde nuestro caso de uso no vas a retornar una respuesta
    var agregarTramiteResponse = agregarTramiteInexistente.Ejecutar(agregarTramiteInexistenteRequest);

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
}catch(EntidadNoEncontradaException e)
{
    Console.WriteLine(e.Message);
}catch(Exception e)
{
    Console.WriteLine(e.Message);
}
Console.WriteLine();
Console.WriteLine();
