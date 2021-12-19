using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Topshelf.Runtime.Windows;

namespace Server2
{
   
    public class SystemInfo
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class MEMORYSTATUSEX
        {
            public uint dwLength;           //Размер структуры
            public uint dwMemoryLoad;       // Число от 0 до 100, указывающее приблизительный процент используемой физической памяти
            public ulong ullTotalPhys;      //общее кол-во физической(оперативной) памяти
            public ulong ullAvailPhys;      //свободное кол-во физической(оперативной) памяти
            public ulong ullTotalPageFile;  //предел памяти для системы или текущего процесса
            public ulong ullAvailPageFile;  //Максимальный объем памяти,который текущий процесс может передать в байтах.
            public ulong ullTotalVirtual;   //общее количество виртуальной памяти(файл подкачки)
            public ulong ullAvailVirtual;   //свободное количество виртуальной памяти(файл подкачки)
            public ulong ullAvailExtendedVirtual;

            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);


        public string GetPhysicsMemory()
        {
            
            MEMORYSTATUSEX mEMORYSTATUSEX = new MEMORYSTATUSEX();

            if (GlobalMemoryStatusEx(mEMORYSTATUSEX))
            {
                return mEMORYSTATUSEX.dwMemoryLoad.ToString();
            }
            else
            {
                return "Error API";
            }
        }

        public string GetVirtualMemory()
        {
            MEMORYSTATUSEX mEMORYSTATUSEX = new MEMORYSTATUSEX();

            if (GlobalMemoryStatusEx(mEMORYSTATUSEX))
            {
                var procent = mEMORYSTATUSEX.ullAvailVirtual / (mEMORYSTATUSEX.ullTotalVirtual / 100);
                return procent.ToString();
            }
            else
            {
                return "Error API";
            }
        }
    }
}
