//-----------------------------------------------------------------------
// <copyright file="TeamBuildDesktop.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Windows;
    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Core.ViewModels;
    using TeamBuildScreen.Core.Views;

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

        public void Startup()
        {
            this.service.TfsUrl = Settings.Default.TfsUri;
            var builds = Settings.Default.Builds;
            var viewModel = new DesktopViewModel(this.service, builds);
            var desktop = new Desktop(viewModel);

            // configure view model
            viewModel.Columns = Settings.Default.Columns;
            viewModel.ConfigRequested += (object sender, EventArgs e) =>
            {
                // restore the view
                desktop.Restore();

                // this is a blocking call
                var saved = this.ShowSettingsDialog();

                if (saved)
                {
                    // re-init builds after settings dialog is closed
                    viewModel.InitializeBuildPanels(Settings.Default.Builds);
                }
            };

            // configure view
            desktop.Title = this.title;
            desktop.Closed += (object sender, EventArgs e) =>
            {
                Application.Current.Shutdown();
            };

            // show view
            desktop.Show();

            // start monitoring the builds
            this.service.Start();
        }

        private bool ShowSettingsDialog()
        {
            var saved = false;
            var settingsModel = new ScreenSaverSettingsModel(this.service, this.projectPicker);
            var viewModel = new ScreenSaverSettingsViewModel(settingsModel);
            var settings = new ScreenSaverSettings(viewModel);

            // load settings
            settingsModel.Load();

            // configure view model
            viewModel.CloseRequested += (object sender, EventArgs e) =>
            {
                settings.Close();
            };
            viewModel.SaveRequested += (object sender, EventArgs e) =>
            {
                settings.Close();
                saved = true;
            };

            // show view
            settings.ShowDialog();

            return saved;
        }
    }
}