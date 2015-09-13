using System;
using Microsoft.TeamFoundation.Build.Client;

namespace TeamBuildScreen.Tfs2012.Models
{
	public static class BuildPhaseStatusConverter
	{
		public static Core.Models.BuildPhaseStatus Convert(BuildPhaseStatus status)
		{
			switch (status)
			{
				case BuildPhaseStatus.Failed:
					return Core.Models.BuildPhaseStatus.Failed;
				case BuildPhaseStatus.Succeeded:
					return Core.Models.BuildPhaseStatus.Succeeded;
				case BuildPhaseStatus.Unknown:
					return Core.Models.BuildPhaseStatus.Unknown;
				default:
					throw new NotSupportedException("Unable to convert unrecognised BuildPhaseStatus.");
			}
		}
	}
}