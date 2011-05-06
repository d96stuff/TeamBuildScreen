using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuildScreen.Core.Models;
using Microsoft.TeamFoundation.Build.Client;

namespace TeamBuildScreen.Tfs2010.Models
{
    public class BuildInfo : BuildInfoBase, IBuildInfo
    {
        private IBuildDetail buildDetail;

        public BuildInfo(IBuildDetail buildDetail, string flavour, string platform)
        {
            this.buildDetail = buildDetail;

            if (this.buildDetail != null)
            {
                IConfigurationSummary configurationSummary = InformationNodeConverters.GetConfigurationSummary(this.buildDetail, flavour, platform);

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
        }

        #region IBuildInfo Members

        public bool BuildFinished
        {
            get
            {
                if (this.buildDetail != null)
                {
                    return this.buildDetail.BuildFinished;
                }
                else
                {
                    return false;
                }
            }
        }

        public DateTime FinishTime
        {
            get
            {
                if (this.buildDetail != null)
                {
                    return this.buildDetail.FinishTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public TeamBuildScreen.Core.Models.BuildStatus Status
        {
            get
            {
                if (this.buildDetail != null)
                {
                    return BuildStatusConverter.Convert(this.buildDetail.Status);
                }
                else
                {
                    return TeamBuildScreen.Core.Models.BuildStatus.NotStarted;
                }
            }
        }

        public string RequestedFor
        {
            get
            {
                if (this.buildDetail != null)
                {
                    return this.buildDetail.RequestedFor;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public DateTime StartTime
        {
            get
            {
                if (this.buildDetail != null)
                {
                    return this.buildDetail.StartTime;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        #endregion
    }
}