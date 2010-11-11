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

    public class TeamBuildScreenSaver
    {
        #region Fields

        /// <summary>
        /// The service used to contact the build server.
        /// </summary>
        private IBuildServerService service;
        private ScreenSaver<Main, BuildGridViewModel> screenSaver;
        private IDomainProjectPicker projectPicker;

        #endregion

        public TeamBuildScreenSaver(IBuildServerService service, IDomainProjectPicker projectPicker)
        {
            this.service = service;
            this.projectPicker = projectPicker;
        }

        public void Startup(string[] args)
        {
            if (args.Length > 0)
            {
                string arg = args[0].ToLower(CultureInfo.InvariantCulture).Trim().Substring(0, 2);

                switch (arg)
                {
                    case "/c": // configuration mode
                        var settingsModel = new ScreenSaverSettingsModel(this.service, this.projectPicker);
                        settingsModel.Load();
                        var viewModel = new ScreenSaverSettingsViewModel(settingsModel);
                        ScreenSaverSettings settings = new ScreenSaverSettings(viewModel);
                        viewModel.CloseRequested += (object sender, EventArgs e) => {
                            settings.Close();
                        };
                        viewModel.SaveRequested += (object sender, EventArgs e) => {
                            settings.Close();
                        };
                        settings.ShowDialog();
                        break;
                    case "/p": // preview mode
                        this.InitializeScreenSaver(false, 1);

                        if (this.screenSaver != null)
                        {
                            this.screenSaver.ShowPreview(Convert.ToInt32(args[1]));
                            this.service.Start();
                        }
                        break;
                    case "/s": // normal mode
                        this.InitializeScreenSaver(true);

                        if (this.screenSaver != null)
                        {
                            this.screenSaver.Show();
                            this.service.Start();
                        }
                        break;
                    default: // unknown argument
                        Application.Current.Shutdown();
                        break;
                }
            }
            else
            {
                this.InitializeScreenSaver(true);

                if (this.screenSaver != null)
                {
                    this.screenSaver.Show();
                    this.service.Start();
                }
            }
        }

        public void Exit()
        {
            if (this.screenSaver != null)
            {
                this.screenSaver.Dispose();
            }

            if (this.service != null)
            {
                this.service.Dispose();
            }
        }

        /// <summary>
        /// Initializes the screen saver. Sets the <see cref="BuildGridViewModel.InnerMargin"/> to 8.
        /// </summary>
        /// <param name="allowClose">A value that indicates whether the screen saver should close when clicked on.</param>
        private void InitializeScreenSaver(bool allowClose)
        {
            this.InitializeScreenSaver(allowClose, 8);
        }

        /// <summary>
        /// Initializes the screen saver.
        /// </summary>
        /// <param name="allowClose">A value that indicates whether the screen saver should close when clicked on.</param>
        /// <param name="innerMargin">
        /// The margin to set between each <see cref="BuildPanel"/>. This usually only needs to be set under special 
        /// circumnstances, for example - if the view is to be displayed in a small preview window.
        /// </param>
        private void InitializeScreenSaver(bool allowClose, int innerMargin)
        {
            string tfsUri = Settings.Default.TfsUri;

            if (string.IsNullOrEmpty(tfsUri))
            {
                Application.Current.Shutdown();
            }
            else
            {
                this.service.TfsUrl = tfsUri;
                StringCollection builds = Settings.Default.Builds;

                BuildGridViewModel viewModel = new BuildGridViewModel(this.service, builds);
                viewModel.Columns = Settings.Default.Columns;
                if (allowClose)
                {
                    viewModel.CloseRequested += (object sender, EventArgs e) => { Application.Current.Shutdown(); };
                }
                viewModel.InnerMargin = innerMargin;

                this.screenSaver = new ScreenSaver<Main, BuildGridViewModel>(viewModel);
            }
        }
    }
}