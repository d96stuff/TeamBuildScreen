//-----------------------------------------------------------------------
// <copyright file="ScreenSaver.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Input;

    #endregion

    /// <summary>
    /// Provides an implementation of a screen saver following the Model View-Model Model pattern where 
    /// <typeparamref name="V"/> represents the View, and <typeparamref name="VM"/> represents the View-Model.
    /// </summary>
    /// <typeparam name="V">The type used as the View.</typeparam>
    /// <typeparam name="VM">
    /// The type used as the View-Model. This is set as the <see cref="FrameworkElement.DataContext"/> 
    /// property of each View created.
    /// </typeparam>
    public class ScreenSaver<V, VM> : IDisposable where V : Window, new()
    {
        #region Fields

        /// <summary>
        /// Provides a host for displaying WPF content within a Win32 control.
        /// </summary>
        private HwndSource hostWindow;
        private IList<V> windows;
        private VM dataContext;

        #endregion

        #region Constructors

        public ScreenSaver(VM dataContext)
        {
            this.windows = new List<V>();
            this.dataContext = dataContext;
        }

        #endregion

        #region Methods

        public void Show()
        {
            System.Windows.Forms.Screen[] screens = System.Windows.Forms.Screen.AllScreens;

            foreach (System.Windows.Forms.Screen screen in screens)
            {
                this.CreateWindow(screen, false);
            }

            foreach (V window in this.windows)
            {
                window.Show();
            }
        }

        public void ShowPreview(int previewHandle)
        {
            this.ShowPreview(previewHandle, "LayoutRoot");
        }

        public void ShowPreview(int previewHandle, string rootVisualPropertyName)
        {
            V preview = this.CreateWindow(System.Windows.Forms.Screen.PrimaryScreen, true);

            IntPtr previewPointer = new IntPtr(previewHandle);

            Interop.Rectangle clientRectange = new Interop.Rectangle();
            bool result = Interop.Win32Api.GetClientRect(previewPointer, ref clientRectange);

            HwndSourceParameters sourceParams = new HwndSourceParameters("sourceParams");
            sourceParams.PositionX = 0;
            sourceParams.PositionY = 0;
            sourceParams.Height = clientRectange.Bottom - clientRectange.Top;
            sourceParams.Width = clientRectange.Right - clientRectange.Left;
            sourceParams.ParentWindow = previewPointer;
            sourceParams.WindowStyle = (int)(Interop.WindowStyles.WindowStyleVisible | Interop.WindowStyles.WindowStyleChild | Interop.WindowStyles.WindowStyleClipChildren);

            this.hostWindow = new HwndSource(sourceParams);
            this.hostWindow.Disposed += OnHostWindowDisposed;
            this.hostWindow.RootVisual = (Visual)preview.FindName(rootVisualPropertyName);
        }

        private V CreateWindow(System.Windows.Forms.Screen display, bool showCursor)
        {
            V window = new V();
            window.WindowStartupLocation = WindowStartupLocation.Manual;
            window.DataContext = dataContext;
            window.Closed += this.OnWindowClosed;
            window.Left = display.Bounds.X;
            window.Top = display.Bounds.Y;
            window.Width = display.Bounds.Width;
            window.Height = display.Bounds.Height;
            window.ResizeMode = ResizeMode.NoResize;
            window.WindowStyle = WindowStyle.None;

            if (!showCursor)
            {
                window.Cursor = Cursors.None;
            }

            this.windows.Add(window);

            return window;
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnHostWindowDisposed(object sender, EventArgs e)
        {
            foreach (V window in this.windows)
            {
                window.Close();
            }

            Application.Current.Shutdown();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.hostWindow != null)
            {
                this.hostWindow.Dispose();
            }
        }

        #endregion
    }
}