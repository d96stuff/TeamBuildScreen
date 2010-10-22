//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.DesktopDemo
{
    #region Usings

    using System;
    using System.Collections.Specialized;
    using System.Windows;
    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Core.Views;
    using TeamBuildScreen.Core.ViewModels;
    using TeamBuildScreen.Core;
    using TeamBuildScreen.Demo;

    #endregion

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        private void OnStartup(object sender, StartupEventArgs e)
        {
            MockBuildServerService service = new MockBuildServerService();
            IDomainProjectPicker projectPicker = new MockDomainProjectPicker();

            TeamBuildDesktop teamBuildDesktop = new TeamBuildDesktop(service, projectPicker, "Team Build Screen Demo");
            teamBuildDesktop.Startup(e.Args);
        }

        #endregion
    }
}