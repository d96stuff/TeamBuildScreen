using System.Globalization;
using System.Linq;
using TeamBuildScreen.Core.Models;

namespace TeamBuildScreen.Hudson.Models
{
    using System;

    public class HudsonBuildInfo : IBuildInfo
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0,
                                                      DateTimeKind.Utc);
        private readonly freeStyleBuild hudsonBuild;
        private readonly testResult testResult;

        public HudsonBuildInfo(freeStyleBuild hudsonBuild, testResult testResult)
        {
            this.hudsonBuild = hudsonBuild;
            this.testResult = testResult;
        }

        public bool BuildFinished
        {
            get { return !bool.Parse(this.hudsonBuild.building); }
        }

        public DateTime FinishTime
        {
            get { return this.StartTime.AddMilliseconds(long.Parse(this.hudsonBuild.duration)); }
        }

        public BuildStatus Status
        {
            get { return this.GetBuildStatusFromString(this.hudsonBuild.result); }
        }

        public string RequestedFor
        {
            get
            {
                var cause = this.hudsonBuild.action.FirstOrDefault(a => a.cause != null);

                if (cause != null)
                {
                    return cause.cause.First().shortDescription;
                }

                return null;
            }
        }

        public DateTime StartTime
        {
            get { return Epoch.AddSeconds(long.Parse(this.hudsonBuild.timestamp.Substring(0, 10), CultureInfo.InvariantCulture)); }
        }

        public int? TestsFailed
        {
            get { return this.testResult == null ? (int?)null : int.Parse(this.testResult.failCount); }
        }

        public int? TestsPassed
        {
            get { return this.testResult == null ? (int?)null : int.Parse(this.testResult.passCount); }
        }

        public int? TestsTotal
        {
            get
            {
                if (this.testResult == null)
                {
                    return null;
                }

                int skipped = int.Parse(this.testResult.skipCount);

                return skipped + this.TestsFailed + this.TestsPassed;
            }
        }

        public bool HasWarnings
        {
            get { return false; }
        }

        private BuildStatus GetBuildStatusFromString(string s)
        {
            switch (s)
            {
                case null:
                    return BuildStatus.InProgress;
                case "ABORTED":
                    return BuildStatus.Stopped;
                case "FAILURE":
                    return BuildStatus.Failed;
                case "NOT_BUILT":
                    return BuildStatus.NotStarted;
                case "SUCCESS":
                    return BuildStatus.Succeeded;
                case "UNSTABLE":
                    return BuildStatus.PartiallySucceeded;
                default:
                    return BuildStatus.NoneFound;
            }
        }
    }
}
