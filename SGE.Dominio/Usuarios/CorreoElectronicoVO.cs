using SGE.Dominio.Comun;

namespace SGE.Dominio.Usuarios;

public record class CorreoElectronicoVO
{
    public string Cuenta { get; private init; } = "";
    public string Dominio { get; private init; } = "";

    public CorreoElectronicoVO(string cuenta, string dominio)
    {
        if (string.IsNullOrWhiteSpace(cuenta) || string.IsNullOrWhiteSpace(dominio))
        {
            throw new DominioException("La cuenta y el dominio son obligatorios en las direcciones de email");
        }
        Cuenta = cuenta;
        Dominio = dominio;
    }

    protected CorreoElectronicoVO(){}

    public static CorreoElectronicoVO Parse(string emailCompleto)
    {
        if (string.IsNullOrWhiteSpace(emailCompleto) || !emailCompleto.Contains('@'))
        {
            throw new DominioException("El formato del email es inválido.");
        }

        var partes = emailCompleto.Split('@');
        
        // Validación adicional por si envían strings malformados como "usuario@" o "@dominio.com"
        if (string.IsNullOrWhiteSpace(partes[0]) || string.IsNullOrWhiteSpace(partes[1]))
        {
            throw new DominioException("El formato del email es inválido.");
        }

        return new CorreoElectronicoVO(partes[0], partes[1]);
    }

    public override string ToString()
    {
        return $"{Cuenta}@{Dominio}";
    }

}
