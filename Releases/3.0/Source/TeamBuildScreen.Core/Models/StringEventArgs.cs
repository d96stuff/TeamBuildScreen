using System;

namespace TeamBuildScreen.Core.Models
{
	/// <summary>
	/// Event arguments that pass along a string value.
	/// </summary>
	public class StringEventArgs : EventArgs
	{
		public StringEventArgs(string value)
		{
			Value = value;
		}

		public string Value { get; private set; }
	}
}
