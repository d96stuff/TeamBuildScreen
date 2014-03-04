﻿namespace TeamBuildScreen.Tfs2010.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;

    using TeamBuildScreen.Core.Models;

    public class Tfs2010BuildInfo : BuildInfo
    {
		public Tfs2010BuildInfo(IBuildDetail buildDetail, string flavour, string platform, IEnumerable<ITestRun> testRuns, ICoverageAnalysisManager coverageAnalysisManager)
        {
			this.Init(buildDetail, flavour, platform, testRuns, coverageAnalysisManager);
        }

		private void Init(IBuildDetail buildDetail, string flavour, string platform, IEnumerable<ITestRun> testRuns, ICoverageAnalysisManager coverageAnalysisManager)
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
			this.CodeCoverage = GetCodeCoverage(buildDetail, coverageAnalysisManager);
		}

		public int? GetCodeCoverage(IBuildDetail buildDetail, ICoverageAnalysisManager analysisManager)
		{
			IBuildCoverage[] queryBuildCoverage = analysisManager.QueryBuildCoverage(buildDetail.Uri.ToString(), CoverageQueryFlags.Modules);
			int blocksCovered = 0;
			int blocksNotCovered = 0;

			foreach (var coverage in queryBuildCoverage)
			{
				foreach (var moduleInfo in coverage.Modules)
				{
					blocksCovered += moduleInfo.Statistics.BlocksCovered;
					blocksNotCovered += moduleInfo.Statistics.BlocksNotCovered;
				}
			}
			if (blocksCovered + blocksNotCovered == 0)
			{
				return null;
			}
			return 100 * blocksCovered / (blocksCovered + blocksNotCovered);
		}
    }
}