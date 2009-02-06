//-----------------------------------------------------------------------
// <copyright file="ScreenSaverSettings.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Views
{
    #region Usings

    using System.Windows;
    using System.Windows.Input;
    using TeamBuildScreenSaver.Models;
    using TeamBuildScreenSaver.ViewModels;

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

        private void CloseCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();

            e.Handled = true;
        }

        #endregion
    }
}