using System.Net.Sockets;
using System.Text;

Console.WriteLine("Conectando ao servidor...");

TcpClient cliente = new TcpClient();

cliente.Connect("127.0.0.1", 5000);

NetworkStream stream = cliente.GetStream();

Console.Write("Digite uma mensagem: ");

string mensagem = Console.ReadLine() ?? "";

byte[] dados = Encoding.UTF8.GetBytes(mensagem);

stream.Write(dados, 0, dados.Length);

byte[] buffer = new byte[1024];

int bytesLidos = stream.Read(buffer, 0, buffer.Length);

string resposta = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

Console.WriteLine();

Console.WriteLine(resposta);

cliente.Close();

Console.WriteLine("Conexão encerrada.");