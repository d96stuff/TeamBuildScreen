//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaverDemo
{
    #region Usings

    using System;
    using System.Collections.Specialized;
    using System.Windows;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.Client;
    using TeamBuildScreenSaver.Models;
    using TeamBuildScreenSaver.Views;
    using TeamBuildScreenSaver.ViewModels;
    using TeamBuildScreenSaver.UnitTests.Models;
    using TeamBuildScreenSaver;

    #endregion

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private Main main;
        private IBuildServerService service;
        private ScreenSaver<Main, MainViewModel> screenSaver;

        #endregion

        #region Methods

        private void OnStartup(object sender, StartupEventArgs e)
        {
            this.InitializeScreenSaver(true);
            this.screenSaver.Show();
            this.service.Start();
        }

        /// <summary>
        /// Initializes the screen saver.
        /// </summary>
        /// <param name="closeOnClicked">A value that indicates whether the screen saver should close when clicked on.</param>
        private void InitializeScreenSaver(bool closeOnClicked)
        {
            string tfsUri = "tfs";
            StringCollection builds = new StringCollection();
            int columns = 1;

            TeamFoundationServer tfsServer = new TeamFoundationServer(tfsUri);

            builds.Add("TeamProject;MyFirstBuild;Release;Any CPU");
            builds.Add("TeamProject;MySecondBuild;Release;Any CPU");
            builds.Add("TeamProject;MyThirdBuild;Release;Any CPU");

            this.service = new MockBuildServerService();

            MainViewModel viewModel = new MainViewModel(this.service, builds, this.MockConfigurationSummaryHandler);
            viewModel.Columns = columns;
            viewModel.CloseOnClicked = closeOnClicked;

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

        private IConfigurationSummary MockConfigurationSummaryHandler(IBuildDetail build, string flavour, string platform)
        {
            Random random = new Random();

            bool tests = Convert.ToBoolean(random.Next(2));

            if (build.BuildFinished && tests)
            {
                MockConfigurationSummary mockConfigurationSummary = new MockConfigurationSummary();

                ITestSummary testSummary = new MockTestSummary(100, 3);

                mockConfigurationSummary.AddTestSummary(testSummary);

                return mockConfigurationSummary;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}