//-----------------------------------------------------------------------
// <copyright file="ScreenSaverSettingsViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.ViewModels
{
    #region Usings

    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using TeamBuildScreen.Core.Models;

    #endregion

    public class ScreenSaverSettingsViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ScreenSaverSettingsModel dataModel;

        #endregion

        #region Constructors

        public ScreenSaverSettingsViewModel(ScreenSaverSettingsModel dataModel)
        {
            this.dataModel = dataModel;

            this.dataModel.PropertyChanged += this.OnPropertyChanged;

            this.RegisterCommands();
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public ICommand RequestSave
        {
            get;
            set;
        }

        public ICommand RequestClose
        {
            get;
            set;
        }

        public ICommand RequestSelectServer
        {
            get;
            set;
        }

        public string TfsUri
        {
            get
            {
                return this.dataModel.TfsUri;
            }

            set
            {
                this.dataModel.TfsUri = value;
            }
        }

        public int Columns
        {
            get
            {
                return this.dataModel.Columns;
            }

            set
            {
                this.dataModel.Columns = value;
            }
        }

        public int UpdateInterval
        {
            get
            {
                return this.dataModel.UpdateInterval;
            }

            set
            {
                this.dataModel.UpdateInterval = value;
            }
        }

        public int StaleThreshold
        {
            get
            {
                return this.dataModel.StaleThreshold;
            }

            set
            {
                this.dataModel.StaleThreshold = value;
            }
        }

        public ObservableCollection<BuildSetting> Builds
        {
            get
            {
                return this.dataModel.Builds;
            }
        }

        #endregion

        #region Methods

        private void RegisterCommands()
        {
            this.RequestSelectServer = new RelayCommand(x => this.RequestSelectServerExecuted(x), x => this.CommandCanExecute(x));
            this.RequestSave = new RelayCommand(x => this.RequestSaveExecuted(x), x => this.CommandCanExecute(x));
            this.RequestClose = new RelayCommand(x => this.RequestCloseExecuted(x), x => this.CommandCanExecute(x));
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

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e.PropertyName);
        }

        private bool CommandCanExecute(object sender)
        {
            return true;
        }

        private void RequestSelectServerExecuted(object sender)
        {
            IDomainProjectPicker dpp = this.dataModel.ProjectPicker;

            if (dpp.Show())
            {
                this.dataModel.TfsUri = dpp.TfsUri;

                this.OnPropertyChanged("Builds");
            }
        }

        private void RequestSaveExecuted(object sender)
        {
            this.dataModel.Save();

            if (this.SaveRequested != null)
            {
                this.SaveRequested(this, EventArgs.Empty);
            }
        }

        private void RequestCloseExecuted(object sender)
        {
            if (this.CloseRequested != null)
            {
                this.CloseRequested(this, EventArgs.Empty);
            }
        }

        #endregion

        public event EventHandler CloseRequested;

        public event EventHandler SaveRequested;
    }
}