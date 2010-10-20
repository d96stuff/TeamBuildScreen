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
            StringCollection builds = new StringCollection();
            builds.Add("TeamProject;MyFirstBuild;Release;Any CPU");
            builds.Add("TeamProject;MySecondBuild;Release;Any CPU");

            MainViewModel viewModel = new MainViewModel(service, builds);
            viewModel.Columns =1;
            viewModel.CloseOnClicked = true;

            Main main = new Main();
            main.DataContext = viewModel;
            main.Show();
            service.Start();
        }

        #endregion
    }
}