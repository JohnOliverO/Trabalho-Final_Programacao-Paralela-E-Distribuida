public class Conta
{
    public int Numero { get; set; }

    public decimal Saldo { get; set; }

    public Conta(int numero, decimal saldo)
    {
        Numero = numero;
        Saldo = saldo;
    }
}