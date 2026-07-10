public class Banco
{
    private decimal saldo = 1000m;

    private readonly object saldoLock = new();

    public decimal ConsultarSaldo()
    {
        return saldo;
    }

    public string Depositar(decimal valor)
    {
        lock (saldoLock)
        {
            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Depositando R$ {valor}");

            Thread.Sleep(500);

            saldo += valor;

            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Novo saldo: {saldo}");

            return $"Depósito realizado.\nSaldo atual: R$ {saldo:F2}";
        }
    }

    public string Sacar(decimal valor)
    {
        lock (saldoLock)
        {
            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Sacando R$ {valor}");

            Thread.Sleep(500);

            if (saldo >= valor)
            {
                saldo -= valor;

                Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Novo saldo: {saldo}");

                return $"Saque realizado.\nSaldo atual: R$ {saldo:F2}";
            }

            return $"Saldo insuficiente.\nSaldo atual: R$ {saldo:F2}";
        }
    }
}