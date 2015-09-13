using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Build.Client;

namespace TeamBuildScreen.Tfs2008.Models
{
    public static class BuildStatusConverter
    {
        public static TeamBuildScreen.Core.Models.BuildStatus Convert(BuildStatus status)
        {
            switch (status)
            {
                case BuildStatus.Failed:
                    return TeamBuildScreen.Core.Models.BuildStatus.Failed;
                case BuildStatus.InProgress:
                    return TeamBuildScreen.Core.Models.BuildStatus.InProgress;
                case BuildStatus.NotStarted:
                    return TeamBuildScreen.Core.Models.BuildStatus.NotStarted;
                case BuildStatus.PartiallySucceeded:
                    return TeamBuildScreen.Core.Models.BuildStatus.PartiallySucceeded;
                case BuildStatus.Stopped:
                    return TeamBuildScreen.Core.Models.BuildStatus.Stopped;
                case BuildStatus.Succeeded:
                    return TeamBuildScreen.Core.Models.BuildStatus.Succeeded;
                default:
                    throw new NotSupportedException("Unable to convert unrecognised BuildStatus.");
            }
        }
    }
}