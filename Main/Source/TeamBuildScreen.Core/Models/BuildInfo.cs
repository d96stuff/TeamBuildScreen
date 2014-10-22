using System.Collections.Generic;

namespace TeamBuildScreen.Core.Models
{
    using System;

    public class BuildInfo : IBuildInfo
    {
        public static IBuildInfo Empty = new BuildInfo
            {
                HasWarnings = false,
                RequestedFor = null,
                TestsFailed = null,
                TestsPassed = null,
                TestsTotal = null,
                BuildFinished = false,
                FinishTime = null,
                StartTime = null,
                Status = BuildStatus.NoneFound,
				CodeCoverage = null
            };

        public virtual bool BuildFinished { get; protected set; }

        public virtual DateTime? FinishTime { get; protected set; }

        public virtual bool HasWarnings { get; protected set; }

		public BuildPhaseStatus CompilationStatus { get; protected set; }

		public BuildPhaseStatus TestStatus { get; protected set; }

		public virtual string RequestedFor { get; protected set; }

        public virtual DateTime? StartTime { get; protected set; }

        public virtual BuildStatus Status { get; protected set; }

        public virtual int? TestsFailed { get; protected set; }

        public virtual int? TestsPassed { get; protected set; }

		public virtual int? TestsTotal { get; protected set; }

		public virtual List<string> FailedTests { get; protected set; }

		public virtual int? CodeCoverage { get; protected set; }
	}
}