namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.Windows;

    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Hudson.ViewModels;
    using TeamBuildScreen.Hudson.Views;

    public class HudsonDomainProjectPicker : IDomainProjectPicker
    {
        private string url;

        #region IDomainProjectPicker Members

        public bool Show()
        {
            var cancelled = false;
            var viewModel = new SelectHudsonViewModel();
            var dialog = new SelectHudson(viewModel);

            // configure view model
            viewModel.OkRequested += (sender, e) =>
                {
                    string message;

                    if (IsValid(viewModel.Url, out message))
                    {
                        dialog.Close();
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Error contacting server: {0}", message));
                    }
                };
            viewModel.CancelRequested += (sender, e) =>
                {
                    cancelled = true;
                    dialog.Close();
                };

            // show view
            dialog.ShowDialog();

            this.url = !cancelled ? viewModel.Url : null;

            return !cancelled;
        }

        private bool IsValid(string url, out string message)
        {
            message = string.Empty;

            if (url.Trim().Length == 0)
            {
                message = "You must enter an address.";

                return false;
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                message = string.Format("'{0}' does not appear to be a valid address.", url);

                return false;
            }

            try
            {
                HudsonProvider.Get(url);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }

            return true;
        }

        public string TfsUri
        {
            get { return this.url; }
        }

        #endregion
    }
}