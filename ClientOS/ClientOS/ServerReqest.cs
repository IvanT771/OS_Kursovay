using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientOS
{
    public static class ServerReqest
    {
        #region PublicMethods

        public static string ReqestToServer(string reqest, TcpClient clientSocket)
        {
            if(!clientSocket.Connected)
                return "Соединение прервано!";

            //Отправка запроса на сервер 
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(reqest);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            //Ответ сервера
            byte[] inStream = new byte[255];
            serverStream.Read(inStream, 0, 255);
            string returndata = Encoding.ASCII.GetString(inStream);

            return returndata;
        }

        /// <summary>
        /// Отправляет запрос на сервер, возвращает bool в завмсимости 
        /// был ли обработан запрос
        /// </summary>
        /// <param name="result">хранит ответ на запрос</param>
        /// <param name="reqest">запрос на сервер</param>
        /// <param name="clientSocket">клиентское подключение</param>
        /// <returns></returns>
        public static bool TryReqestToServer(out string result, string reqest, TcpClient clientSocket)
        {
            try
            {
                //Отправка запроса на сервер 
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(reqest);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                //Ответ сервера
                byte[] inStream = new byte[255];
                serverStream.Read(inStream, 0, 255);
                string returndata = Encoding.ASCII.GetString(inStream);

                result = returndata;
                return true;
            }
            catch
            {
                result = "Connection error!";
                return false;
            }
        }

        #endregion
    }
}
