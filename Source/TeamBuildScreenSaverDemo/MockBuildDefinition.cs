//-----------------------------------------------------------------------
// <copyright file="MockBuildDefinition.cs" company="Jim Liddell"> 
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

    public class MockBuildDefinition : IBuildDefinition
    {
        #region Fields

        private string teamProject;
        private string name;

        #endregion

        #region Constructors

        public MockBuildDefinition(string teamProject, string name)
        {
            this.teamProject = teamProject;
            this.name = name;
        }

        #endregion

        #region IBuildDefinition Members

        public ISchedule AddSchedule()
        {
            throw new NotImplementedException();
        }

        public IBuildServer BuildServer
        {
            get { throw new NotImplementedException(); }
        }

        public string ConfigurationFolderPath
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int ContinuousIntegrationQuietPeriod
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ContinuousIntegrationType ContinuousIntegrationType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IBuildRequest CreateBuildRequest()
        {
            throw new NotImplementedException();
        }

        public IBuildDetail CreateManualBuild(string buildNumber, string dropLocation, BuildStatus buildStatus, IBuildAgent agent, string requestedFor)
        {
            throw new NotImplementedException();
        }

        public IBuildDetail CreateManualBuild(string buildNumber, string dropLocation)
        {
            throw new NotImplementedException();
        }

        public IBuildDetail CreateManualBuild(string buildNumber)
        {
            throw new NotImplementedException();
        }

        public IProjectFile CreateProjectFile()
        {
            throw new NotImplementedException();
        }

        public IBuildDefinitionSpec CreateSpec()
        {
            throw new NotImplementedException();
        }

        public IBuildAgent DefaultBuildAgent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Uri DefaultBuildAgentUri
        {
            get { throw new NotImplementedException(); }
        }

        public string DefaultDropLocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Enabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Uri LastBuildUri
        {
            get { throw new NotImplementedException(); }
        }

        public string LastGoodBuildLabel
        {
            get { throw new NotImplementedException(); }
        }

        public Uri LastGoodBuildUri
        {
            get { throw new NotImplementedException(); }
        }

        public IBuildDetail[] QueryBuilds()
        {
            throw new NotImplementedException();
        }

        public Dictionary<BuildStatus, IRetentionPolicy> RetentionPolicies
        {
            get { throw new NotImplementedException(); }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public List<ISchedule> Schedules
        {
            get { throw new NotImplementedException(); }
        }

        public IWorkspaceTemplate Workspace
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IBuildGroupItem Members

        public string FullPath
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public string TeamProject
        {
            get { return this.teamProject; }
        }

        public Uri Uri
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}