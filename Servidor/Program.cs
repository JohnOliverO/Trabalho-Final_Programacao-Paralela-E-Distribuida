using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener servidor = new TcpListener(IPAddress.Any, 5000);

servidor.Start();

Console.WriteLine("====================================");
Console.WriteLine(" Servidor iniciado na porta 5000");
Console.WriteLine(" Aguardando cliente...");
Console.WriteLine("====================================");

while (true)
{
    TcpClient cliente = servidor.AcceptTcpClient();

    Console.WriteLine("Cliente conectado!");

    NetworkStream stream = cliente.GetStream();

    byte[] buffer = new byte[1024];

    int bytesLidos = stream.Read(buffer, 0, buffer.Length);

    string mensagem = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

    Console.WriteLine($"Cliente enviou: {mensagem}");

    string resposta = $"Servidor recebeu: {mensagem}";

    byte[] respostaBytes = Encoding.UTF8.GetBytes(resposta);

    stream.Write(respostaBytes, 0, respostaBytes.Length);

    cliente.Close();

    Console.WriteLine("Cliente desconectado.\n");
}