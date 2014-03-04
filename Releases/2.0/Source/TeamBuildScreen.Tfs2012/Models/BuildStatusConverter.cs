using System;
using Microsoft.TeamFoundation.Build.Client;

namespace TeamBuildScreen.Tfs2012.Models
{
    public static class BuildStatusConverter
    {
        public static Core.Models.BuildStatus Convert(BuildStatus status)
        {
            switch (status)
            {
                case BuildStatus.Failed:
                    return Core.Models.BuildStatus.Failed;
                case BuildStatus.InProgress:
                    return Core.Models.BuildStatus.InProgress;
                case BuildStatus.NotStarted:
                    return Core.Models.BuildStatus.NotStarted;
                case BuildStatus.PartiallySucceeded:
                    return Core.Models.BuildStatus.PartiallySucceeded;
                case BuildStatus.Stopped:
                    return Core.Models.BuildStatus.Stopped;
                case BuildStatus.Succeeded:
                    return Core.Models.BuildStatus.Succeeded;
                default:
                    throw new NotSupportedException("Unable to convert unrecognised BuildStatus.");
            }
        }
    }
}