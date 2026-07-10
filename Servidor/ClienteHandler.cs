using System.Net.Sockets;
using System.Text;

public class ClienteHandler
{
    private TcpClient cliente;
    private Banco banco;

    public ClienteHandler(TcpClient cliente, Banco banco)
    {
        this.cliente = cliente;
        this.banco = banco;
    }

    public void Atender()
    {
        Console.WriteLine("Cliente conectado!");
        Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId}");

        try
        {
            NetworkStream stream = cliente.GetStream();

            byte[] buffer = new byte[1024];

            int bytesLidos = stream.Read(buffer, 0, buffer.Length);

            string mensagem = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

            Console.WriteLine($"Mensagem: {mensagem}");

            string resposta;

            string[] partes = mensagem.Split(' ');

            switch (partes[0].ToLower())
            {
                case "saldo":

                    resposta = $"Saldo atual: R$ {banco.ConsultarSaldo():F2}";
                    break;

                case "depositar":

                    if (partes.Length < 2 || !decimal.TryParse(partes[1], out decimal deposito))
                        resposta = "Uso: depositar valor";
                    else
                        resposta = banco.Depositar(deposito);

                    break;

                case "sacar":

                    if (partes.Length < 2 || !decimal.TryParse(partes[1], out decimal saque))
                        resposta = "Uso: sacar valor";
                    else
                        resposta = banco.Sacar(saque);

                    break;

                default:

                    resposta =
            @"Comandos:

            saldo

            depositar 100

            sacar 50";
                    break;
            }

            byte[] respostaBytes = Encoding.UTF8.GetBytes(resposta);

            stream.Write(respostaBytes);

            cliente.Close();

            Console.WriteLine("Cliente desconectado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}