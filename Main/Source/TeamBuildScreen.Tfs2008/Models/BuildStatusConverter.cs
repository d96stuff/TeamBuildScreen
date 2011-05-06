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
                    break;
                case BuildStatus.InProgress:
                    return TeamBuildScreen.Core.Models.BuildStatus.InProgress;
                    break;
                case BuildStatus.NotStarted:
                    return TeamBuildScreen.Core.Models.BuildStatus.NotStarted;
                    break;
                case BuildStatus.PartiallySucceeded:
                    return TeamBuildScreen.Core.Models.BuildStatus.PartiallySucceeded;
                    break;
                case BuildStatus.Stopped:
                    return TeamBuildScreen.Core.Models.BuildStatus.Stopped;
                    break;
                case BuildStatus.Succeeded:
                    return TeamBuildScreen.Core.Models.BuildStatus.Succeeded;
                    break;
                default:
                    throw new NotSupportedException("Unable to convert unrecognised BuildStatus.");
            }
        }
    }
}