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

                if (partes.Length != 2)
                    return "Uso: saldo <conta>";

                if (!int.TryParse(partes[1], out int contaSaldo))
                    return "Número da conta inválido.";

                return banco.ConsultarSaldo(contaSaldo);

            case "depositar":

                if (partes.Length != 3)
                    return "Uso: depositar <conta> <valor>";

                if (!int.TryParse(partes[1], out int contaDeposito))
                    return "Número da conta inválido.";

                if (!decimal.TryParse(partes[2], out decimal valorDeposito))
                    return "Valor inválido.";

                return banco.Depositar(contaDeposito, valorDeposito);

            case "sacar":

                if (partes.Length != 3)
                    return "Uso: sacar <conta> <valor>";

                if (!int.TryParse(partes[1], out int contaSaque))
                    return "Número da conta inválido.";

                if (!decimal.TryParse(partes[2], out decimal valorSaque))
                    return "Valor inválido.";

                return banco.Sacar(contaSaque, valorSaque);
            case "transferir":

                if (partes.Length != 4)
                    return "Uso: transferir <contaOrigem> <contaDestino> <valor>";

                if (!int.TryParse(partes[1], out int contaOrigem))
                    return "Conta de origem inválida.";

                if (!int.TryParse(partes[2], out int contaDestino))
                    return "Conta de destino inválida.";

                if (!decimal.TryParse(partes[3], out decimal valorTransferencia))
                    return "Valor inválido.";

                return banco.Transferir(contaOrigem, contaDestino, valorTransferencia);
            case "help":
            case "ajuda":

                return """
==================================
COMANDOS DISPONÍVEIS
==================================

Contas existentes:

1001
1002
1003

Consultar saldo:

saldo 1001

Depositar:

depositar 1001 200

Sacar:

sacar 1001 50

Transferir:

transferir 1001 1002 300

Encerrar cliente:

sair
""";

            default:
                return "Comando desconhecido. Digite 'help'.";
        }
    }
}