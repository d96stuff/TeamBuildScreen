//-----------------------------------------------------------------------
// <copyright file="BuildDetailModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Models
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Windows.Threading;
    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    /// <summary>
    /// Represents the current details of an individual build definition.
    /// </summary>
    public class BuildDetailModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The <see cref="IBuildServerService"/> for the <see cref="BuildDetailModel"/>.
        /// </summary>
        private IBuildServerService service;

        /// <summary>
        /// The model for the <see cref="BuildDetailModel"/>.
        /// </summary>
        private IBuildDetail model;

        /// <summary>
        /// The key for the <see cref="BuildDetailModel"/>.
        /// </summary>
        private string key;

        /// <summary>
        /// The configuration for the <see cref="BuildDetailModel"/> (e.g. 'Release').
        /// </summary>
        private string configuration;

        /// <summary>
        /// The platform for the <see cref="BuildDetailModel"/> (e.g. 'Any CPU').
        /// </summary>
        private string platform;

        private bool isQueued = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildDetailModel"/> class.
        /// </summary>
        /// <param name="teamProject">The name of the Team Project for the <see cref="BuildDetailModel"/>.</param>
        /// <param name="definitionName">The name of the build defintionf or the <see cref="BuildDetailModel"/>.</param>
        /// <param name="configuration">The configuration for the <see cref="BuildDetailModel"/> (e.g. 'Release').</param>
        /// <param name="platform">The platform for the <see cref="BuildDetailModel"/> (e.g. 'Any CPU').</param>
        /// <param name="service">The <see cref="IBuildServerService"/> for the <see cref="BuildDetailModel"/>.</param>
        public BuildDetailModel(string teamProject, string definitionName, string configuration, string platform, IBuildServerService service)
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
        /// Gets the model for the <see cref="BuildDetailModel"/>.
        /// </summary>
        public IBuildDetail Model
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
        /// Gets the configuration for the <see cref="BuildDetailModel"/> (e.g. 'Release').
        /// </summary>
        public string Configuration
        {
            get
            {
                return this.configuration;
            }
        }

        /// <summary>
        /// Gets the platform for the <see cref="BuildDetailModel"/> (e.g. 'Any CPU').
        /// </summary>
        public string Platform
        {
            get
            {
                return this.platform;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the build has any builds queued.
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

        #endregion

        #region Methods

        /// <summary>
        /// Updates the <see cref="BuildDetailModel.Model"/> from the <see cref="IBuildServerService"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateModel(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(
                DispatcherPriority.DataBind,
                new Action(delegate
                    {
                        IBuildDetail model = this.service.GetBuildDetail(this.key);

                        if (model != null)
                        {
                            this.Model = model;
                        }

                        this.IsQueued = this.service.IsQueued(this.key);
                    }));
        }

        /// <summary>
        /// Raises the <see cref="BuildDetailModel.PropertyChanged"/> event.
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
    }
}