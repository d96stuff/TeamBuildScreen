//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaverTests
{
    #region Usings

    using System;
    using System.Collections.Specialized;
    using System.Windows;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.Client;
    using TeamBuildScreenSaver.DataModels;
    using TeamBuildScreenSaver.Views;

    #endregion

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private Main main;
        private IBuildServerQuery serverQuery;

        #endregion

        #region Methods

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string tfsUri = "tfs";
            StringCollection builds = new StringCollection();
            int columns = 1;

            TeamFoundationServer tfsServer = new TeamFoundationServer(tfsUri);

            builds.Add("TeamProject;MyFirstBuild;Release;Any CPU");
            builds.Add("TeamProject;MySecondBuild;Release;Any CPU");
            builds.Add("TeamProject;MyThirdBuild;Release;Any CPU");

            this.serverQuery = new MockBuildServerQuery();

            tfsServer.Dispose();

            this.main = new Main(this.serverQuery, builds, MockConfigurationSummaryHandler);
            this.main.Columns = columns;
            this.main.Closed += new EventHandler(main_Closed);
            this.main.Show();
            this.serverQuery.Start();
        }

        private void main_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (this.serverQuery != null)
            {
                this.serverQuery.Dispose();
            }
        }

        private IConfigurationSummary MockConfigurationSummaryHandler(IBuildDetail build, string flavour, string platform)
        {
            Random random = new Random();

            bool tests = Convert.ToBoolean(random.Next(2));

            if (build.BuildFinished && tests)
            {
                return new MockConfigurationSummary();
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}