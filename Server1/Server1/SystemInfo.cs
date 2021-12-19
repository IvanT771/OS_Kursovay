using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Topshelf.Runtime.Windows;
using System.Management;

namespace Server1
{
   
    public class SystemInfo
    {

        [DllImport("kernel32.dll")]
        static public extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static public extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        public string GetGPUName()
        {
            ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (ManagementObject obj in objvide.Get())
            {
                return obj["Name"].ToString();
            }
            
            return "Error";
        } 


        public string HideServerConsole(int hideTime)
        {
            try 
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);

                ShowConsoleOnTimeAsync(hideTime);

                return "The operation was successfully completed";
            }
            catch
            {
                return "Error";
            }
        }

        private async Task ShowConsoleOnTimeAsync(int hideTime)
        {
            await Task.Delay(hideTime);

            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOW);
        }
    }
}
