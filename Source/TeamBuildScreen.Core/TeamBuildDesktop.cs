//-----------------------------------------------------------------------
// <copyright file="TeamBuildDesktop.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuildScreen.Core.Models;
using System.Collections.Specialized;
using TeamBuildScreen.Core.ViewModels;
using TeamBuildScreen.Core.Views;
using System.Globalization;

namespace TeamBuildScreen.Core
{
    public class TeamBuildDesktop
    {
        private IBuildServerService service;
        private IDomainProjectPicker projectPicker;
        private string title;

        public TeamBuildDesktop(IBuildServerService service, IDomainProjectPicker projectPicker, string title)
        {
            this.service = service;
            this.projectPicker = projectPicker;
            this.title = title;
        }

        public void Startup(string[] args)
        {
            if (args.Length > 0)
            {
                string arg = args[0].ToLower(CultureInfo.InvariantCulture).Trim().Substring(0, 2);

                switch (arg)
                {
                    case "/c": // configuration mode
                        ScreenSaverSettingsModel settingsModel = new ScreenSaverSettingsModel(this.service, this.projectPicker);
                        settingsModel.Load();
                        ScreenSaverSettings settings = new ScreenSaverSettings(settingsModel);
                        settings.ShowDialog();
                        break;
                }
            }
            else
            {
                this.service.TfsUrl = Settings.Default.TfsUri;
                StringCollection builds = Settings.Default.Builds;

                MainViewModel viewModel = new MainViewModel(this.service, builds);
                viewModel.Columns = Settings.Default.Columns;
                viewModel.CloseOnClicked = false;

                Desktop desktop = new Desktop();
                desktop.Title = this.title;
                desktop.DataContext = viewModel;
                desktop.Show();
                this.service.Start();
            }
        }
    }
}