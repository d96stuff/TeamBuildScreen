//-----------------------------------------------------------------------
// <copyright file="BuildSetting.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace TeamBuildScreen.Core.Models
{
    #region Usings

    using System.ComponentModel;

    #endregion

	public class BuildSetting : INotifyPropertyChanged, IComparable<BuildSetting>, IEquatable<BuildSetting>
    {
        #region Fields

        private string platform;
        private string configuration;
        private string teamProject;
        private string definitionName;
        private bool isEnabled;
	    private int orderNo;

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

				OrderNo = value ? Int32.MaxValue - 1 : Int32.MaxValue;

                this.OnPropertyChanged("IsEnabled");
            }
        }

		public int OrderNo
		{
			get
			{
				return this.orderNo;
			}

			set
			{
				this.orderNo = value;

				this.OnPropertyChanged("OrderNo");
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

		public int CompareTo(BuildSetting other)
		{
			if (this.OrderNo == other.OrderNo && this.TeamProject == other.TeamProject)
				return String.Compare(this.DefinitionName, other.DefinitionName, StringComparison.Ordinal);
			if (this.OrderNo == other.OrderNo)
				return String.Compare(this.TeamProject, other.TeamProject, StringComparison.Ordinal);
			return this.OrderNo.CompareTo(other.OrderNo);
		}

		public bool Equals(BuildSetting other)
		{
			if (this.DefinitionName.Equals(other.DefinitionName) && this.TeamProject.Equals(other.TeamProject))
				return true;
			return false;
		}
    }
}