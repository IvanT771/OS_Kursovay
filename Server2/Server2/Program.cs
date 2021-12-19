using System;

namespace Server2
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServerTcp serverTcp = new ServerTcp();
            serverTcp.StartServer();

            Console.Read();
        }
    }
}
