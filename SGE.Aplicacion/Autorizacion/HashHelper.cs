using System.Security.Cryptography;
using System.Text;

namespace SGE.Aplicacion.Autorizacion;

public static class HashHelper
{
    public static string GenerarSHA256(string texto)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(texto);
        byte[] hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash);
    }
}
