using System;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public interface IExpedienteRepository
{
    //agregar el interfaz
    void AgregarExpediente(Expediente expediente);
}
