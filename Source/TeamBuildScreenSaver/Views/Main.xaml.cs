//-----------------------------------------------------------------------
// <copyright file="Main.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Views
{
    #region Usings

    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using TeamBuildScreenSaver.Models;
    using TeamBuildScreenSaver.ViewModels;

    #endregion

    public partial class Main
    {
        #region Fields

        private bool isInteractive = true;

        #endregion

        #region Properties

        public bool IsInteractive
        {
            set
            {
                this.isInteractive = value;
            }
        }

        #endregion

        #region Constructors

        public Main()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: This should be encapsulted as a Command on MainViewModel
            if (this.isInteractive)
            {
                Application.Current.Shutdown();
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: This should be encapsulted as a Command on MainViewModel
            if (this.isInteractive)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion
    }
}