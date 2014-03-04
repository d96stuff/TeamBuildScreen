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
            get { return "http://default/"; }
        }

        #endregion
    }
}