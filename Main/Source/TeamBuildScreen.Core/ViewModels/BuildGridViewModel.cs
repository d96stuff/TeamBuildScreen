//-----------------------------------------------------------------------
// <copyright file="BuildGridViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.ViewModels
{
    #region Usings

    using System;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows.Input;
    using TeamBuildScreen.Core.Models;

    #endregion

    public class BuildGridViewModel : INotifyPropertyChanged
    {
        #region Fields

        private double innerMargin = 8;
        private int columns = 6;
        private IBuildServerService service;

        #endregion

        #region Properties

        public ObservableCollection<BuildInfoViewModel> Builds
        {
            get;
            private set;
        }

        public IMessageViewModel ErrorMessageViewModel
        {
            get;
            private set;
        }

        public ICommand RequestClose
        {
            get;
            set;
        }

        public double InnerMargin
        {
            get
            {
                return this.innerMargin;
            }

            set
            {
                this.innerMargin = value;

                this.OnPropertyChanged("InnerMargin");
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
                this.columns = value;

                this.OnPropertyChanged("Columns");
            }
        }

        #endregion

        #region Constructors

        public BuildGridViewModel(IBuildServerService service, StringCollection builds)
        {
            this.service = service;
            this.Builds = new ObservableCollection<BuildInfoViewModel>();
            this.ErrorMessageViewModel = new BuildServerServiceMessageViewModel(service);
            this.InitializeBuildPanels(builds);
            this.RequestClose = new RelayCommand(x => this.RequestCloseExecuted(x), x => this.CommandCanExecute(x));
        }

        #endregion

        #region Methods

        private BuildInfoViewModel GetBuildInfoViewModel(string build)
        {
            string[] buildParts = build.Split(';');
            string teamProject = buildParts[0];
            string definitionName = buildParts[1];
            string configuration = buildParts[2];
            string platform = buildParts[3];

            BuildInfoModel dataModel = new BuildInfoModel(teamProject, definitionName, configuration, platform, service);
            BuildInfoViewModel viewModel = new BuildInfoViewModel(dataModel);
            return viewModel;
        }

        public void InitializeBuildPanels(StringCollection builds)
        {
            this.Builds.Clear();
            this.service.ClearBuilds();

            foreach (string build in builds)
            {
                BuildInfoViewModel viewModel = GetBuildInfoViewModel(build);

                this.Builds.Add(viewModel);
            }
        }

        private bool CommandCanExecute(object sender)
        {
            return true;
        }

        private void RequestCloseExecuted(object sender)
        {
            if (this.CloseRequested != null)
            {
                this.CloseRequested(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged(string propertyName)
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

        public event EventHandler CloseRequested;

        #endregion
    }
}