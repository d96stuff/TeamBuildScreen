//-----------------------------------------------------------------------
// <copyright file="ScreenSaverSettingsViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.ViewModels
{
    #region Usings

    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.TeamFoundation.Proxy;
    using TeamBuildScreenSaver.DataModels;

    #endregion

    public class ScreenSaverSettingsViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ScreenSaverSettingsDataModel dataModel;
        private readonly CommandBindingCollection commandBindings;
        private ICommand selectTfsCommand;

        #endregion

        #region Properties

        public CommandBindingCollection CommandBindings
        {
            get
            {
                return this.commandBindings;
            }
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

        public ObservableCollection<BuildSetting> Builds
        {
            get
            {
                return this.dataModel.Builds;
            }
        }

        public ICommand SelectTfsServer
        {
            get
            {
                return this.selectTfsCommand;
            }
        }

        #endregion

        #region Constructors

        public ScreenSaverSettingsViewModel(ScreenSaverSettingsDataModel dataModel)
        {
            this.dataModel = dataModel;

            this.dataModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                this.OnPropertyChanged(e.PropertyName);
            };

            this.commandBindings = new CommandBindingCollection();

            this.RegisterCommands();
        }

        #endregion

        #region Methods

        private void RegisterCommands()
        {
            this.selectTfsCommand = new RoutedCommand("SelectTfsServer", typeof(ScreenSaverSettingsViewModel));

            CommandBinding selectTfsServerBinding = new CommandBinding(this.selectTfsCommand, SelectTfsServerExecuted, CommandCanExecute);
            CommandBinding saveBinding = new CommandBinding(ApplicationCommands.Save, SaveExecuted, CommandCanExecute);

            CommandManager.RegisterClassCommandBinding(typeof(ScreenSaverSettingsViewModel), selectTfsServerBinding);
            CommandManager.RegisterClassCommandBinding(typeof(ScreenSaverSettingsViewModel), saveBinding);

            this.commandBindings.Add(selectTfsServerBinding);
            this.commandBindings.Add(saveBinding);
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

        private void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void SelectTfsServerExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DomainProjectPicker dpp = new DomainProjectPicker(DomainProjectPickerMode.None);

            if (dpp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.dataModel.TfsUri = dpp.SelectedServer.Uri.ToString();

                this.OnPropertyChanged("Builds");
            }

            e.Handled = true;
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.dataModel.Save();

            Application.Current.Shutdown();
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