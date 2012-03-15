//-----------------------------------------------------------------------
// <copyright file="BuildStatusStringConverter.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Views
{
    #region Usings

    using System;
    using System.Globalization;
    using System.Threading;
    using TeamBuildScreen.Core.Models;

    #endregion

    public class DateTimeStringConverter : System.Windows.Data.IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime? time = (DateTime?)value;

            if (time.HasValue)
            {
                return time.Value.ToString(new CultureInfo(Thread.CurrentThread.CurrentCulture.Name));
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}