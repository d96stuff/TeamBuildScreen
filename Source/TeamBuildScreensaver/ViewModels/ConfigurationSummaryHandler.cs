using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Build.Client;

namespace TeamBuildScreensaver.ViewModels
{
    public delegate IConfigurationSummary ConfigurationSummaryHandler(IBuildDetail build, string flavour, string platform);
}
