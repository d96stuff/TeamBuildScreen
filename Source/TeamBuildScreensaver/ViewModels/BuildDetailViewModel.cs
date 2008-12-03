//-----------------------------------------------------------------------
// <copyright file="BuildDetailsViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreensaver.ViewModels
{
    #region Usings

    using System.ComponentModel;
    using System.Text;
    using Microsoft.TeamFoundation.Build.Client;
    using TeamBuildScreensaver.DataModels;
    using TeamBuildScreensaver.Views;
    using System.Collections.Generic;

    #endregion

    public class BuildDetailViewModel : INotifyPropertyChanged
    {
        #region Fields

        private BuildDetailDataModel dataModel;
        private string summary;
        private BuildStatus status;
        private ConfigurationSummaryHandler configurationSummaryHandler;

        #endregion

        #region Properties

        public BuildStatus Status
        {
            get
            {
                return this.status;
            }
        }

        public string Summary
        {
            get
            {
                return this.summary;
            }
        }

        public ConfigurationSummaryHandler ConfigurationSummaryHandler
        {
            set
            {
                this.configurationSummaryHandler = value;
            }
        }

        #endregion

        #region Constructors

        public BuildDetailViewModel(BuildDetailDataModel dataModel)
        {
            this.Init(dataModel);
            this.configurationSummaryHandler = InformationNodeConverters.GetConfigurationSummary;
        }

        public BuildDetailViewModel(BuildDetailDataModel dataModel, ConfigurationSummaryHandler configurationSummaryHandler)
        {
            this.Init(dataModel);
            this.configurationSummaryHandler = configurationSummaryHandler;
        }

        #endregion

        #region Methods

        private void Init(BuildDetailDataModel dataModel)
        {
            this.dataModel = dataModel;
            this.dataModel.PropertyChanged += dataModel_PropertyChanged;
        }

        private void dataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.UpdateFromModel();
        }

        private void UpdateFromModel()
        {
            this.status = this.dataModel.Model.Status;

            StringBuilder text = new StringBuilder();

            text.AppendLine(string.Format(
                "{0}.{1}",
                this.dataModel.Model.BuildDefinition.TeamProject,
                this.dataModel.Model.BuildDefinition.Name));
            text.AppendLine(new BuildStatusStringConverter().Convert(this.dataModel.Model.Status, typeof(string), null, null).ToString());
            text.AppendLine("Requested by " + this.dataModel.Model.RequestedFor);
            text.AppendLine("Started on " + this.dataModel.Model.StartTime);

            if (this.dataModel.Model.BuildFinished)
            {
                text.AppendLine("Completed on " + this.dataModel.Model.FinishTime);

                //IConfigurationSummary configurationSummary = this.configurationSummaryHandler(this.dataModel.Model, "Release", "Any CPU");
                IList<IConfigurationSummary> configurationSummaries = InformationNodeConverters.GetConfigurationSummaries(this.dataModel.Model);

                if (configurationSummaries != null &&
                    configurationSummaries.Count > 0 &&
                    configurationSummaries[0].TestSummaries.Count > 0)
                {
                    ITestSummary summary = configurationSummaries[0].TestSummaries[0];
                    text.AppendLine(string.Format("Test results: {0} passed, {1} failed, {2} total.", summary.TestsPassed, summary.TestsFailed, summary.TestsTotal));
                }
                else
                {
                    text.AppendLine("No test result.");
                }
            }

            this.summary = text.ToString();

            this.OnPropertyChanged("Status");
            this.OnPropertyChanged("Summary");
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}