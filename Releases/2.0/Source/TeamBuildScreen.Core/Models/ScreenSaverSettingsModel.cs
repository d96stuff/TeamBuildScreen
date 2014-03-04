//-----------------------------------------------------------------------
// <copyright file="ScreenSaverSettingsModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Models
{
    #region Usings

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System;

    #endregion

    /// <summary>
    /// Provides ability to read and modify application settings.
    /// </summary>
    public class ScreenSaverSettingsModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Application settings collection.
        /// </summary>
        private Settings settings;

        /// <summary>
        /// Collection of <see cref="BuildSetting"/> objects that represent the builds currently configured.
        /// </summary>
        private ObservableCollection<BuildSetting> builds;

        /// <summary>
        /// The URL of the Team Foundation Server.
        /// </summary>
        private string tfsUri;

        /// <summary>
        /// The number of columns to display.
        /// </summary>
        private int columns;

        /// <summary>
        /// The interval at which the build server will be queried.
        /// </summary>
        private int updateInterval;

        /// <summary>
        /// The interval at which the build server will be queried.
        /// </summary>
        private int staleThreshold;

	    /// <summary>
	    /// Specifies the format of the Team Project Name in the build info.
	    /// </summary>
		private TeamProjectNameFormat teamProjectNameFormat;

        private IBuildServerService buildServerService;

	    #endregion

        #region Properties

        public string TfsUri
        {
            get
            {
                return this.tfsUri;
            }

            set
            {
                if (value != this.tfsUri)
                {
                    this.tfsUri = value;

                    this.OnPropertyChanged("TfsUri");
                }
            }
        }

        public int Columns
        {
            get
            {
                return this.columns;
            }

            set
            {
                if (value != this.columns)
                {
                    this.columns = value;

                    this.OnPropertyChanged("Columns");
                }
            }
        }

        public int UpdateInterval
        {
            get
            {
                return this.updateInterval;
            }

            set
            {
                if (value != this.updateInterval)
                {
                    this.updateInterval = value;

                    this.OnPropertyChanged("UpdateInterval");
                }
            }
        }

        public int StaleThreshold
        {
            get
            {
                return this.staleThreshold;
            }

            set
            {
                if (value != this.staleThreshold)
                {
                    this.staleThreshold = value;

                    this.OnPropertyChanged("StaleThreshold");
                }
            }
        }

		public TeamProjectNameFormat CurrentTeamProjectNameFormat
	    {
		    get
			{
				return this.teamProjectNameFormat;
			}
			set
			{
				if (value != this.teamProjectNameFormat)
				{
					this.teamProjectNameFormat = value;

					this.OnPropertyChanged("CurrentTeamProjectNameFormat");
				}
			}
	    }

        public ObservableCollection<BuildSetting> Builds
        {
            get
            {
                return this.builds;
            }
        }

        public IDomainProjectPicker ProjectPicker
        {
            get;
            private set;
        }

	    #endregion

        #region Constructors

        public ScreenSaverSettingsModel(IBuildServerService buildServerService, IDomainProjectPicker projectPicker)
        {
            this.buildServerService = buildServerService;
            this.ProjectPicker = projectPicker;
            this.settings = Settings.Default;
            this.builds = new ObservableCollection<BuildSetting>();

            this.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("TfsUri"))
                {
                    this.LoadBuilds(this.builds);
                }
            };

            this.Load();
        }
        
        #endregion

        #region Methods

        public void Save()
        {
            this.settings.TfsUri = this.tfsUri;
            this.settings.UpdateInterval = this.updateInterval;
            this.settings.StaleThreshold = this.staleThreshold;
	        this.settings.TeamProjectNameFormat = (int)this.teamProjectNameFormat;

            this.SaveBuilds();

            // don't allow columns to be greater than the number of builds
            this.settings.Columns = Math.Min(this.columns, this.settings.Builds.Count);

            this.settings.Save();
        }

        public void Load()
        {
            this.tfsUri = this.settings.TfsUri;
            this.columns = this.settings.Columns;
            this.updateInterval = this.settings.UpdateInterval;
            this.staleThreshold = this.settings.StaleThreshold;
	        this.teamProjectNameFormat = (TeamProjectNameFormat)this.settings.TeamProjectNameFormat;

            if (!string.IsNullOrEmpty(this.TfsUri))
            {
                this.LoadBuilds(this.builds);
            }
        }

        private void SaveBuilds()
        {
            this.settings.Builds.Clear();

            foreach (BuildSetting build in this.builds)
            {
                if (build.IsEnabled)
                {
                    this.settings.Builds.Add(
                        string.Format("{0};{1};{2};{3}",
                        build.TeamProject,
                        build.DefinitionName,
                        build.Configuration,
                        build.Platform));
                }
            }
        }

        /// <summary>
        /// Loads the available builds from the build server.
        /// </summary>
        /// <param name="builds">The collection to populate.</param>
        private void LoadBuilds(ICollection<BuildSetting> builds)
        {
            this.buildServerService.TfsUrl = this.tfsUri;
            this.buildServerService.LoadBuilds(builds);

            foreach (BuildSetting buildSetting in builds)
            {
                bool found = false;

                foreach (string build in this.settings.Builds)
                {
                    string[] buildParts = build.Split(';');
                    string teamProject = buildParts[0];
                    string definitionName = buildParts[1];
                    string configuration = buildParts[2];
                    string platform = buildParts[3];

                    if (definitionName.Equals(buildSetting.DefinitionName) &&
                        ((string.IsNullOrEmpty(teamProject) && string.IsNullOrEmpty(buildSetting.TeamProject)) || (teamProject != null && teamProject.Equals(buildSetting.TeamProject))))
                    {
                        buildSetting.Configuration = configuration;
                        buildSetting.Platform = platform;
                        buildSetting.IsEnabled = true;

                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    buildSetting.Configuration = "Release";
                    buildSetting.Platform = "Any CPU";
                    buildSetting.IsEnabled = false;
                }
            }

            this.OnPropertyChanged("Builds");
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