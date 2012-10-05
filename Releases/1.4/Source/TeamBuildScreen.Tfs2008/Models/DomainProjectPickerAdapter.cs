using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuildScreen.Core.Models;
using Microsoft.TeamFoundation.Proxy;

namespace TeamBuildScreen.Tfs2008.Models
{
    public class DomainProjectPickerAdapter : IDomainProjectPicker
    {
        private DomainProjectPicker dpp;

        public DomainProjectPickerAdapter()
        {
            this.dpp = new DomainProjectPicker(DomainProjectPickerMode.None);
        }

        public bool Show()
        {
            return this.dpp.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public string TfsUri
        {
            get
            {
                return this.dpp.SelectedServer.Uri.ToString();
            }
        }
    }
}