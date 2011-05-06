//-----------------------------------------------------------------------
// <copyright file="Main.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Views
{
    using System;
    using System.Linq;
    using System.Windows;

    public partial class Main
    {
        #region Constructors

        public Main()
        {
            this.InitializeComponent();

            this.Loaded += delegate
                {
                    // in WPF - setting WindowState to Maximised before the Window is loaded results in 
                    // it always being maximised to the primary display, even if it has been positioned on 
                    // a secondary display - the workaround is to maximise it only once it has been loaded
                    this.WindowState = WindowState.Maximized;
                };
        }

        #endregion
    }
}