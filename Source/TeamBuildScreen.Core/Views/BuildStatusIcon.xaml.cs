//-----------------------------------------------------------------------
// <copyright file="BuildStatusIcon.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Views
{
    #region Usings

    using System.Windows;
    using System.Windows.Controls;
    using System.ComponentModel;
    using TeamBuildScreen.Core.Models;

    #endregion

    public partial class BuildStatusIcon : UserControl
    {
        #region Statics

        public static DependencyProperty StatusProperty =
         DependencyProperty.Register("Status", typeof(BuildStatus), typeof(BuildStatusIcon));

        #endregion

        #region Properties

        public BuildStatus Status
        {
            get { return (BuildStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        #endregion

        #region Constructors

        public BuildStatusIcon()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}