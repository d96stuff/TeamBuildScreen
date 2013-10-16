//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.SaverTfs2012
{
    #region Usings

    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Threading;
    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Core.ViewModels;
    using TeamBuildScreen.Core.Views;
    using TeamBuildScreen.Tfs2012.Models;
    using TeamBuildScreen.Core;

    #endregion

	public partial class App : System.Windows.Application
    {
        #region Fields

        private TeamBuildScreenSaver screenSaver;

        #endregion

        #region Methods

        private void OnStartup(Object sender, StartupEventArgs e)
        {
            IBuildServerService service = new BuildServerService();
            IDomainProjectPicker projectPicker = new TeamProjectPickerAdapter();
            this.screenSaver = new TeamBuildScreenSaver(service, projectPicker);

            string[] args = e.Args;

            this.screenSaver.Startup(args);
        }
        
        private void OnExit(object sender, ExitEventArgs e)
        {
            this.screenSaver.Exit();
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