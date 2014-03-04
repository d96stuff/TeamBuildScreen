//-----------------------------------------------------------------------
// <copyright file="MockBuildDetail.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Demo
{
    #region Usings

    using System;
    using TeamBuildScreen.Core.Models;

    #endregion

    public class MockBuildInfo : IBuildInfo
    {
        #region Fields

        private string requestedFor;
        private DateTime startTime;
        private bool buildFinished;
        private DateTime finishTime;

        #endregion

        #region Constructors

        public MockBuildInfo(BuildStatus status, string requestedFor, DateTime startTime, bool buildFinished, DateTime finishTime)
        {
            this.Status = status;
            this.requestedFor = requestedFor;
            this.startTime = startTime;
            this.buildFinished = buildFinished;
            this.finishTime = finishTime;
        }

        #endregion

        #region IBuildDetail Members

        public bool BuildFinished
        {
            get { return this.buildFinished; }
        }

        public DateTime? FinishTime
        {
            get { return this.finishTime; }
        }

        public string RequestedFor
        {
            get { return string.Format("Requested by {0}", this.requestedFor); }
        }

        public DateTime? StartTime
        {
            get { return this.startTime; }
        }

        public BuildStatus Status { get; set; }

        public int? TestsFailed { get; set; }

        public int? TestsPassed { get; set; }

        public int? TestsTotal
        {
            get
            {
                if (this.TestsPassed.HasValue)
                {
                    return this.TestsFailed.Value + this.TestsPassed.Value;
                }

                return null;
            }
        }

        public bool HasWarnings { get; set; }
	    public int? CodeCoverage { get; private set; }

	    #endregion
    }
}