using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuildScreen.Core.Models;
using Microsoft.TeamFoundation.Build.Client;

namespace TeamBuildScreen.Tfs2010.Models
{
    public class BuildInfo : IBuildInfo
    {
        private IBuildDetail buildDetail;
        private int? testsFailed;
        private int? testsPassed;
        private int? testsTotal;

        public BuildInfo(IBuildDetail buildDetail, string flavour, string platform)
        {
            this.buildDetail = buildDetail;

            IConfigurationSummary configurationSummary = InformationNodeConverters.GetConfigurationSummary(this.buildDetail, flavour, platform);

            if (configurationSummary != null)
            {
                if (configurationSummary.TestSummaries.Count > 0)
                {
                    ITestSummary testSummary = configurationSummary.TestSummaries[0];

                    this.testsFailed = testSummary.TestsFailed;
                    this.testsPassed = testSummary.TestsPassed;
                    this.testsTotal = testSummary.TestsTotal;
                }

                if (configurationSummary.CompilationSummaries.Count > 0)
                {
                    ICompilationSummary compilationSummary = configurationSummary.CompilationSummaries[0];

                    this.HasWarnings = compilationSummary.CompilationWarnings > 0;
                }
            }
        }

        #region IBuildInfo Members

        public bool BuildFinished
        {
            get
            {
                return this.buildDetail.BuildFinished;
            }
        }

        public DateTime FinishTime
        {
            get
            {
                return this.buildDetail.FinishTime;
            }
        }

        public TeamBuildScreen.Core.Models.BuildStatus Status
        {
            get
            {
                return BuildStatusConverter.Convert(this.buildDetail.Status);
            }
        }

        public string RequestedFor
        {
            get
            {
                return this.buildDetail.RequestedFor;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.buildDetail.StartTime;
            }
        }

        public int? TestsFailed
        {
            get
            {
                return this.testsFailed;
            }
        }

        public int? TestsPassed
        {
            get
            {
                return this.testsPassed;
            }
        }

        public int? TestsTotal
        {
            get
            {
                return this.testsTotal;
            }
        }

        public bool HasWarnings
        {
            get;
            private set;
        }

        #endregion
    }
}