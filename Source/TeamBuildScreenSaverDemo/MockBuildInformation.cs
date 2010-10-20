//-----------------------------------------------------------------------
// <copyright file="MockBuildInformation.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaverDemo
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    public class MockBuildInformation : IBuildInformation
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