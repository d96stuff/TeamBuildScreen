using System;
using System.Windows.Data;
using TeamBuildScreen.Core.Models;

namespace TeamBuildScreen.Core.Views.Converters
{
	public class MoveUpConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			BuildSetting build = value as BuildSetting;
			if (build == null)
			{
				return false;
			}

			return build.OrderNo > 0 && build.IsEnabled;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			BuildSetting build = value as BuildSetting;
			if (build == null)
			{
				return false;
			}

			return build.OrderNo > 0 && build.IsEnabled;
		}
	}
}
