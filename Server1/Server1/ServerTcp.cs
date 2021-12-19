using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server1
{
    public class ServerTcp
    {
        const string ip = "127.0.0.1";
        const int port = 7071;

        [Obsolete]
        public void StartServer()
        {
            try
            {
                TcpListener serverSocket = new TcpListener(IPAddress.Parse(ip), port);
                TcpClient clientSocket = default(TcpClient);
                int counter = 0; //Счетчик подключенных клиентов

                serverSocket.Start();
                Console.WriteLine($"Server started on port {port}.");

                while (true)
                {
                    clientSocket = serverSocket.AcceptTcpClient();
                    Console.WriteLine($"Client {++counter} сonnected to the server.");

                    //Создаем нового клиента
                    HandleClinet client = new HandleClinet();

                    //Активируем клиента
                    client.StartClient(clientSocket, Convert.ToString(counter));
                }
            }
            catch 
            {

                Console.WriteLine("Failed to start the server!");
            }
           
        }
    }
}
