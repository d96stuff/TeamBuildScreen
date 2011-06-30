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
    using TeamBuildScreen.Core.Models;

    #endregion

    public class BuildInfoViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The model underlying the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        private BuildInfoModel dataModel;

        /// <summary>
        /// The description for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        private string description;

        /// <summary>
        /// The name of the person who requested the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        private string requestedBy;

        /// <summary>
        /// The time when the build was started.
        /// </summary>
        private DateTime? startedOn;

        /// <summary>
        /// The time the build was completed.
        /// </summary>
        private DateTime? completedOn;

        /// <summary>
        /// The test results for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        private string testResults;

        /// <summary>
        /// The status for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        private BuildStatus status;

        /// <summary>
        /// A static synchronisation lock used to synchronise access across all instances.
        /// </summary>
        private static object synchronisationLock = new object();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        public BuildStatus Status
        {
            get
            {
                return this.status;
            }
        }

        /// <summary>
        /// Gets the description for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets the name of the person who requested the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        public string RequestedBy
        {
            get
            {
                return this.requestedBy;
            }
        }

        /// <summary>
        /// Gets the time when the build was started.
        /// </summary>
        public DateTime? StartedOn
        {
            get
            {
                return this.startedOn;
            }
        }

        /// <summary>
        /// Gets the time the build was completed.
        /// </summary>
        public DateTime? CompletedOn
        {
            get
            {
                return this.completedOn;
            }
        }

        /// <summary>
        /// Gets the test results for the <see cref="BuildInfoViewModel"/>.
        /// </summary>
        public string TestResults
        {
            get
            {
                return this.testResults;
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

            this.description = this.dataModel.Description;
            this.status = BuildStatus.Loading;

            this.HasWarnings = false;
        }

        /// <summary>
        /// Updates the view model from the model.
        /// </summary>
        private void UpdateFromModel()
        {
            bool hasWarnings = false;

            this.description = this.dataModel.Description;
            this.status = BuildStatus.NoneFound;
            this.requestedBy = null;
            this.startedOn = null;
            this.completedOn = null;
            this.testResults = null;

            if (this.dataModel.Model != null)
            {
                this.status = this.dataModel.Model.Status;
                this.requestedBy = this.dataModel.Model.RequestedFor;

                // remove domain prefix from requestedBy, if found
                if (!string.IsNullOrEmpty(this.requestedBy))
                {
                    this.requestedBy = this.requestedBy.Contains("\\")
                                           ? this.requestedBy.Split('\\')[1]
                                           : this.requestedBy;
                }
                this.startedOn = this.dataModel.Model.StartTime;

                if (this.dataModel.Model.BuildFinished)
                {
                    this.completedOn = this.dataModel.Model.FinishTime;

                    if (this.dataModel.Model.TestsTotal.HasValue)
                    {
                        this.testResults = string.Format("Test results: {0} passed, {1} failed, {2} total.", this.dataModel.Model.TestsPassed, this.dataModel.Model.TestsFailed, this.dataModel.Model.TestsTotal);
                    }
                    else
                    {
                        this.testResults = "No test result.";
                    }

                    hasWarnings = this.dataModel.Model.HasWarnings;
                }
            }

            this.HasWarnings = hasWarnings;

            this.OnPropertyChanged("IsQueued");
            this.OnPropertyChanged("IsStale");
            this.OnPropertyChanged("Status");
            this.OnPropertyChanged("Description");
            this.OnPropertyChanged("RequestedBy");
            this.OnPropertyChanged("StartedOn");
            this.OnPropertyChanged("CompletedOn");
            this.OnPropertyChanged("TestResults");
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