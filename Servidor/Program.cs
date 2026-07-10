using System.Net;
using System.Net.Sockets;

TcpListener servidor = new TcpListener(IPAddress.Any, 5000);

servidor.Start();

Banco banco = new Banco();

Console.WriteLine("====================================");
Console.WriteLine(" SERVIDOR BANCÁRIO");
Console.WriteLine(" Porta: 5000");
Console.WriteLine("====================================");

while (true)
{
    TcpClient cliente = servidor.AcceptTcpClient();

    ClienteHandler handler = new ClienteHandler(cliente, banco);

    _ = Task.Run(handler.Atender);
}