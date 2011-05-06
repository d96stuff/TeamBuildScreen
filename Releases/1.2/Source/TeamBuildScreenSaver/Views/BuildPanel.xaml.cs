//-----------------------------------------------------------------------
// <copyright file="BuildPanel.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Views
{
    #region Usings

    using TeamBuildScreenSaver.Models;
    using TeamBuildScreenSaver.ViewModels;
    using System.ComponentModel;
    using System.Windows;
using System.Windows.Controls;

    #endregion

    public partial class BuildPanel
    {
        private Border border;
        private BuildStatusIcon statusIcon;

        #region Constructors

        public BuildPanel()
		{
			this.InitializeComponent();

            bool isDesignTime = (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;

            if (isDesignTime)
            {
                // TODO: Set DataContext to mock object at design time
            }
        }

        #endregion
    }
}