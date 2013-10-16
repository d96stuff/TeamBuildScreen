namespace TeamBuildScreen.Tfs2012.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;

    using TeamBuildScreen.Core.Models;

    public class Tfs2012BuildInfo : Core.Models.BuildInfo
    {
        public Tfs2012BuildInfo(IBuildDetail buildDetail, string flavour, string platform, IEnumerable<ITestRun> testRuns)
        {
            this.Init(buildDetail, flavour, platform, testRuns);
        }

        private void Init(IBuildDetail buildDetail, string flavour, string platform, IEnumerable<ITestRun> testRuns)
        {
            this.TestsFailed = testRuns.Select(run => run.Statistics.FailedTests).Sum();
            this.TestsPassed = testRuns.Select(run => run.Statistics.PassedTests).Sum();
            this.TestsTotal = testRuns.Select(run => run.Statistics.TotalTests).Sum();

            var configurationSummary = InformationNodeConverters.GetConfigurationSummary(buildDetail, flavour, platform);

            if (configurationSummary != null)
            {
                this.HasWarnings = configurationSummary.TotalCompilationWarnings > 0;
            }

            this.BuildFinished = buildDetail.BuildFinished;
            this.FinishTime = buildDetail.FinishTime;
            this.Status = BuildStatusConverter.Convert(buildDetail.Status);
            this.RequestedFor = buildDetail.RequestedFor;
            this.StartTime = buildDetail.StartTime;
        }
    }
}