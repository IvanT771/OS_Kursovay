using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server1
{
    public class HandleClinet
    {
        #region Fields 

        private SystemInfo _systemInfo = new SystemInfo();
        private TcpClient _clientSocket;
        private string _clientId;

        #endregion

        #region PrivateMethods

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

            Console.WriteLine("[MessageFromServer]: " + message);
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
                    Console.WriteLine($"[MessageFromClient {_clientId}]: {messageFromClient}");

                    //Обрабатываем сообщение
                    MessageProcessing(messageFromClient);

                }
                catch 
                {
                    Console.WriteLine($"Client {_clientId} disconnect!");
                    break;
                }
            }
        }

        /// <summary>
        /// Обрабатывает сообщение от клиента
        /// </summary>
        /// <param name="message"></param>
        private void MessageProcessing(string message)
        {
            if (message.Contains("HideServerConsole"))
            {
                int time = Convert.ToInt32(message.Split(':')[0]);
                SendHideServerConsole(time);
            }

            switch (message)
            {
                case "GetNameServer" : ShowMessageToClient("Server1"); break;
                case "GetGPUName": SendingGPUName(); break;
                default: break;
            }
        }

        private void SendingGPUName()
        {
            var result = _systemInfo.GetGPUName();
            ShowMessageToClient(result);
        }

        private void SendHideServerConsole(int time)
        {
            var result = _systemInfo.HideServerConsole(time);
            ShowMessageToClient(result);
        }

        #endregion
    }
}
