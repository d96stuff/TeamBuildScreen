//-----------------------------------------------------------------------
// <copyright file="TeamProjectPickerAdapter.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using TeamBuildScreen.Core.Models;

namespace TeamBuildScreen.Tfs2012.Models
{
    using Microsoft.TeamFoundation.Client;

    public class TeamProjectPickerAdapter : IDomainProjectPicker
    {
        private TeamProjectPicker tpp;

        public TeamProjectPickerAdapter()
        {
            this.tpp = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
        }

        public bool Show()
        {
            return this.tpp.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public string TfsUri
        {
            get
            {
                return this.tpp.SelectedTeamProjectCollection.Uri.ToString();
            }
        }
    }
}