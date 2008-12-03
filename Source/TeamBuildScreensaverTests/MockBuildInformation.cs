using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Build.Client;

namespace TeamBuildScreensaverTests
{
    class MockBuildInformation : IBuildInformation
    {
        #region IBuildInformation Members

        public IBuildInformationNode CreateNode()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public List<IBuildInformationNode> GetNodesByType(string type)
        {
            throw new NotImplementedException();
        }

        public List<IBuildInformationNode> GetSortedNodesByType(string type, IComparer<IBuildInformationNode> comparer)
        {
            throw new NotImplementedException();
        }

        public IBuildInformationNode[] Nodes
        {
            get { throw new NotImplementedException(); }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
