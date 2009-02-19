﻿//-----------------------------------------------------------------------
// <copyright file="MainViewModel.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.ViewModels
{
    #region Usings

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using TeamBuildScreenSaver.Models;
    using System.Windows.Input;
    using System.Windows;

    #endregion

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly CommandBindingCollection commandBindings;
        private double innerMargin = 8;
        private int columns = 6;
        private ICommand clickedCommand;
        private bool closeOnClicked = true;

        #endregion

        #region Properties

        public CommandBindingCollection CommandBindings
        {
            get
            {
                return this.commandBindings;
            }
        }

        public ObservableCollection<BuildDetailViewModel> Builds
        {
            get;
            private set;
        }

        public IMessageViewModel ErrorMessageViewModel
        {
            get;
            private set;
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

        public bool CloseOnClicked
        {
            get
            {
                return this.closeOnClicked;
            }
            set
            {
                this.closeOnClicked = value;
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

        public ICommand Clicked
        {
            get
            {
                return this.clickedCommand;
            }
        }

        #endregion

        #region Constructors

        public MainViewModel(IBuildServerService service, StringCollection builds)
        {
            this.InitializeBuildPanels(service, builds, null);
            this.commandBindings = new CommandBindingCollection();
            this.RegisterCommands();
        }

        public MainViewModel(IBuildServerService service, StringCollection builds, ConfigurationSummaryHandler configurationSummaryHandler)
        {
            this.InitializeBuildPanels(service, builds, configurationSummaryHandler);
            this.commandBindings = new CommandBindingCollection();
            this.RegisterCommands();
        }

        #endregion

        #region Methods

        private void InitializeBuildPanels(IBuildServerService service, StringCollection builds, ConfigurationSummaryHandler configurationSummaryHandler)
        {
            this.ErrorMessageViewModel = new BuildServerServiceMessageViewModel(service);
            this.Builds = new ObservableCollection<BuildDetailViewModel>();

            foreach (string build in builds)
            {
                string[] buildParts = build.Split(';');
                string teamProject = buildParts[0];
                string definitionName = buildParts[1];
                string configuration = buildParts[2];
                string platform = buildParts[3];

                BuildDetailModel dataModel = new BuildDetailModel(teamProject, definitionName, configuration, platform, service);
                BuildDetailViewModel viewModel =
                    configurationSummaryHandler == null? new BuildDetailViewModel(dataModel) : new BuildDetailViewModel(dataModel, configurationSummaryHandler);

                this.Builds.Add(viewModel);
            }
        }

        private void RegisterCommands()
        {
            this.clickedCommand = new RoutedCommand("Clicked", typeof(MainViewModel));

            CommandBinding clickedBinding = new CommandBinding(this.clickedCommand, this.ClickedExecuted, this.ClickedCanExecute);

            CommandManager.RegisterClassCommandBinding(typeof(MainViewModel), clickedBinding);

            this.commandBindings.Add(clickedBinding);
        }

        private void ClickedCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.closeOnClicked;
            e.Handled = true;
        }

        private void ClickedExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
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