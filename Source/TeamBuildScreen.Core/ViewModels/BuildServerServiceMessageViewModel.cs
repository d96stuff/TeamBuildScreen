//-----------------------------------------------------------------------
// <copyright file="BuildServerServiceMessageViewModel.cs" company="Jim Liddell">
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

    public class BuildServerServiceMessageViewModel : IMessageViewModel, INotifyPropertyChanged
    {
        #region Constuctors

        public BuildServerServiceMessageViewModel(IBuildServerService service)
        {
            service.QueryCompleted += OnQueryCompleted;
            service.Error += OnError;
            service.NotConfigured += OnNotConfigured;
            service.Stopped += OnStopped;
        }

        #endregion

        #region Methods

        private void OnQueryCompleted(object sender, EventArgs e)
        {
            this.IsVisible = false;
            this.OnPropertyChanged("IsVisible");
        }

        private void OnError(object sender, StringEventArgs e)
        {
			this.DisplayMessage(string.Format("Unable to contact build server.\nLast connection {0}", e.Value));
        }

        private void OnNotConfigured(object sender, EventArgs e)
        {
            this.DisplayMessage("No builds have been configured.");
        }

        private void OnStopped(object sender, EventArgs e)
        {
            this.DisplayMessage(string.Empty);
        }

        private void DisplayMessage(string message)
        {
            this.Message = message;
            this.IsVisible = true;
            this.OnPropertyChanged("Message");
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