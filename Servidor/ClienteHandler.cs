//Trabalho-Final_Programação-Paralela-E-Distribuída/Servidor/ClienteHandler.cs
using System.Net.Sockets;
using System.Text;

public class ClienteHandler
{
    private readonly TcpClient cliente;
    private readonly Comandos comandos;

    public ClienteHandler(TcpClient cliente, Banco banco)
    {
        this.cliente = cliente;
        comandos = new Comandos(banco);
    }

    public void Atender()
    {
        Console.WriteLine($"Cliente conectado (Thread {Environment.CurrentManagedThreadId})");

        try
        {
            NetworkStream stream = cliente.GetStream();

            while (cliente.Connected)
            {
                byte[] buffer = new byte[1024];

                int bytesLidos = stream.Read(buffer);

                if (bytesLidos == 0)
                    break;

                string mensagem = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                Console.WriteLine(
                    $"[{DateTime.Now:HH:mm:ss}] " +
                    $"Thread {Environment.CurrentManagedThreadId}: {mensagem}"
                );

                string resposta = comandos.Processar(mensagem);

                byte[] respostaBytes = Encoding.UTF8.GetBytes(resposta);

                stream.Write(respostaBytes);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            cliente.Close();

            Console.WriteLine($"Cliente desconectado (Thread {Environment.CurrentManagedThreadId})");
        }
    }
}