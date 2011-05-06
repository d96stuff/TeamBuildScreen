namespace TeamBuildScreen.Core.Models
{
    using System;

    public class BuildInfo : IBuildInfo
    {
        public static IBuildInfo Empty = new BuildInfo
            {
                HasWarnings = false, 
                RequestedFor = string.Empty, 
                TestsFailed = null, 
                TestsPassed = null, 
                TestsTotal = null, 
                BuildFinished = false, 
                FinishTime = DateTime.MinValue, 
                StartTime = DateTime.MinValue, 
                Status = BuildStatus.NoneFound
            };

        public virtual bool BuildFinished { get; protected set; }

        public virtual DateTime FinishTime { get; protected set; }

        public virtual bool HasWarnings { get; protected set; }

        public virtual string RequestedFor { get; protected set; }

        public virtual DateTime StartTime { get; protected set; }

        public virtual BuildStatus Status { get; protected set; }

        public virtual int? TestsFailed { get; protected set; }

        public virtual int? TestsPassed { get; protected set; }

        public virtual int? TestsTotal { get; protected set; }
    }
}