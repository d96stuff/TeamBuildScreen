using System.Text.RegularExpressions;

namespace TeamBuildScreen.Core
{
	public static class StringExtensions
	{
		public static string FilterCapitalsAndNumbers(this string s)
		{
			return Regex.Replace(s, "[^A-Z0-9]+", "", RegexOptions.Compiled);
		}
	}
}

