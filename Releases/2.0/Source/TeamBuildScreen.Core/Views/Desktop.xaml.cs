//-----------------------------------------------------------------------
// <copyright file="Desktop.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel;

namespace TeamBuildScreen.Core.Views
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for Desktop.xaml
    /// </summary>
    public partial class Desktop : Window
    {
        private bool maximizing = false;

        public Desktop(object viewModel)
        {
            InitializeComponent();

            this.StateChanged += new EventHandler(this.OnStateChanged);
            this.DataContext = viewModel;
        }

        public void Restore()
        {
            this.OnRestore(this, new RoutedEventArgs());
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized && !this.maximizing)
            {
                this.maximizing = true;

                // flick the WindowState to Normal and back to Maximised to make sure it 
                // appears over the TaskBar.
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.None;
                this.Topmost = true;
                this.WindowState = WindowState.Maximized;
                this.ResizeMode = ResizeMode.NoResize;

                this.maximizing = false;
            }
        }

        private void OnRestore(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
            this.Topmost = false;
        }

        private void OnMinimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Settings.Default.Height = Height;
            Settings.Default.Width = Width;
            Settings.Default.Top = Top;
            Settings.Default.Left = Left;
            Settings.Default.State = WindowState;
            Settings.Default.Save();

            base.OnClosing(e);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Height = Settings.Default.Height;
            Width = Settings.Default.Width;
            Top = Settings.Default.Top;
            Left = Settings.Default.Left;
            WindowState = Settings.Default.State;
        }
    }
}