using System.Net;
using System.Net.Sockets;

TcpListener servidor = new TcpListener(IPAddress.Any, 5000);

servidor.Start();

Console.WriteLine("================================");
Console.WriteLine("Servidor iniciado");
Console.WriteLine("================================");

Banco banco = new Banco();

while (true)
{
    TcpClient cliente = servidor.AcceptTcpClient();

    ClienteHandler handler = new ClienteHandler(cliente, banco);

    Task.Run(() =>
    {
        handler.Atender();
    });
}