using System.ComponentModel;

namespace TeamBuildScreen.Core.Models
{
	public enum TeamProjectNameFormat
	{
		[Description("None")]
		None = 0,
		[Description("Short")]
		Short = 1,
		[Description("Full")]
		Full = 2
	}
}