//-----------------------------------------------------------------------
// <copyright file="MainViewModel.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.ViewModels
{
    #region Usings

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using TeamBuildScreen.Core.Models;
    using System.Windows.Input;
    using System.Windows;
    using TeamBuildScreen.Core.Views;

    #endregion

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private double innerMargin = 8;
        private int columns = 6;
        private bool closeOnClicked = true;

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

        #endregion

        #region Constructors

        public MainViewModel(IBuildServerService service, StringCollection builds)
        {
            this.InitializeBuildPanels(service, builds);
            this.RegisterCommands();
        }

        #endregion

        #region Methods

        private void InitializeBuildPanels(IBuildServerService service, StringCollection builds)
        {
            this.ErrorMessageViewModel = new BuildServerServiceMessageViewModel(service);
            this.Builds = new ObservableCollection<BuildInfoViewModel>();

            foreach (string build in builds)
            {
                string[] buildParts = build.Split(';');
                string teamProject = buildParts[0];
                string definitionName = buildParts[1];
                string configuration = buildParts[2];
                string platform = buildParts[3];

                BuildInfoModel dataModel = new BuildInfoModel(teamProject, definitionName, configuration, platform, service);
                BuildInfoViewModel viewModel = new BuildInfoViewModel(dataModel);

                this.Builds.Add(viewModel);
            }
        }

        private void RegisterCommands()
        {
            CommandBinding closeBinding =
                new CommandBinding(
                    ApplicationCommands.Close,
                    this.CloseExecuted,
                    this.CommandCanExecute);

            CommandManager.RegisterClassCommandBinding(typeof(Main), closeBinding);
        }

        private void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.closeOnClicked;
            e.Handled = true;
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
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