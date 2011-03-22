using System.Globalization;
using TeamBuildScreen.Core.Models;

namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.Xml;

    using TeamBuildScreen.Hudson.Models.Tasks.JUnit;

    public class HudsonBuildInfo : BuildInfo, IBuildInfo
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0,
                                                      DateTimeKind.Utc);
        private readonly FreeStyleBuild hudsonBuild;
        private readonly TestResult testResult;

        public HudsonBuildInfo(FreeStyleBuild hudsonBuild, TestResult testResult)
        {
            this.hudsonBuild = hudsonBuild;
            this.testResult = testResult;

            if (this.testResult != null)
            {
                this.TestsFailed = this.testResult.FailCount;
                this.TestsPassed = this.testResult.PassCount;

                int skipped = this.testResult.SkipCount;

                this.TestsTotal = skipped+ this.TestsFailed + this.TestsPassed;
            }
        }

        public bool BuildFinished
        {
            get
            {
                if (this.hudsonBuild != null)
                {
                    return !this.hudsonBuild.Building;
                }

                return false;
            }
        }

        public DateTime FinishTime
        {
            get
            {
                if (this.hudsonBuild != null)
                {
                    return this.StartTime.AddMilliseconds(this.hudsonBuild.Duration);
                }

                return DateTime.MinValue;
            }
        }

        public BuildStatus Status
        {
            get
            {
                if (this.hudsonBuild != null)
                {
                    return !this.BuildFinished
                               ? BuildStatus.InProgress
                               : GetBuildStatusFromResult(this.hudsonBuild.Result);
                }

                return BuildStatus.NotStarted;
            }
        }

        public string RequestedFor
        {
            get
            {
                if (this.hudsonBuild != null)
                {
                    if (this.hudsonBuild.Culprit == null || this.hudsonBuild.Culprit.Length == 0)
                    {
                        return "anonymous";
                    }

                    return this.hudsonBuild.Culprit[0].FullName;
                }

                return string.Empty;
            }
        }

        public DateTime StartTime
        {
            get
            {
                if (this.hudsonBuild != null)
                {
                    var timeStampInMilliseconds = this.hudsonBuild.Timestamp.ToString().Substring(0, 10);

                    return Epoch.AddSeconds(long.Parse(timeStampInMilliseconds, CultureInfo.InvariantCulture));
                }

                return DateTime.MinValue;
            }
        }

        private static BuildStatus GetBuildStatusFromResult(object result)
        {
            var nodes = result as XmlNode[];

            switch (nodes[0].InnerText)
            {
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