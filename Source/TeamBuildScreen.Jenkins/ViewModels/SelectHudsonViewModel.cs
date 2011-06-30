using System;
using System.Windows.Input;
using TeamBuildScreen.Core.Models;

namespace TeamBuildScreen.Hudson.ViewModels
{
    public class SelectHudsonViewModel
    {
        public SelectHudsonViewModel()
        {
            this.RegisterCommands();
        }

        #region Properties

        public ICommand RequestOk
        {
            get;
            set;
        }

        public ICommand RequestCancel
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        #endregion

        private void RegisterCommands()
        {
            this.RequestOk = new RelayCommand(this.RequestOkExecuted, this.CommandCanExecute);
            this.RequestCancel = new RelayCommand(this.RequestCancelExecuted, this.CommandCanExecute);
        }

        private void RequestOkExecuted(object sender)
        {
            if (this.OkRequested != null)
            {
                this.OkRequested(this, EventArgs.Empty);
            }
        }

        private void RequestCancelExecuted(object sender)
        {
            if (this.CancelRequested != null)
            {
                this.CancelRequested(this, EventArgs.Empty);
            }
        }

        private bool CommandCanExecute(object sender)
        {
            return true;
        }

        public event EventHandler OkRequested;

        public event EventHandler CancelRequested;
    }
}
