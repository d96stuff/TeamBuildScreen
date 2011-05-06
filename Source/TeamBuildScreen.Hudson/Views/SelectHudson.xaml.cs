//-----------------------------------------------------------------------
// <copyright file="Main.xaml.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Views
{
    public partial class SelectHudson
    {
        #region Constructors

        public SelectHudson(object viewModel)
        {
            this.InitializeComponent();

            this.DataContext = viewModel;
        }

        #endregion
    }
}