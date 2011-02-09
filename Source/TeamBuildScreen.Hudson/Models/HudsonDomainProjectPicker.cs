namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using TeamBuildScreen.Hudson.ViewModels;
    using TeamBuildScreen.Hudson.Views;
    using TeamBuildScreen.Core.Models;

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
            viewModel.OkRequested += (object sender, EventArgs e) => dialog.Close();
            viewModel.CancelRequested += (object sender, EventArgs e) =>
            {
                cancelled = true;
                dialog.Close();
            };

            // show view
            dialog.ShowDialog();

            this.url = !cancelled ? viewModel.Url : null;

            return !cancelled;
        }

        public string TfsUri
        {
            get { return this.url; }
        }

        #endregion
    }
}