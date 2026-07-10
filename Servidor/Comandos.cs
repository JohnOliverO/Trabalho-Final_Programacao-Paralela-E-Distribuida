public class Comandos
{
    private readonly Banco banco;

    public Comandos(Banco banco)
    {
        this.banco = banco;
    }

    public string Processar(string mensagem)
    {
        string[] partes = mensagem.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (partes.Length == 0)
            return "Comando inválido.";

        switch (partes[0].ToLower())
        {
            case "saldo":
                return $"Saldo atual: R$ {banco.ConsultarSaldo():F2}";

            case "depositar":

                if (partes.Length < 2)
                    return "Uso: depositar <valor>";

                if (!decimal.TryParse(partes[1], out decimal deposito))
                    return "Valor inválido.";

                return banco.Depositar(deposito);

            case "sacar":

                if (partes.Length < 2)
                    return "Uso: sacar <valor>";

                if (!decimal.TryParse(partes[1], out decimal saque))
                    return "Valor inválido.";

                return banco.Sacar(saque);

            case "ajuda":
            case "help":

                return """
=========================
COMANDOS DISPONÍVEIS
=========================

saldo

depositar <valor>

sacar <valor>

help

sair
""";

            default:

                return "Comando desconhecido. Digite 'help'.";
        }
    }
}