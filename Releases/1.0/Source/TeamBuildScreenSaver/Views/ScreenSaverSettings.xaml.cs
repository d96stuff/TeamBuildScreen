//-----------------------------------------------------------------------
// <copyright file="ScreenSaverSettings.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Views
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using TeamBuildScreenSaver.ViewModels;
    using TeamBuildScreenSaver.DataModels;

    #endregion

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class ScreenSaverSettings : Window
    {
        #region Constructors

        public ScreenSaverSettings(ScreenSaverSettingsDataModel dataModel)
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