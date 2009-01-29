//-----------------------------------------------------------------------
// <copyright file="BuildServerQueryMessageViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.ViewModels
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TeamBuildScreenSaver.DataModels;
    using System.ComponentModel;

    #endregion

    public class BuildServerQueryMessageViewModel : IMessageViewModel, INotifyPropertyChanged
    {
        #region Constuctors

        public BuildServerQueryMessageViewModel(IBuildServerQuery serverQuery)
        {
            this.Message = "Unable to contact Team Foundation Server.";
            serverQuery.QueryCompleted += OnQueryCompleted;
            serverQuery.Error += OnError;
        }

        #endregion

        #region Methods

        private void OnQueryCompleted(object sender, EventArgs e)
        {
            this.IsVisible = false;
            this.OnPropertyChanged("IsVisible");
        }

        private void OnError(object sender, EventArgs e)
        {
            this.IsVisible = true;
            this.OnPropertyChanged("IsVisible");
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

        #region IMessageViewModel Members

        public string Message
        {
            get;
            private set;
        }

        public bool IsVisible
        {
            get;
            private set;
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