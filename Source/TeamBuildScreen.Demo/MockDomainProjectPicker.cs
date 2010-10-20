using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuildScreen.Core.Models;

namespace TeamBuildScreen.Demo
{
    public class MockDomainProjectPicker : IDomainProjectPicker
    {
        #region IDomainProjectPicker Members

        public bool Show()
        {
            return true;
        }

        public string TfsUri
        {
            get { return string.Empty; }
        }

        #endregion
    }
}