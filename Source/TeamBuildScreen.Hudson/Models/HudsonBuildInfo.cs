namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.Globalization;
    using System.Xml;

    using TeamBuildScreen.Core.Models;
    using TeamBuildScreen.Hudson.Models.Tasks.JUnit;

    public class HudsonBuildInfo : BuildInfo
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public HudsonBuildInfo(FreeStyleBuild hudsonBuild, TestResult testResult)
        {
            this.Init(hudsonBuild, testResult);
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

        private void Init(FreeStyleBuild hudsonBuild, TestResult testResult)
        {
            if (testResult != null)
            {
                this.TestsFailed = testResult.FailCount;
                this.TestsPassed = testResult.PassCount;

                int skipped = testResult.SkipCount;

                this.TestsTotal = skipped + this.TestsFailed + this.TestsPassed;
            }

            this.BuildFinished = !hudsonBuild.Building;
            this.FinishTime = this.StartTime.AddMilliseconds(hudsonBuild.Duration);
            this.Status = this.BuildFinished ? GetBuildStatusFromResult(hudsonBuild.Result) : BuildStatus.InProgress;

            if (hudsonBuild.Culprit == null || hudsonBuild.Culprit.Length == 0)
            {
                this.RequestedFor = "anonymous";
            }
            else
            {
                this.RequestedFor = hudsonBuild.Culprit[0].FullName;
            }

            var timeStampInMilliseconds = hudsonBuild.Timestamp.ToString().Substring(0, 10);

            this.StartTime = Epoch.AddSeconds(long.Parse(timeStampInMilliseconds, CultureInfo.InvariantCulture));
        }
    }
}