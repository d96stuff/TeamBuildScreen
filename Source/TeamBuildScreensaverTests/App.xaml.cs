using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Collections.Specialized;
using Microsoft.TeamFoundation.Client;
using TeamBuildScreensaver.DataModels;
using TeamBuildScreensaver.Views;
using Microsoft.TeamFoundation.Build.Client;

namespace TeamBuildScreensaverTests
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Main main;
        private IBuildServerQuery serverQuery;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string tfsUri = "tfs";
            StringCollection builds = new StringCollection();
            int columns = 1;

            TeamFoundationServer tfsServer = new TeamFoundationServer(tfsUri);

            builds.Add("TeamProject;MyFirstBuild");
            builds.Add("TeamProject;MySecondBuild");
            builds.Add("TeamProject;MyThirdBuild");

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
    }
}
