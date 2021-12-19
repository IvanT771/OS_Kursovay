using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server2
{
    public class HandleClinet
    {
        private SystemInfo _systemInfo = new SystemInfo();
        private TcpClient _clientSocket;
        private string _clientId;

        public void StartClient(TcpClient inClientSocket, string id)
        {
            _clientSocket = inClientSocket;
            _clientId = id;

            //Создаем новый поток и запускаем его для общения с клиентом
            Thread ctThread = new Thread(ClientReqest);
            ctThread.Start();
        }

        /// <summary>
        /// Отправляет сообщение клиенту
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessageToClient(string message)
        {
            NetworkStream networkStream = _clientSocket.GetStream();
            var sendBytes = Encoding.ASCII.GetBytes(message);

            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();

            Console.WriteLine($"[{DateTime.Now}][Server]: {message}");
        }



        /// <summary>
        /// Получение запросов клиента
        /// </summary>
        private void ClientReqest()
        {
            byte[] inputBytes = new byte[255];

            while (true)
            {
                try
                {
                    //Получение сообщения от клиента
                    NetworkStream networkStream = _clientSocket.GetStream();
                    networkStream.Read(inputBytes, 0, 255);

                    //Перевод сообщения из массива байтов в строку 
                    var messageFromClient = Encoding.ASCII.GetString(inputBytes);
                    messageFromClient = messageFromClient.Substring(0, messageFromClient.IndexOf("$"));

                    //Вывод сообщения в лог сервера
                    Console.WriteLine($"[{DateTime.Now}][Client {_clientId}]: {messageFromClient}");

                    //Обрабатываем сообщение
                    MessageProcessing(messageFromClient);

                }
                catch 
                {
                    Console.WriteLine($"[{DateTime.Now}] Client {_clientId} disconnect!");
                    break;
                }
            }
        }

        private void MessageProcessing(string message)
        {
            switch (message)
            {
                case "GetVirtualMemory": SendingVirtualMemory(); break;
                case "GetPhysicMemory": SendingPhysicMemory(); break;
                case "GetNameServer" : ShowMessageToClient("Server2"); break;

                default: break;
            }
        }

        private void SendingVirtualMemory()
        {
            var result = _systemInfo.GetVirtualMemory();
            ShowMessageToClient(result);
        }

        private void SendingPhysicMemory()
        {
            var result = _systemInfo.GetPhysicsMemory();
            ShowMessageToClient(result);
        }
    }
}
