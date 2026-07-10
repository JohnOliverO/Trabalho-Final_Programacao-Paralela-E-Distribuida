using System.Net.Sockets;
using System.Text;

Console.WriteLine("==================================");
Console.WriteLine("    CLIENTE BANCÁRIO TCP");
Console.WriteLine("==================================");
Console.WriteLine("Conectando ao servidor...");

TcpClient cliente = new();

try
{
    cliente.Connect("127.0.0.1", 5000);

    Console.WriteLine("Conectado com sucesso!");
    Console.WriteLine();

    Console.WriteLine("Digite 'help' para listar os comandos.");
    Console.WriteLine("Digite 'sair' para encerrar.");
    Console.WriteLine();

    NetworkStream stream = cliente.GetStream();

    while (true)
    {
        Console.Write("> ");

        string? comando = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(comando))
            continue;

        if (comando.Equals("sair", StringComparison.OrdinalIgnoreCase))
            break;

        byte[] dados = Encoding.UTF8.GetBytes(comando);

        stream.Write(dados, 0, dados.Length);

        byte[] buffer = new byte[1024];

        int bytesLidos = stream.Read(buffer, 0, buffer.Length);

        if (bytesLidos == 0)
        {
            Console.WriteLine("Servidor desconectado.");
            break;
        }

        string resposta = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

        Console.WriteLine();
        Console.WriteLine(resposta);
        Console.WriteLine();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}
finally
{
    cliente.Close();
    Console.WriteLine("Conexão encerrada.");
}