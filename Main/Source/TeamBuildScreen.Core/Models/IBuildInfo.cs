using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamBuildScreen.Core.Models
{
    public interface IBuildInfo
    {
        bool BuildFinished { get; }

        DateTime FinishTime { get; }

        BuildStatus Status { get; }

        string RequestedFor { get; }

        DateTime StartTime { get; }

        int? TestsFailed { get; }

        int? TestsPassed { get; }

        int? TestsTotal { get; }

        bool HasWarnings { get; }
    }
}