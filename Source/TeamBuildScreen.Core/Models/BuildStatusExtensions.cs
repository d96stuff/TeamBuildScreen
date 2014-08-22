using System;

namespace TeamBuildScreen.Core.Models
{
	public static class BuildStatusExtensions
	{
		public static string ToFriendlyString(this BuildStatus buildStatus)
		{
			switch (buildStatus)
			{
				case BuildStatus.Stopped:
					return "Stopped";
				case BuildStatus.Succeeded:
					return "Succeeded";
				case BuildStatus.Failed:
					return "Failed";
				case BuildStatus.InProgress:
					return "Started";
				case BuildStatus.NotStarted:
					return "Not started";
				case BuildStatus.PartiallySucceeded:
					return "Partially succeeded";
				case BuildStatus.Loading:
					return "Loading...";
				case BuildStatus.NoneFound:
					return "No build(s) found.";
				default:
					throw new ArgumentOutOfRangeException("buildStatus");
			}
		}
	}
}
