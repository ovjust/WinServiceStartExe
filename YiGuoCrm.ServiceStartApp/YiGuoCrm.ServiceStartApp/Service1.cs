using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Threading;

namespace YiGuoCrm.ServiceStartApp
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread th = new Thread(Class1.Start);
            th.IsBackground = true;
            th.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
