using System;

namespace Server1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Serve 1";

            ServerTcp serverTcp = new ServerTcp();
            serverTcp.StartServer();

            Console.Read();
        }
    }
}
