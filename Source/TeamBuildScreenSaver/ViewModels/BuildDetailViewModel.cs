//-----------------------------------------------------------------------
// <copyright file="BuildDetailViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.ViewModels
{
    #region Usings

    using System.ComponentModel;
    using System.Text;
    using Microsoft.TeamFoundation.Build.Client;
    using TeamBuildScreenSaver.DataModels;
    using TeamBuildScreenSaver.Views;

    #endregion

    public class BuildDetailViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The model underlying the <see cref="BuildDetailViewModel"/>.
        /// </summary>
        private BuildDetailDataModel dataModel;

        /// <summary>
        /// The summary for the <see cref="BuildDetailViewModel"/>.
        /// </summary>
        private string summary;

        /// <summary>
        /// The status for the <see cref="BuildDetailViewModel"/>.
        /// </summary>
        private BuildStatus status;

        private ConfigurationSummaryHandler configurationSummaryHandler;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status for the <see cref="BuildDetailViewModel"/>.
        /// </summary>
        public BuildStatus Status
        {
            get
            {
                return this.status;
            }
        }

        /// <summary>
        /// Gets the summary for the <see cref="BuildDetailViewModel"/>.
        /// </summary>
        public string Summary
        {
            get
            {
                return this.summary;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildDetailViewModel"/> class. Uses
        /// <see cref="Microsoft.TeamFoundation.Build.Client.InformationNodeConverters.GetConfigurationSummary"/>
        /// to resolve the configuration summary for the <see cref="BuildDetailViewModel"/>.
        /// </summary>
        /// <param name="dataModel">The model underlying the <see cref="BuildDetailViewModel"/>.</param>
        public BuildDetailViewModel(BuildDetailDataModel dataModel)
        {
            this.Init(dataModel);
            this.configurationSummaryHandler = InformationNodeConverters.GetConfigurationSummary;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildDetailViewModel"/> class.
        /// </summary>
        /// <param name="dataModel">The model underlying the <see cref="BuildDetailViewModel"/>.</param>
        /// <param name="configurationSummaryHandler">The delegate that resolves the configuration summary for the <see cref="BuildDetailViewModel"/>.</param>
        public BuildDetailViewModel(BuildDetailDataModel dataModel, ConfigurationSummaryHandler configurationSummaryHandler)
        {
            this.Init(dataModel);
            this.configurationSummaryHandler = configurationSummaryHandler;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the <see cref="BuildDetailViewModel"/> with the specified <see cref="BuildDetailDataModel"/>.
        /// </summary>
        /// <param name="dataModel">The model that is encapsulated by the <see cref="BuildDetailViewModel"/>.</param>
        private void Init(BuildDetailDataModel dataModel)
        {
            this.dataModel = dataModel;
            this.dataModel.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                this.UpdateFromModel();
            };
        }

        /// <summary>
        /// Updates the view model from the model.
        /// </summary>
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

                IConfigurationSummary configurationSummary = this.configurationSummaryHandler(this.dataModel.Model, this.dataModel.Configuration, this.dataModel.Platform);

                if (configurationSummary != null &&
                    configurationSummary.TestSummaries.Count > 0)
                {
                    ITestSummary summary = configurationSummary.TestSummaries[0];
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

        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}