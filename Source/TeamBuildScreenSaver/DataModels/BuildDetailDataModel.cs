//-----------------------------------------------------------------------
// <copyright file="BuildDetailDataModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.DataModels
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
    public class BuildDetailDataModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The <see cref="IBuildServerQuery"/> for the <see cref="BuildDetailDataModel"/>.
        /// </summary>
        private IBuildServerQuery serverQuery;

        /// <summary>
        /// The model for the <see cref="BuildDetailDataModel"/>.
        /// </summary>
        private IBuildDetail model;

        /// <summary>
        /// The key for the <see cref="BuildDetailDataModel"/>.
        /// </summary>
        private string key;

        /// <summary>
        /// The configuration for the <see cref="BuildDetailDataModel"/> (e.g. 'Release').
        /// </summary>
        private string configuration;

        /// <summary>
        /// The platform for the <see cref="BuildDetailDataModel"/> (e.g. 'Any CPU').
        /// </summary>
        private string platform;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildDetailDataModel"/> class.
        /// </summary>
        /// <param name="teamProject">The name of the Team Project for the <see cref="BuildDetailDataModel"/>.</param>
        /// <param name="definitionName">The name of the build defintionf or the <see cref="BuildDetailDataModel"/>.</param>
        /// <param name="configuration">The configuration for the <see cref="BuildDetailDataModel"/> (e.g. 'Release').</param>
        /// <param name="platform">The platform for the <see cref="BuildDetailDataModel"/> (e.g. 'Any CPU').</param>
        /// <param name="serverQuery">The <see cref="IBuildServerQuery"/> for the <see cref="BuildDetailDataModel"/>.</param>
        public BuildDetailDataModel(string teamProject, string definitionName, string configuration, string platform, IBuildServerQuery serverQuery)
        {
            this.key = string.Format("{0};{1}", teamProject, definitionName);
            this.configuration = configuration;
            this.platform = platform;
            this.serverQuery = serverQuery;

            this.serverQuery.AddBuild(this.key);
            this.serverQuery.QueryCompleted += this.UpdateModel;
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
        /// Gets the model for the <see cref="BuildDetailDataModel"/>.
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
        /// Gets the configuration for the <see cref="BuildDetailDataModel"/> (e.g. 'Release').
        /// </summary>
        public string Configuration
        {
            get
            {
                return this.configuration;
            }
        }

        /// <summary>
        /// Gets the platform for the <see cref="BuildDetailDataModel"/> (e.g. 'Any CPU').
        /// </summary>
        public string Platform
        {
            get
            {
                return this.platform;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the <see cref="BuildDetailDataModel.Model"/> from the <see cref="IBuildServerQuery"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateModel(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(
                DispatcherPriority.DataBind,
                new Action(delegate
                    {
                        IBuildDetail model = this.serverQuery[this.key];

                        if (model != null)
                        {
                            this.Model = model;
                        }
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