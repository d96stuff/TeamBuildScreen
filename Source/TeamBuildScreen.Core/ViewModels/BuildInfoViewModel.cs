//-----------------------------------------------------------------------
// <copyright file="BuildDetailViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.ViewModels
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Text;
    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Core.Views;

    #endregion

    public class BuildInfoViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The model underlying the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        private BuildInfoModel dataModel;

        /// <summary>
        /// The summary for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        private string summary;

        /// <summary>
        /// The status for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        private BuildStatus? status = null;

        /// <summary>
        /// A static synchronisation lock used to synchronise access across all instances.
        /// </summary>
        private static object synchronisationLock = new object();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        public BuildStatus? Status
        {
            get
            {
                return this.status;
            }
        }

        /// <summary>
        /// Gets the summary for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        public string Summary
        {
            get
            {
                return this.summary;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the build has any builds queued.
        /// </summary>
        public bool IsQueued
        {
            get
            {
                return this.dataModel.IsQueued;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current build was completed more than a week ago.
        /// </summary>
        public bool IsStale
        {
            get
            {
                return this.dataModel.IsStale;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the build completed with warnings.
        /// </summary>
        public bool HasWarnings
        {
            get; set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildInfoViewModel"/> class. Uses
        /// <see cref="Microsoft.TeamFoundation.Build.Client.InformationNodeConverters.GetConfigurationSummary"/>
        /// to resolve the configuration summary for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        /// <param name="dataModel">The model underlying the <see cref="BuildInfoViewModel"/>.</param>
        public BuildInfoViewModel(BuildInfoModel dataModel)
        {
            this.Init(dataModel);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the <see cref="BuildInfoViewModel"/> with the specified <see cref="BuildDetailDataModel"/>.
        /// </summary>
        /// <param name="dataModel">The model that is encapsulated by the <see cref="BuildInfoViewModel"/>.</param>
        private void Init(BuildInfoModel dataModel)
        {
            this.dataModel = dataModel;
            this.dataModel.PropertyChanged += delegate(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                this.UpdateFromModel();
            };

            StringBuilder text = new StringBuilder();
            text.AppendLine(string.Format(
                "{0}: {1}",
                this.dataModel.TeamProject,
                this.dataModel.DefinitionName));
            text.Append("Loading...");

            this.summary = text.ToString();

            this.EnsureSummaryIsCorrectNumberOfLines();

            this.HasWarnings = false;
        }

        private void EnsureSummaryIsCorrectNumberOfLines()
        {
            string[] lines = this.summary.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            int difference = 6 - lines.Length;

            for (int i = 0; i < difference; i++)
            {
                this.summary = this.summary + Environment.NewLine;
            }
        }

        /// <summary>
        /// Updates the view model from the model.
        /// </summary>
        private void UpdateFromModel()
        {
            StringBuilder text = new StringBuilder();
            bool hasWarnings = false;

            text.AppendLine(string.Format(
                "{0}: {1}",
                this.dataModel.TeamProject,
                this.dataModel.DefinitionName));

            if (this.dataModel.Model != null)
            {
                this.status = this.dataModel.Model.Status;

                text.AppendLine(new BuildStatusStringConverter().Convert(this.dataModel.Model.Status, typeof(string), null, null).ToString());
                text.AppendLine("Requested by " + this.dataModel.Model.RequestedFor);
                text.Append("Started on " + this.dataModel.Model.StartTime);

                if (this.dataModel.Model.BuildFinished)
                {
                    text.AppendLine();
                    text.AppendLine("Completed on " + this.dataModel.Model.FinishTime);

                    if (this.dataModel.Model.TestsTotal.HasValue)
                    {
                        text.Append(string.Format("Test results: {0} passed, {1} failed, {2} total.", this.dataModel.Model.TestsPassed, this.dataModel.Model.TestsFailed, this.dataModel.Model.TestsTotal));
                    }
                    else
                    {
                        text.Append("No test result.");
                    }

                    hasWarnings = this.dataModel.Model.HasWarnings;
                }
            }
            else
            {
                this.status = null;

                text.Append("No build(s) found.");
            }

            this.summary = text.ToString();
            this.HasWarnings = hasWarnings;

            this.EnsureSummaryIsCorrectNumberOfLines();

            this.OnPropertyChanged("IsQueued");
            this.OnPropertyChanged("IsStale");
            this.OnPropertyChanged("Status");
            this.OnPropertyChanged("Summary");
            this.OnPropertyChanged("HasWarnings");
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