using System;
using Microsoft.TeamFoundation.Build.WebApi;

namespace TeamBuildScreen.Tfs.Models
{
	public static class BuildStatusConverter
	{
		public static Core.Models.BuildStatus Convert(BuildStatus? status, BuildResult? result)
		{
			switch (status)
			{
				case BuildStatus.InProgress:
					return Core.Models.BuildStatus.InProgress;
				case BuildStatus.NotStarted:
					return Core.Models.BuildStatus.NotStarted;
				case BuildStatus.Completed:
					return Convert(result);
				default:
					throw new NotSupportedException("Unable to convert unrecognised BuildStatus.");
			}
		}

		private static Core.Models.BuildStatus Convert(BuildResult? result)
		{
			switch (result)
			{
				case BuildResult.Canceled:
					return Core.Models.BuildStatus.Stopped;
				case BuildResult.Failed:
					return Core.Models.BuildStatus.Failed;
				case BuildResult.PartiallySucceeded:
					return Core.Models.BuildStatus.PartiallySucceeded;
				case BuildResult.Succeeded:
					return Core.Models.BuildStatus.Succeeded;
				default:
					throw new NotSupportedException("Unable to convert unrecognised BuildResult.");
			}
		}

		internal static BuildStatus? Convert(Microsoft.TeamFoundation.Build.Client.BuildStatus status)
		{
			switch (status)
			{
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.All:
					return BuildStatus.All;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.Failed:
					return BuildStatus.Completed;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.InProgress:
					return BuildStatus.InProgress;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.None:
					return BuildStatus.None;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.NotStarted:
					return BuildStatus.NotStarted;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.PartiallySucceeded:
					return BuildStatus.Completed;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.Stopped:
					return BuildStatus.Cancelling;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.Succeeded:
					return BuildStatus.Completed;
				default:
					return BuildStatus.None;
			}
		}

		internal static BuildResult? ConvertToBuildResult(Microsoft.TeamFoundation.Build.Client.BuildStatus buildStatus)
		{
			switch (buildStatus)
			{
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.All:
					return BuildResult.Canceled;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.Failed:
					return BuildResult.Failed;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.InProgress:
					return BuildResult.None;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.None:
					return BuildResult.None;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.NotStarted:
					return BuildResult.None;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.PartiallySucceeded:
					return BuildResult.PartiallySucceeded;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.Stopped:
					return BuildResult.Canceled;
				case Microsoft.TeamFoundation.Build.Client.BuildStatus.Succeeded:
					return BuildResult.Succeeded;
				default:
					return BuildResult.None;
			}
		}
	}
}