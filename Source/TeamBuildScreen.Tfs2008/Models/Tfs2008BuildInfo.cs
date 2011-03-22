namespace TeamBuildScreen.Tfs2008.Models
{
    using System;

    using Microsoft.TeamFoundation.Build.Client;

    using TeamBuildScreen.Core.Models;

    using BuildStatus = TeamBuildScreen.Core.Models.BuildStatus;

    public class Tfs2008BuildInfo : BuildInfo
    {
        private readonly IBuildDetail buildDetail;

        public Tfs2008BuildInfo(IBuildDetail buildDetail, string flavour, string platform)
        {
            this.buildDetail = buildDetail;

            IConfigurationSummary configurationSummary =
                InformationNodeConverters.GetConfigurationSummary(this.buildDetail, flavour, platform);

            if (configurationSummary != null)
            {
                if (configurationSummary.TestSummaries.Count > 0)
                {
                    ITestSummary testSummary = configurationSummary.TestSummaries[0];

                    this.TestsFailed = testSummary.TestsFailed;
                    this.TestsPassed = testSummary.TestsPassed;
                    this.TestsTotal = testSummary.TestsTotal;
                }

                if (configurationSummary.CompilationSummaries.Count > 0)
                {
                    ICompilationSummary compilationSummary = configurationSummary.CompilationSummaries[0];

                    this.HasWarnings = compilationSummary.CompilationWarnings > 0;
                }
            }
        }

        #region IBuildInfo Members

        public override bool BuildFinished
        {
            get
            {
                return this.buildDetail.BuildFinished;
            }
        }

        public override DateTime FinishTime
        {
            get
            {
                return this.buildDetail.FinishTime;
            }
        }

        public override BuildStatus Status
        {
            get
            {
                return BuildStatusConverter.Convert(this.buildDetail.Status);
            }
        }

        public override string RequestedFor
        {
            get
            {
                return this.buildDetail.RequestedFor;
            }
        }

        public override DateTime StartTime
        {
            get
            {
                return this.buildDetail.StartTime;
            }
        }

        #endregion
    }
}