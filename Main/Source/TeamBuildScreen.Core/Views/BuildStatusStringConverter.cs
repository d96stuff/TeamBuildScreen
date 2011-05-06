//-----------------------------------------------------------------------
// <copyright file="BuildStatusStringConverter.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Views
{
    #region Usings

    using System;
    using TeamBuildScreen.Core.Models;

    #endregion

    /// <summary>
    /// Converts and instance of <see cref="BuildStatus"/> to an instance of <see cref="System.String"/>.
    /// </summary>
    public class BuildStatusStringConverter : System.Windows.Data.IValueConverter
    {
        #region Methods

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the specified <see cref="BuildStatus"/>.
        /// </summary>
        /// <inheritdoc/>
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
                case BuildStatus.Loading:
                    return "Loading...";
                case BuildStatus.NoneFound:
                    return "No build(s) found.";
                default:
                    return status.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}