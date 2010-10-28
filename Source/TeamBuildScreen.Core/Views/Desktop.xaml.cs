namespace TeamBuildScreen.Core.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for Desktop.xaml
    /// </summary>
    public partial class Desktop : Window
    {
        private bool fullScreening = false;

        public Desktop()
        {
            InitializeComponent();

            this.StateChanged += new EventHandler(this.OnStateChanged);
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized && !this.fullScreening)
            {
                this.fullScreening = true;

                // flick the WindowState to Normal and back to Maximised to make sure it 
                // appears over the TaskBar.
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.None;
                this.Topmost = true;
                this.WindowState = WindowState.Maximized;
                this.ResizeMode = ResizeMode.NoResize;

                this.fullScreening = false;
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
    }
}