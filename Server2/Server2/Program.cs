﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Server2
{

    public class Program
    {
        

        static void Main(string[] args)
        {
            ServerTcp serverTcp = new ServerTcp();
            serverTcp.StartServer();

            //SystemInfo systemInfo = new SystemInfo();
            //Console.WriteLine("Physics = "+systemInfo.GetPhysicsMemory()+"%");
            //Console.WriteLine("Virtual = "+systemInfo.GetVirtualMemory()+"%");

            Console.Read();

        }
    }
}
