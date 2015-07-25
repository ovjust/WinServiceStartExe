using Cjwdev.WindowsApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace YiGuoCrm.ServiceStartApp
{
    public class Class1
    {
        public static void Start()
        {
            //Thread.Sleep(30000);
            var appPaths = AppSettings.appPaths;
            string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory; ;

            var sw = File.CreateText(Path.Combine(currentDirectory, "log.txt"));
            sw.WriteLine(string.Format("{0} appStart {1}", DateTime.Now, appPaths));
            sw.Flush();
            sw.Close();
            //MessageBox.Show(appPaths);
            var paths = appPaths.Split(new char[]{';'},StringSplitOptions.RemoveEmptyEntries);
            foreach (var path in paths)
            {
                //Process.Start(path);
                StartApp(path);
                Thread.Sleep(1000);
            }
        }


        public static void StartApp(string strAppPath)
        {
            try
            {
                IntPtr userTokenHandle = IntPtr.Zero;
                ApiDefinitions.WTSQueryUserToken(ApiDefinitions.WTSGetActiveConsoleSessionId(), ref userTokenHandle);

                ApiDefinitions.PROCESS_INFORMATION procInfo = new ApiDefinitions.PROCESS_INFORMATION();
                ApiDefinitions.STARTUPINFO startInfo = new ApiDefinitions.STARTUPINFO();
                startInfo.cb = (uint)Marshal.SizeOf(startInfo);

                ApiDefinitions.CreateProcessAsUser(
                    userTokenHandle,
                    strAppPath,
                  "",
                    IntPtr.Zero,
                    IntPtr.Zero,
                    false,
                    0,
                    IntPtr.Zero,
                    null,
                    ref startInfo,
                    out procInfo);

                if (userTokenHandle != IntPtr.Zero)
                    ApiDefinitions.CloseHandle(userTokenHandle);

                var _currentAquariusProcessId = (int)procInfo.dwProcessId;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); 
            }
        }
    }
}
