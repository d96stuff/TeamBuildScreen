using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using TeamBuildScreen.Core.Models;
using TeamBuildScreen.Tfs2008.Models;
using TeamBuildScreen.Core.ViewModels;
using TeamBuildScreen.Core.Views;
using TeamBuildScreen.Core;

namespace TeamBuildScreen.DesktopTfs2008
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        private void OnStartup(object sender, StartupEventArgs e)
        {
            IBuildServerService service = new BuildServerService();
            IDomainProjectPicker projectPicker = new DomainProjectPickerAdapter();

            TeamBuildDesktop teamBuildDesktop = new TeamBuildDesktop(service, projectPicker, "Team Build Screen 2008");
            teamBuildDesktop.Startup();
        }

        #endregion
    }
}