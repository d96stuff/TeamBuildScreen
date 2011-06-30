﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using OpenRasta.Configuration;
using OpenRasta.Hosting.HttpListener;
using OpenRasta.DI;

namespace TeamBuildScreen.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
