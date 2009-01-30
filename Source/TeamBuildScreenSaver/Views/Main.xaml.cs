//-----------------------------------------------------------------------
// <copyright file="Main.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Views
{
    #region Usings

    using System.Collections.Specialized;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using TeamBuildScreenSaver.DataModels;
    using TeamBuildScreenSaver.ViewModels;

    #endregion

    public partial class Main
    {
        #region Fields

        private bool isInteractive = true;

        #endregion

        #region Properties

        public double InnerMargin
        {
            set
            {
                foreach (UIElement element in this.BuildGrid.Children)
                {
                    Viewbox viewbox = element as Viewbox;

                    if (viewbox != null)
                    {
                        viewbox.Margin = new Thickness(value);
                    }
                }
            }
        }

        public bool IsInteractive
        {
            set
            {
                this.isInteractive = value;
            }
        }

        public int Columns
        {
            set
            {
                this.BuildGrid.Columns = value;
            }
        }

        #endregion

        #region Constructors

        public Main(IBuildServerQuery serverQuery, StringCollection builds)
        {
            this.InitializeComponent();

            this.InitializeBuildPanels(serverQuery, builds, null);
        }

        public Main(IBuildServerQuery serverQuery, StringCollection builds, ConfigurationSummaryHandler configurationSummaryHandler)
        {
            this.InitializeComponent();

            this.InitializeBuildPanels(serverQuery, builds, configurationSummaryHandler);
        }

        private void InitializeBuildPanels(IBuildServerQuery serverQuery, StringCollection builds, ConfigurationSummaryHandler configurationSummaryHandler)
        {
            foreach (string build in builds)
            {
                string[] buildParts = build.Split(';');
                string teamProject = buildParts[0];
                string definitionName = buildParts[1];
                string configuration = buildParts[2];
                string platform = buildParts[3];

                BuildDetailDataModel dataModel = new BuildDetailDataModel(teamProject, definitionName, configuration, platform, serverQuery);

                BuildPanel panel;

                if (configurationSummaryHandler == null)
                {
                    panel = new BuildPanel(dataModel);
                }
                else
                {
                    panel = new BuildPanel(dataModel, configurationSummaryHandler);
                }

                Viewbox viewbox = new Viewbox();
                viewbox.Child = panel;
                viewbox.Margin = new Thickness(8);

                this.BuildGrid.Children.Add(viewbox);
            }

            this.Message.DataContext = new BuildServerQueryMessageViewModel(serverQuery);
        }

        #endregion

        #region Methods

        private void Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.isInteractive)
            {
                Application.Current.Shutdown();
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.isInteractive)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion
    }
}