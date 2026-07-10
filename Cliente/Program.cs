using System.Net.Sockets;
using System.Text;

Console.WriteLine("==========================");
Console.WriteLine("CLIENTE BANCÁRIO");
Console.WriteLine("==========================");

TcpClient cliente = new();

cliente.Connect("127.0.0.1", 5000);

NetworkStream stream = cliente.GetStream();

Console.WriteLine("Conectado ao servidor.");

while (true)
{
    Console.Write("\n> ");

    string? comando = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(comando))
        continue;

    if (comando.ToLower() == "sair")
        break;

    byte[] dados = Encoding.UTF8.GetBytes(comando);

    stream.Write(dados);

    byte[] buffer = new byte[1024];

    int bytesLidos = stream.Read(buffer);

    string resposta = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

    Console.WriteLine();
    Console.WriteLine(resposta);
}

cliente.Close();

Console.WriteLine("Conexão encerrada.");