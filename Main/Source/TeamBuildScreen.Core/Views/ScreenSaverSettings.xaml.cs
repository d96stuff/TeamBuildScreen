//-----------------------------------------------------------------------
// <copyright file="ScreenSaverSettings.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Views
{
    #region Usings

    using System.Windows;

    #endregion

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class ScreenSaverSettings : Window
    {
        #region Constructors

        public ScreenSaverSettings(object viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }

        #endregion
    }
}