//-----------------------------------------------------------------------
// <copyright file="DesktopViewModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.ViewModels
{
    #region Usings

    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using TeamBuildScreen.Core.Models;

    #endregion

    public class DesktopViewModel : BuildGridViewModel
    {
        public ICommand RequestConfig
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the icon for the <see cref="BuildGridViewModel"/>.
        /// </summary>
        /// <remarks>The icon reflects the overall status of all the builds.</remarks>
        public ImageSource SummaryIcon
        {
            get
            {
                string uriString;

                if (this.Builds.Any(b => b.Status == BuildStatus.InProgress))
                {
                    uriString = @"pack://application:,,/TeamBuildScreen.Core;component/InProgress.ico";
                }
                else if (this.Builds.Any(b => b.Status == BuildStatus.NotStarted))
                {
                    uriString = @"pack://application:,,/TeamBuildScreen.Core;component/NotStarted.ico";
                }
                else if (this.Builds.Any(b => b.Status == BuildStatus.Stopped))
                {
                    uriString = @"pack://application:,,/TeamBuildScreen.Core;component/Stopped.ico";
                }
                else if (this.Builds.Any(b => b.Status == BuildStatus.Failed))
                {
                    uriString = @"pack://application:,,/TeamBuildScreen.Core;component/Failed.ico";
                }
                else if (this.Builds.Any(b => b.Status == BuildStatus.PartiallySucceeded))
                {
                    uriString = @"pack://application:,,/TeamBuildScreen.Core;component/PartiallySucceeded.ico";
                }
                else if (this.Builds.Any(b => b.Status == BuildStatus.Succeeded))
                {
                    uriString = @"pack://application:,,/TeamBuildScreen.Core;component/Succeeded.ico";
                }
                else
                {
                    uriString = @"pack://application:,,/TeamBuildScreen.Core;component/Unknown.ico";
                }

                IconBitmapDecoder decoder = new IconBitmapDecoder(
                    new Uri(uriString, UriKind.RelativeOrAbsolute),
                    BitmapCreateOptions.None,
                    BitmapCacheOption.Default);

                return decoder.Frames[0];
            }
        }

        public DesktopViewModel(IBuildServerService service, StringCollection builds) : base(service, builds)
        {
            this.RequestConfig = new RelayCommand(x => this.OnRequestConfigExecuted(x), x => this.OnRequestConfigCanExecute(x));

            service.QueryCompleted += new EventHandler(OnQueryCompleted);
        }

        private void OnQueryCompleted(object sender, EventArgs e)
        {
            this.OnPropertyChanged("SummaryIcon");
        }

        private bool OnRequestConfigCanExecute(object sender)
        {
            return true;
        }

        private void OnRequestConfigExecuted(object sender)
        {
            if (this.ConfigRequested != null)
            {
                this.ConfigRequested(this, EventArgs.Empty);
            }
        }

        public event EventHandler ConfigRequested;
    }
}