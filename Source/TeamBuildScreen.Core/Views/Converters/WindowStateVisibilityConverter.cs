//-----------------------------------------------------------------------
// <copyright file="BuildStatusStringConverter.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Windows;

namespace TeamBuildScreen.Core.Views.Converters
{
    #region Usings

	

	#endregion

    /// <summary>
    /// Converts and instance of <see cref="WindowState"/> to an instance of <see cref="Visibility"/>.
    /// </summary>
    public class WindowStateVisibilityConverter : System.Windows.Data.IValueConverter
    {
        #region Methods

        /// <summary>
        /// Returns <see cref="Visibility.Visible"/> if the value if equal to <see cref="WindowState.Maximized"/>. Otherwise returns <see cref="Visibility.Hidden"/>.
        /// </summary>
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            WindowState state = (WindowState)value;

            if (state == WindowState.Maximized)
            {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}