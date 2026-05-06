using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;

Console.WriteLine("Hola mundo");

/////////////////////////////////////////////////////////////////////
////// PRUEBA UNITARIA - CASO DE USO PARA AGREGAR UN EXPEDIENTE ////
////////////////////////////////////////////////////////////////////
// //probamos creando una instacia del caso de uso y los dto
// Guid idPrueba = Guid.NewGuid();
// var autorizacion = new autorizacionService();
// var request = new AgregarExpedienteRequest(
//     "Archivo de prueba",
//     idPrueba  
// );
// var agregarExpediente = new AgregarExpedienteUseCase(autorizacion);
// var response = agregarExpediente.Ejecutar(request);

// Console.WriteLine(response.IdExpediente);

// class autorizacionService: IAutorizacionService
// {
//     public bool PoseeElPermiso(Guid idUsuario, Permiso permiso)
//     {
//         return true;
//     }
// }