//-----------------------------------------------------------------------
// <copyright file="ScreenSaverSettingsModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Models
{
    #region Usings

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;

    #endregion

    public class ScreenSaverSettingsModel : INotifyPropertyChanged
    {
        #region Fields

        private Settings settings;
        private ObservableCollection<BuildSetting> builds;
        private string tfsUri;
        private int columns;
        private int updateInterval;

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

        public ObservableCollection<BuildSetting> Builds
        {
            get
            {
                return this.builds;
            }
        }

        #endregion

        #region Constructors

        public ScreenSaverSettingsModel()
        {
            this.settings = Settings.Default;
            this.builds = new ObservableCollection<BuildSetting>();

            this.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals("TfsUri"))
                {
                    this.LoadBuilds(this.builds, this.TfsUri);
                }
            };

            this.Load();
        }
        
        #endregion

        #region Methods

        public void Save()
        {
            this.settings.TfsUri = this.tfsUri;
            this.settings.Columns = this.columns;
            this.settings.UpdateInterval = this.updateInterval;

            this.SaveBuilds();

            this.settings.Save();
        }

        public void Load()
        {
            this.tfsUri = this.settings.TfsUri;
            this.columns = this.settings.Columns;
            this.updateInterval = this.settings.UpdateInterval;

            this.LoadBuilds(this.builds, this.TfsUri);
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

        private void LoadBuilds(ICollection<BuildSetting> builds, string tfsUri)
        {
            if (!string.IsNullOrEmpty(tfsUri))
            {
                builds.Clear();

                TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer(tfsUri);

                IBuildServer buildServer = (IBuildServer)tfs.GetService(typeof(IBuildServer));
                VersionControlServer versionControlServer = (VersionControlServer)tfs.GetService(typeof(VersionControlServer));

                TeamProject[] teamProjects = versionControlServer.GetAllTeamProjects(true);

                foreach (TeamProject project in teamProjects)
                {
                    IBuildDefinition[] projectBuilds = buildServer.QueryBuildDefinitions(project.Name);

                    foreach (IBuildDefinition definition in projectBuilds)
                    {
                        BuildSetting buildSettingDataModel = new BuildSetting()
                        {
                            DefinitionName = definition.Name,
                            TeamProject = project.Name
                        };

                        bool found = false;

                        foreach (string build in this.settings.Builds)
                        {
                            string[] buildParts = build.Split(';');
                            string teamProject = buildParts[0];
                            string definitionName = buildParts[1];
                            string configuration = buildParts[2];
                            string platform = buildParts[3];

                            if (definitionName.Equals(buildSettingDataModel.DefinitionName) &&
                                teamProject.Equals(buildSettingDataModel.TeamProject))
                            {
                                buildSettingDataModel.Configuration = configuration;
                                buildSettingDataModel.Platform = platform;
                                buildSettingDataModel.IsEnabled = true;

                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            buildSettingDataModel.Configuration = "Release";
                            buildSettingDataModel.Platform = "Any CPU";
                            buildSettingDataModel.IsEnabled = false;
                        }

                        builds.Add(buildSettingDataModel);
                    }
                }

                this.OnPropertyChanged("Builds");
            }
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