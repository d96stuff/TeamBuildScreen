//-----------------------------------------------------------------------
// <copyright file="BuildSetting.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.DataModels
{
    #region Usings

    using System.ComponentModel;

    #endregion

    public class BuildSetting : INotifyPropertyChanged
    {
        #region Fields

        private string platform;
        private string configuration;
        private string teamProject;
        private string definitionName;
        private bool isEnabled;

        #endregion

        #region Properties

        public string Platform
        {
            get
            {
                return this.platform;
            }
            set
            {
                this.platform = value;

                this.OnPropertyChanged("Platform");
            }
        }

        public string Configuration
        {
            get
            {
                return this.configuration;
            }
            set
            {
                this.configuration = value;

                this.OnPropertyChanged("Configuration");
            }
        }

        public string TeamProject
        {
            get
            {
                return this.teamProject;
            }
            set
            {
                this.teamProject = value;

                this.OnPropertyChanged("TeamProject");
            }
        }

        public string DefinitionName
        {
            get
            {
                return this.definitionName;
            }
            set
            {
                this.definitionName = value;

                this.OnPropertyChanged("DefinitionName");
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.isEnabled = value;

                this.OnPropertyChanged("IsEnabled");
            }
        }

        #endregion

        #region Methods

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