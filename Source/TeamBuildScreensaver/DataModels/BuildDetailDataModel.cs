//-----------------------------------------------------------------------
// <copyright file="BuildDetailsDataModel.cs" company="Jim Liddell">
//    Copyright © Jim Liddell.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreensaver.DataModels
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Windows.Threading;
    using Microsoft.TeamFoundation.Build.Client;
using TeamBuildScreensaver.ViewModels;

    #endregion

    public class BuildDetailDataModel : INotifyPropertyChanged
    {
        #region Fields

        private IBuildServerQuery serverQuery;
        private IBuildDetail model;
        private string key;

        #endregion

        #region Properties

        public IBuildDetail Model
        {
            get
            {
                return this.model;
            }
            private set
            {
                this.model = value;

                this.OnPropertyChanged("Model");
            }
        }

        #endregion

        #region Constructors

        public BuildDetailDataModel(string teamProject, string definitionName, IBuildServerQuery serverQuery)
        {
            this.key = string.Format("{0};{1}", teamProject, definitionName);
            this.serverQuery = serverQuery;

            this.serverQuery.AddBuild(this.key);
            this.serverQuery.QueryCompleted += query_QueryCompleted;
        }

        #endregion

        #region Methods

        private void query_QueryCompleted(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.DataBind,
                new Action(delegate
                    {
                        IBuildDetail model = this.serverQuery[this.key];

                        if (model != null)
                        {
                            this.Model = model;
                        }
                    }));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}