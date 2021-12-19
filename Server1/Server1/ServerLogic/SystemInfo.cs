using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Management;

namespace Server1
{  
    public class SystemInfo
    {
        #region Constant Fields

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        #endregion

        #region Field

        [DllImport("kernel32.dll")]
        static public extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static public extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        #region PublicMethods

        /// <summary>
        /// Возвращает имя видеокарты
        /// </summary>
        /// <returns></returns>
        public string GetGPUName()
        {
            ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");

            foreach (ManagementObject obj in objvide.Get())
            {
                return obj["Name"].ToString();
            }
            
            return "Error";
        } 

        /// <summary>
        /// Срыкает на заданное время окно сервера
        /// </summary>
        /// <param name="hideTime">время в млс</param>
        /// <returns></returns>
        public string HideServerConsole(int hideTime)
        {
            try 
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);

                _ = ShowConsoleOnTimeAsync(hideTime, handle);

                return "The operation was successfully completed";
            }
            catch
            {
                return "Error";
            }
        }

        #endregion

        #region PrivateMethods

        private async Task ShowConsoleOnTimeAsync(int hideTime, IntPtr handle)
        {
            await Task.Delay(hideTime);

            ShowWindow(handle, SW_SHOW);
        }

        #endregion
    }
}
