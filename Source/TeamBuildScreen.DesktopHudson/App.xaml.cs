//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.DesktopHudson
{
    #region Usings

    using System.Windows;
    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Core;
    using TeamBuildScreen.Hudson.Models;

    #endregion

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        private void OnStartup(object sender, StartupEventArgs e)
        {
            HudsonBuildServerService service = new HudsonBuildServerService();
            IDomainProjectPicker projectPicker = new HudsonDomainProjectPicker();

            TeamBuildDesktop teamBuildDesktop = new TeamBuildDesktop(service, projectPicker, "Team Build Screen Hudson");
            teamBuildDesktop.Startup();
        }

        #endregion
    }
}