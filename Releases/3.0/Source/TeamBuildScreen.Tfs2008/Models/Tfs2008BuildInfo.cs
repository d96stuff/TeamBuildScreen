using System.Collections.Generic;

namespace TeamBuildScreen.Tfs2008.Models
{
    using Microsoft.TeamFoundation.Build.Client;

    using TeamBuildScreen.Core.Models;

    public class Tfs2008BuildInfo : BuildInfo
    {
        public Tfs2008BuildInfo(IBuildDetail buildDetail, string flavour, string platform)
        {
            this.Init(buildDetail, flavour, platform);
        }

        private void Init(IBuildDetail buildDetail, string flavour, string platform)
        {
			this.FailedTests = new List<string>();

			var configurationSummary = InformationNodeConverters.GetConfigurationSummary(buildDetail, flavour, platform);

            if (configurationSummary != null)
            {
                if (configurationSummary.TestSummaries.Count > 0)
                {
                    var testSummary = configurationSummary.TestSummaries[0];

                    this.TestsFailed = testSummary.TestsFailed;
                    this.TestsPassed = testSummary.TestsPassed;
                    this.TestsTotal = testSummary.TestsTotal;
                }

                this.HasWarnings = configurationSummary.TotalCompilationWarnings > 0;
            }

            this.BuildFinished = buildDetail.BuildFinished;
            this.FinishTime = buildDetail.FinishTime;
            this.Status = BuildStatusConverter.Convert(buildDetail.Status);
			this.CompilationStatus = BuildPhaseStatusConverter.Convert(buildDetail.CompilationStatus);
			this.TestStatus = BuildPhaseStatusConverter.Convert(buildDetail.TestStatus);
			this.RequestedFor = buildDetail.RequestedFor;
            this.StartTime = buildDetail.StartTime;
        }
    }
}