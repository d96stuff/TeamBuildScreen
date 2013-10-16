//-----------------------------------------------------------------------
// <copyright file="BuildPanel.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Views
{
    #region Usings

    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Core.ViewModels;
    using System.ComponentModel;
    using System.Windows;

	#endregion

    public partial class BuildPanel
    {
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