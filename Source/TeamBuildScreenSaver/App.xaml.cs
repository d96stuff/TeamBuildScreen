//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver
{
    #region Usings

    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Threading;
    using Microsoft.TeamFoundation.Client;
    using TeamBuildScreenSaver.Models;
    using TeamBuildScreenSaver.ViewModels;
    using TeamBuildScreenSaver.Views;

    #endregion

	public partial class App : System.Windows.Application
    {
        #region Fields

        /// <summary>
        /// The service used to contact the build server.
        /// </summary>
        private IBuildServerService service;
        private ScreenSaver<Main, MainViewModel> screenSaver;

        #endregion

        #region Methods

        private void OnStartup(Object sender, StartupEventArgs e)
        {
            string[] args = e.Args;

            if (args.Length > 0)
            {
                string arg = args[0].ToLower(CultureInfo.InvariantCulture).Trim().Substring(0, 2);

                switch (arg)
                {
                    case "/c": // configuration mode
                        ScreenSaverSettings settings = new ScreenSaverSettings(new ScreenSaverSettingsModel());
                        settings.ShowDialog();
                        break;
                    case "/p": // preview mode
                        this.InitializeScreenSaver(false, 1);
                        this.screenSaver.ShowPreview(Convert.ToInt32(args[1]));
                        this.service.Start();
                        break;
                    case "/s": // normal mode
                        this.InitializeScreenSaver(true);
                        this.screenSaver.Show();
                        this.service.Start();
                        break;
                    default: // unknown argument
                        Application.Current.Shutdown();
                        break;
                }
            }
            else
            {
                // no arguments passed in
                this.InitializeScreenSaver(true);
                this.screenSaver.Show();
                this.service.Start();
            }
        }

        /// <summary>
        /// Initializes the screen saver. Sets the <see cref="MainViewModel.InnerMargin"/> to 8.
        /// </summary>
        /// <param name="closeOnClicked">A value that indicates whether the screen saver should close when clicked on.</param>
        private void InitializeScreenSaver(bool closeOnClicked)
        {
            this.InitializeScreenSaver(closeOnClicked, 8);
        }

        /// <summary>
        /// Initializes the screen saver.
        /// </summary>
        /// <param name="closeOnClicked">A value that indicates whether the screen saver should close when clicked on.</param>
        /// <param name="innerMargin">
        /// The margin to set between each <see cref="BuildPanel"/>. This usually only needs to be set under special 
        /// circumnstances, for example - if the view is to be displayed in a small preview window.
        /// </param>
        private void InitializeScreenSaver(bool closeOnClicked, int innerMargin)
        {
            string tfsUri = Settings.Default.TfsUri;

            if (string.IsNullOrEmpty(tfsUri))
            {
                Application.Current.Shutdown();
            }

            TeamFoundationServer tfsServer = new TeamFoundationServer(tfsUri);

            this.service = new BuildServerService(tfsServer.Uri.AbsoluteUri);

            StringCollection builds = Settings.Default.Builds;

            MainViewModel viewModel = new MainViewModel(this.service, builds);
            viewModel.Columns = Settings.Default.Columns;
            viewModel.CloseOnClicked = closeOnClicked;
            viewModel.InnerMargin = innerMargin;

            tfsServer.Dispose();

            this.screenSaver = new ScreenSaver<Main, MainViewModel>(viewModel);
        }

        private void OnExit(object sender, ExitEventArgs e)
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

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO: Log details
            MessageBox.Show(
                string.Format(
                "An error has been detected in the application that has caused the application to shutdown:\n\n{0}\n\nApologies for any inconvenience.",
                e.Exception.Message),
                "Team Build Screen Saver",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            e.Handled = true;

            if (Application.Current != null)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion
    }
}