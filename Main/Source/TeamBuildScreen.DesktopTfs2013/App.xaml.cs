﻿using System.Windows;
using TeamBuildScreen.Core.Models;
using TeamBuildScreen.Tfs2013.Models;
using TeamBuildScreen.Core;

namespace TeamBuildScreen.DesktopTfs2013
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
            IDomainProjectPicker projectPicker = new TeamProjectPickerAdapter();

            TeamBuildDesktop teamBuildDesktop = new TeamBuildDesktop(service, projectPicker, "Team Build Screen 2013");
            teamBuildDesktop.Startup();
        }

        #endregion
    }
}