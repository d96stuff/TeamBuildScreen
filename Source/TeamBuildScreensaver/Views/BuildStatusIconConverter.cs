//-----------------------------------------------------------------------
// <copyright file="BuildDetailsViewModel.cs" company="Jim Liddell">
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

    public class SuccessVisibilityConverter : BuildStatusVisibilityConverter, System.Windows.Data.IValueConverter
    {
        public SuccessVisibilityConverter() : base(BuildStatus.Succeeded) { }
    }

    public class SuccessOpacityConverter : BuildStatusOpacityConverter, System.Windows.Data.IValueConverter
    {
        public SuccessOpacityConverter() : base(BuildStatus.Succeeded) { }
    }

    public class FailureVisibilityConverter : BuildStatusVisibilityConverter, System.Windows.Data.IValueConverter
    {
        public FailureVisibilityConverter() : base(BuildStatus.Failed) { }
    }

    public class InProgressVisibilityConverter : BuildStatusVisibilityConverter, System.Windows.Data.IValueConverter
    {
        public InProgressVisibilityConverter() : base(BuildStatus.InProgress) { }
    }

    public class PartialSuccessVisibilityConverter : BuildStatusVisibilityConverter, System.Windows.Data.IValueConverter
    {
        public PartialSuccessVisibilityConverter() : base(BuildStatus.PartiallySucceeded) { }
    }

    public class BuildStatusVisibilityConverter
    {
        private BuildStatus visibleStatus;

        public BuildStatusVisibilityConverter(BuildStatus visibleStatus)
        {
            this.visibleStatus = visibleStatus;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BuildStatus status = (BuildStatus)value;

            if (status == this.visibleStatus)
            {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class BuildStatusOpacityConverter
    {
        private BuildStatus visibleStatus;

        public BuildStatusOpacityConverter(BuildStatus visibleStatus)
        {
            this.visibleStatus = visibleStatus;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BuildStatus status = (BuildStatus)value;

            if (status == this.visibleStatus)
            {
                return 1.0;
            }

            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class BuildStatusColorConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BuildStatus status = (BuildStatus)value;

            if (status == BuildStatus.Succeeded)
            {
                // #802B7720
                return System.Windows.Media.Color.FromArgb(128, 43, 119, 42);
            }
            else if (status == BuildStatus.InProgress)
            {
                // #80717171
                return System.Windows.Media.Color.FromArgb(128, 113, 113, 113);
            }
            else
            {
                // #80CF2C00
                return System.Windows.Media.Color.FromArgb(128, 207, 44, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class BuildStatusStringConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BuildStatus status = (BuildStatus)value;

            switch (status)
            {
                case BuildStatus.InProgress:
                    return "In Progress";
                    break;
                case BuildStatus.NotStarted:
                    return "Not Started";
                    break;
                case BuildStatus.PartiallySucceeded:
                    return "Partially Succeeded";
                    break;
                default:
                    return status.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class BuildDetailStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            StringBuilder text = new StringBuilder();

            text.AppendLine(string.Format("{0}.{1}", values[0].ToString(), values[1].ToString()));
            text.AppendLine(new BuildStatusStringConverter().Convert(values[2], typeof(string), null, null).ToString());
            text.AppendLine("Requested by " + values[3].ToString());
            text.AppendLine("Started on " + values[4].ToString());
            text.AppendLine("Completed on " + values[5].ToString());

            IBuildDetail build = values[6] as IBuildDetail;

            IConfigurationSummary configurationSummary = InformationNodeConverters.GetConfigurationSummary(build, "Release", "Any CPU");

            if (configurationSummary != null && configurationSummary.TestSummaries.Count > 0)
            {
                ITestSummary summary = configurationSummary.TestSummaries[0];
                text.AppendLine(string.Format("Test results: {0} passed, {1} failed, {2} total.", summary.TestsPassed, summary.TestsFailed, summary.TestsTotal));
            }
            else
            {
                text.AppendLine("No test result.");
            }

            return text.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("Not supported");
        }
    }
}