//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreensaver
{
    #region Usings

    using System;
    using System.Windows;
    using System.Windows.Interop;
    using TeamBuildScreensaver.Views;
    using System.Collections.Specialized;
    using Microsoft.TeamFoundation.Client;
    using TeamBuildScreensaver.DataModels;
using Microsoft.TeamFoundation.Build.Client;
    using System.Windows.Threading; 

    #endregion

	public partial class App: System.Windows.Application
    {
        #region Fields

        private HwndSource winWPFContent;
        private Main main;
        private IBuildServerQuery serverQuery;

        #endregion

        #region Methods

        private void Application_Startup(object sender, StartupEventArgs e)   
        {
            string tfsUri = Settings.Default.TfsUri;
            StringCollection builds = Settings.Default.Builds;

            if (string.IsNullOrEmpty(tfsUri))
            {
                // error condition
                Application.Current.Shutdown();
            }

            TeamFoundationServer tfsServer = new TeamFoundationServer(tfsUri);

            this.serverQuery = new BuildServerQuery(tfsServer.Uri.AbsoluteUri);

            tfsServer.Dispose();

            this.main = new Main(this.serverQuery, builds);
            this.main.Columns = Settings.Default.Columns;
            this.main.Closed += new EventHandler(main_Closed);

            if (e.Args.Length == 0)
            {
                // debugging
                this.main.Show();
                try
                {
                    this.serverQuery.Start();
                }
                catch (Exception ex)
                {
                    Application_DispatcherUnhandledException(this, null);
                }
            }
            else if (e.Args[0].ToLower().StartsWith("/p"))       
            {   
                // preview window

                this.main.InnerMargin = 1;
                // set this property so that the preview window does not close when clicked on
                this.main.IsInteractive = false;
  
                Int32 previewHandle = Convert.ToInt32(e.Args[1]);   
  
                IntPtr pPreviewHnd = new IntPtr(previewHandle);   
  
                RECT lpRect = new RECT();   
                bool bGetRect = Win32API.GetClientRect(pPreviewHnd, ref lpRect);   
  
                HwndSourceParameters sourceParams = new HwndSourceParameters("sourceParams");   
  
                sourceParams.PositionX = 0;   
                sourceParams.PositionY = 0;   
                sourceParams.Height = lpRect.Bottom - lpRect.Top;   
                sourceParams.Width = lpRect.Right - lpRect.Left;   
                sourceParams.ParentWindow = pPreviewHnd;   
                sourceParams.WindowStyle = (int)(WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD | WindowStyles.WS_CLIPCHILDREN);   
  
                winWPFContent = new HwndSource(sourceParams);   
                winWPFContent.Disposed += new EventHandler(winWPFContent_Disposed);
                winWPFContent.RootVisual = this.main.LayoutRoot;
                this.serverQuery.Start();
            }   
            else if (e.Args[0].ToLower().StartsWith("/s"))
            {
                // normal mode (either preview or otherwise)
                this.main.Show();
                this.serverQuery.Start();
            } 
            else if (e.Args[0].ToLower().StartsWith("/c"))        
            {
                // configuration mode
                EditSettings editSettings = new EditSettings();
                editSettings.Show();
            }   
            else
            {
                // unknown mode
                Application.Current.Shutdown();   
            }
        }

        private void main_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }   

        private void winWPFContent_Disposed(object sender, EventArgs e)
        {
            this.main.Close();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (this.serverQuery != null)
            {
                this.serverQuery.Dispose();
            }
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An error has been detected in the application that has caused the application to shutdown.");

            e.Handled = true;

            Application.Current.Shutdown();
        }

        #endregion
    }
}