using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using Roomie.Desktop.Engine;

namespace Roomie.Desktop.Service
{
    //TODO: make this work!
    public partial class RoomieService : ServiceBase
    {
        RoomieEngine engine;
        public RoomieService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            engine = new RoomieEngine();
            engine.Start();
        }

        protected override void OnStop()
        {
            engine.Shutdown();
        }
    }
}
