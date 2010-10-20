//-----------------------------------------------------------------------
// <copyright file="ScreenSaverSettings.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Views
{
    #region Usings

    using System.Windows;
    using System.Windows.Input;
    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Core.ViewModels;

    #endregion

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class ScreenSaverSettings : Window
    {
        #region Constructors

        public ScreenSaverSettings(ScreenSaverSettingsModel dataModel)
        {
            InitializeComponent();

            this.DataContext = new ScreenSaverSettingsViewModel(dataModel);
        }

        #endregion
    }
}