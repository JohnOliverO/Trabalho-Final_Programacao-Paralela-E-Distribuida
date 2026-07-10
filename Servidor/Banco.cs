using System.Collections.Generic;
using System.Threading;

public class Banco
{
    private readonly Dictionary<int, Conta> contas;

    private readonly object saldoLock = new();

    public Banco()
    {
        contas = new Dictionary<int, Conta>()
        {
            {1001, new Conta(1001, 1000m)},
            {1002, new Conta(1002, 500m)},
            {1003, new Conta(1003, 2500m)}
        };
    }

    public string ConsultarSaldo(int numeroConta)
    {
        if (!contas.ContainsKey(numeroConta))
            return "Conta não encontrada.";

        return $"Conta {numeroConta}\nSaldo: R$ {contas[numeroConta].Saldo:F2}";
    }

    public string Depositar(int numeroConta, decimal valor)
    {
        lock (saldoLock)
        {
            if (!contas.ContainsKey(numeroConta))
                return "Conta não encontrada.";

            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Depositando R$ {valor} na conta {numeroConta}");

            Thread.Sleep(500);

            contas[numeroConta].Saldo += valor;

            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Novo saldo: {contas[numeroConta].Saldo}");

            return $"Depósito realizado.\nSaldo atual: R$ {contas[numeroConta].Saldo:F2}";
        }
    }

    public string Sacar(int numeroConta, decimal valor)
    {
        lock (saldoLock)
        {
            if (!contas.ContainsKey(numeroConta))
                return "Conta não encontrada.";

            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Sacando R$ {valor} da conta {numeroConta}");

            Thread.Sleep(500);

            if (contas[numeroConta].Saldo >= valor)
            {
                contas[numeroConta].Saldo -= valor;

                Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Novo saldo: {contas[numeroConta].Saldo}");

                return $"Saque realizado.\nSaldo atual: R$ {contas[numeroConta].Saldo:F2}";
            }

            return $"Saldo insuficiente.\nSaldo atual: R$ {contas[numeroConta].Saldo:F2}";
        }
    }
    public string Transferir(int contaOrigem, int contaDestino, decimal valor)
    {
        lock (saldoLock)
        {
            if (!contas.ContainsKey(contaOrigem))
                return "Conta de origem não encontrada.";

            if (!contas.ContainsKey(contaDestino))
                return "Conta de destino não encontrada.";

            if (contaOrigem == contaDestino)
                return "A conta de origem deve ser diferente da conta de destino.";

            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Transferindo R$ {valor} da conta {contaOrigem} para {contaDestino}");

            Thread.Sleep(500);

            if (contas[contaOrigem].Saldo < valor)
                return "Saldo insuficiente.";

            contas[contaOrigem].Saldo -= valor;
            contas[contaDestino].Saldo += valor;

            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Transferência concluída.");

            return
    $@"Transferência realizada com sucesso!

    Conta Origem ({contaOrigem}): R$ {contas[contaOrigem].Saldo:F2}
    Conta Destino ({contaDestino}): R$ {contas[contaDestino].Saldo:F2}";
        }
    }
}