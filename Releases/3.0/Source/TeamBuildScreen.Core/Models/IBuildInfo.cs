using System;
using System.Collections.Generic;

namespace TeamBuildScreen.Core.Models
{
    public interface IBuildInfo
    {
        bool BuildFinished { get; }

        DateTime? FinishTime { get; }

        BuildStatus Status { get; }

		BuildPhaseStatus CompilationStatus { get; }

		BuildPhaseStatus TestStatus { get; }

		string RequestedFor { get; }

        DateTime? StartTime { get; }

        int? TestsFailed { get; }

        int? TestsPassed { get; }

        int? TestsTotal { get; }

        bool HasWarnings { get; }

		int? CodeCoverage { get; }

		List<string> FailedTests { get; }
    }
}