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
    using TeamBuildScreenSaver.Models;
    using TeamBuildScreenSaver.Views;

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

            this.dataModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                this.OnPropertyChanged(e.PropertyName);
            };

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
            CommandBinding selectServerBinding =
                new CommandBinding(
                    TfsCommands.SelectServer,
                    this.SelectServerExecuted,
                    this.CommandCanExecute);
            CommandBinding saveBinding =
                new CommandBinding(
                    ApplicationCommands.Save,
                    this.SaveExecuted,
                    this.CommandCanExecute);
            CommandBinding closeBinding =
                new CommandBinding(
                    ApplicationCommands.Close,
                    this.CloseExecuted,
                    this.CommandCanExecute);

            CommandManager.RegisterClassCommandBinding(typeof(ScreenSaverSettings), selectServerBinding);
            CommandManager.RegisterClassCommandBinding(typeof(ScreenSaverSettings), saveBinding);
            CommandManager.RegisterClassCommandBinding(typeof(ScreenSaverSettings), closeBinding);
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

        private void SelectServerExecuted(object sender, ExecutedRoutedEventArgs e)
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

            e.Handled = true;
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();

            e.Handled = true;
        }

        #endregion
    }
}