//-----------------------------------------------------------------------
// <copyright file="TeamBuildDesktop.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core
{
    using System;
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
            // we will take care of shutting things down
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var settingsModel = new ScreenSaverSettingsModel(this.service, this.projectPicker);

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
            var builds = Settings.Default.Builds;
            var viewModel = new DesktopViewModel(this.service, builds);
            var desktop = new Desktop(viewModel);

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