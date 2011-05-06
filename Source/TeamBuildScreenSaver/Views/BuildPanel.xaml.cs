//-----------------------------------------------------------------------
// <copyright file="BuildPanel.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Views
{
    #region Usings

    using TeamBuildScreenSaver.DataModels;
    using TeamBuildScreenSaver.ViewModels;

    #endregion

    public partial class BuildPanel
    {
        #region Constructors

        public BuildPanel(BuildDetailDataModel dataModel)
		{
			this.InitializeComponent();

            this.DataContext = new BuildDetailViewModel(dataModel);
        }

        public BuildPanel(BuildDetailDataModel dataModel, ConfigurationSummaryHandler configurationSummaryHandler)
        {
            this.InitializeComponent();

            this.DataContext = new BuildDetailViewModel(dataModel, configurationSummaryHandler);
        }

        #endregion
    }
}