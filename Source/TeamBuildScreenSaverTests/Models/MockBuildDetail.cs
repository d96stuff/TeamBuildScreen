//-----------------------------------------------------------------------
// <copyright file="MockBuildDetail.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaverTests.Models
{
    #region Usings

    using System;
    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    public class MockBuildDetail : IBuildDetail
    {
        #region Fields

        private BuildStatus status;
        private string requestedFor;
        private DateTime startTime;
        private bool buildFinished;
        private DateTime finishTime;
        private IBuildDefinition buildDefinition;

        #endregion

        #region Constructors

        public MockBuildDetail(BuildStatus status, string requestedFor, DateTime startTime, bool buildFinished, DateTime finishTime, IBuildDefinition buildDefinition)
        {
            this.status = status;
            this.requestedFor = requestedFor;
            this.startTime = startTime;
            this.buildFinished = buildFinished;
            this.finishTime = finishTime;
            this.buildDefinition = buildDefinition;
        }

        #endregion

        #region IBuildDetail Members

        public IBuildAgent BuildAgent
        {
            get { throw new NotImplementedException(); }
        }

        public Uri BuildAgentUri
        {
            get { throw new NotImplementedException(); }
        }

        public IBuildDefinition BuildDefinition
        {
            get { return this.buildDefinition; }
        }

        public Uri BuildDefinitionUri
        {
            get { throw new NotImplementedException(); }
        }

        public bool BuildFinished
        {
            get { return this.buildFinished; }
        }

        public string BuildNumber
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

        public IBuildServer BuildServer
        {
            get { throw new NotImplementedException(); }
        }

        public string CommandLineArguments
        {
            get { throw new NotImplementedException(); }
        }

        public BuildPhaseStatus CompilationStatus
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

        public string ConfigurationFolderPath
        {
            get { throw new NotImplementedException(); }
        }

        public Uri ConfigurationFolderUri
        {
            get { throw new NotImplementedException(); }
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Connect(int pollingInterval, System.ComponentModel.ISynchronizeInvoke synchronizingObject)
        {
            throw new NotImplementedException();
        }

        public IBuildDeletionResult Delete()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public string DropLocation
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

        public DateTime FinishTime
        {
            get { return this.finishTime; }
        }

        public IBuildInformation Information
        {
            get { throw new NotImplementedException(); }
        }

        public bool KeepForever
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

        public string LabelName
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

        public string LastChangedBy
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime LastChangedOn
        {
            get { throw new NotImplementedException(); }
        }

        public string LogLocation
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

        public event PollingCompletedEventHandler PollingCompleted;

        public string Quality
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

        public BuildReason Reason
        {
            get { throw new NotImplementedException(); }
        }

        public void Refresh(string[] informationTypes, QueryOptions queryOptions)
        {
            throw new NotImplementedException();
        }

        public void RefreshAllDetails()
        {
            throw new NotImplementedException();
        }

        public void RefreshMinimalDetails()
        {
            throw new NotImplementedException();
        }

        public string RequestedBy
        {
            get { throw new NotImplementedException(); }
        }

        public string RequestedFor
        {
            get { return this.requestedFor; }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public string SourceGetVersion
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

        public DateTime StartTime
        {
            get { return this.startTime; }
        }

        public BuildStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public event StatusChangedEventHandler StatusChanged;

        public event StatusChangedEventHandler StatusChanging;

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public BuildPhaseStatus TestStatus
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

        public Uri Uri
        {
            get { throw new NotImplementedException(); }
        }

        public void Wait()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}