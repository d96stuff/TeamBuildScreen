//-----------------------------------------------------------------------
// <copyright file="BuildStatusStringConverter.cs" company="Jim Liddell">
//    Copyright © Jim Liddell.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreensaver.Views
{
    #region Usings

    using System;
    using System.Text;
    using System.Windows;
    using System.Windows.Data;
    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    /// <summary>
    /// Converts and instance of <see cref="BuildStatus"/> to an instance of <see cref="System.String"/>.
    /// </summary>
    public class BuildStatusStringConverter : System.Windows.Data.IValueConverter
    {
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the specified <see cref="BuildStatus"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BuildStatus status = (BuildStatus)value;

            switch (status)
            {
                case BuildStatus.InProgress:
                    return "In Progress";
                case BuildStatus.NotStarted:
                    return "Not Started";
                case BuildStatus.PartiallySucceeded:
                    return "Partially Succeeded";
                default:
                    return status.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
