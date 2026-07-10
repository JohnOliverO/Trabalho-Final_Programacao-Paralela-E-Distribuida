public class Banco
{
    private decimal saldo = 1000m;

    public decimal ConsultarSaldo()
    {
        return saldo;
    }

    public string Depositar(decimal valor)
    {
        saldo += valor;

        return $"Depósito realizado.\nSaldo atual: R$ {saldo:F2}";
    }

    public string Sacar(decimal valor)
    {
        Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Tentando sacar R$ {valor}");

        if (saldo >= valor)
        {
            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Saldo antes: {saldo}");

            Thread.Sleep(500);

            saldo -= valor;

            Console.WriteLine($"[{Environment.CurrentManagedThreadId}] Saldo depois: {saldo}");

            return $"Saque realizado.\nSaldo atual: R$ {saldo:F2}";
        }

        return $"Saldo insuficiente.\nSaldo atual: R$ {saldo:F2}";
    }
}