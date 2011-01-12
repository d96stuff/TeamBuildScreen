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
            var settingsModel = new ScreenSaverSettingsModel(this.service, this.projectPicker);
            var builds = Settings.Default.Builds;
            var viewModel = new DesktopViewModel(this.service, builds);

            // we must instantiate this window first in the application, otherwise the settings for will become the main 
            // window for the application and it will exit after the settings for is closed
            var desktop = new Desktop(viewModel);

            if (string.IsNullOrEmpty(settingsModel.TfsUri))
            {
                // no config found, show settings dialog
                var saved = this.ShowSettingsDialog(settingsModel);

                if (!saved || string.IsNullOrEmpty(settingsModel.TfsUri))
                {
                    // no config found, user cancelled settings dialog - exit application
                    Application.Current.Shutdown();
                    return;
                }
            }

            this.service.TfsUrl = settingsModel.TfsUri;

            // configure view model
            viewModel.Columns = Settings.Default.Columns;
            viewModel.ConfigRequested += (object sender, EventArgs e) =>
            {
                // restore the view
                desktop.Restore();

                // stop querying the server
                this.service.Stop();

                // this is a blocking call
                var saved = this.ShowSettingsDialog(settingsModel);

                if (saved)
                {
                    // re-init builds after settings dialog is closed
                    viewModel.InitializeBuildPanels(Settings.Default.Builds);
                    viewModel.Columns = Settings.Default.Columns;
                }

                this.service.Start();
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

        private bool ShowSettingsDialog(ScreenSaverSettingsModel settingsModel)
        {
            var saved = false;
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
                saved = true;
                settings.Close();
            };

            // show view
            settings.ShowDialog();

            return saved;
        }
    }
}