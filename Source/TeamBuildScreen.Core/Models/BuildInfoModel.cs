//-----------------------------------------------------------------------
// <copyright file="BuildDetailModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Models
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Windows.Threading;

    #endregion

    /// <summary>
    /// Represents the current details of an individual build definition.
    /// </summary>
    public class BuildInfoModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The <see cref="IBuildServerService"/> for the <see cref="BuildInfoModel"/>.
        /// </summary>
        private IBuildServerService service;

        /// <summary>
        /// The model for the <see cref="BuildInfoModel"/>.
        /// </summary>
        private IBuildInfo model;

        /// <summary>
        /// The key for the <see cref="BuildInfoModel"/>.
        /// </summary>
        private string key;

        /// <summary>
        /// The configuration for the <see cref="BuildInfoModel"/> (e.g. 'Release').
        /// </summary>
        private string configuration;

        /// <summary>
        /// The platform for the <see cref="BuildInfoModel"/> (e.g. 'Any CPU').
        /// </summary>
        private string platform;

        /// <summary>
        /// A value indicating whether the build server has further builds queueud for this definition.
        /// </summary>
        private bool isQueued = false;

        /// <summary>
        /// A value indicating whether the current build was completed more than a week ago.
        /// </summary>
        private bool isStale = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildInfoModel"/> class.
        /// </summary>
        /// <param name="teamProject">The name of the Team Project for the <see cref="BuildInfoModel"/>.</param>
        /// <param name="definitionName">The name of the build defintionf or the <see cref="BuildInfoModel"/>.</param>
        /// <param name="configuration">The configuration for the <see cref="BuildInfoModel"/> (e.g. 'Release').</param>
        /// <param name="platform">The platform for the <see cref="BuildInfoModel"/> (e.g. 'Any CPU').</param>
        /// <param name="service">The <see cref="IBuildServerService"/> for the <see cref="BuildInfoModel"/>.</param>
        public BuildInfoModel(string teamProject, string definitionName, string configuration, string platform, IBuildServerService service)
        {
            this.key = string.Format("{0};{1}", teamProject, definitionName);
            this.configuration = configuration;
            this.platform = platform;
            this.service = service;

            this.service.AddBuild(this.key);
            this.service.QueryCompleted += this.UpdateModel;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the model for the <see cref="BuildInfoModel"/>.
        /// </summary>
        public IBuildInfo Model
        {
            get
            {
                return this.model;
            }

            private set
            {
                this.model = value;

                this.OnPropertyChanged("Model");
            }
        }

        /// <summary>
        /// Gets the configuration for the <see cref="BuildInfoModel"/> (e.g. 'Release').
        /// </summary>
        public string Configuration
        {
            get
            {
                return this.configuration;
            }
        }

        /// <summary>
        /// Gets the platform for the <see cref="BuildInfoModel"/> (e.g. 'Any CPU').
        /// </summary>
        public string Platform
        {
            get
            {
                return this.platform;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the build has any builds queued.
        /// </summary>
        public bool IsQueued
        {
            get
            {
                return this.isQueued;
            }

            private set
            {
                this.isQueued = value;

                this.OnPropertyChanged("IsQueued");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current build was completed more than a week ago.
        /// </summary>
        public bool IsStale
        {
            get
            {
                return this.isStale;
            }

            private set
            {
                this.isStale = value;

                this.OnPropertyChanged("IsStale");
            }
        }

        /// <summary>
        /// Gets the team project for the build.
        /// </summary>
        public string TeamProject
        {
            get
            {
                return this.key.Split(';')[0];
            }
        }

        /// <summary>
        /// Gets the definition name for the build.
        /// </summary>
        public string DefinitionName
        {
            get
            {
                return this.key.Split(';')[1];
            }
        }

        public string Description
        {
            get
            {
                return string.IsNullOrEmpty(this.TeamProject) ? this.DefinitionName : string.Format("{0}: {1}", this.TeamProject, this.DefinitionName);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the <see cref="BuildInfoModel.Model"/> from the <see cref="IBuildServerService"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateModel(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(
                DispatcherPriority.DataBind,
                new Action(delegate
                    {
                        this.Model = this.service.GetBuildInfo(this.key, this.configuration, this.platform);
                        this.IsQueued = this.service.IsQueued(this.key);
                        this.IsStale = this.GetIsStale();
                    }));
        }

        private bool GetIsStale()
        {
            if (this.Model != null && this.Model.BuildFinished)
            {
                bool isStale = this.Model.FinishTime.HasValue && this.Model.FinishTime.Value.CompareTo(DateTime.UtcNow.AddDays(-this.service.StaleThreshold)) < 0;

                return isStale;
            }

            return false;
        }

        /// <summary>
        /// Raises the <see cref="BuildInfoModel.PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static BuildInfoModel FromString(string s, IBuildServerService service)
        {
            string[] buildParts = s.Split(';');
            string teamProject = buildParts[0];
            string definitionName = buildParts[1];
            string configuration = buildParts[2];
            string platform = buildParts[3];

            return new BuildInfoModel(teamProject, definitionName, configuration, platform, service);
        }

        #endregion
    }
}