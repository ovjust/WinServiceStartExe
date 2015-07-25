using System;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Threading;

namespace CSCreateProcessAsUserFromService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // As creating a child process might be a time consuming operation,
            // its better to do that in a separate thread than blocking the main thread.
            System.Threading.Thread ProcessCreationThread = new System.Threading.Thread(MyThreadFunc);
            ProcessCreationThread.Start();
        }

        protected override void OnStop()
        {
        }

        // This thread function would launch a child process 
        // in the interactive session of the logged-on user.
        public static void MyThreadFunc()
        {
            //Thread.Sleep(30000);
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (ConfigurationManager.AppSettings[key] == "true")
                {
                    CreateProcessAsUserWrapper.LaunchChildProcess(key);
                    Thread.Sleep(1000);
                    //CreateProcessAsUserWrapper.LaunchChildProcess("C:\\Windows\\notepad.exe");
                }
            }
        }
    }
}
