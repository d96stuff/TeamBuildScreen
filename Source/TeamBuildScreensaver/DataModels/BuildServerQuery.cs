//-----------------------------------------------------------------------
// <copyright file="BuildServerQuery.cs" company="Jim Liddell">
//    Copyright © Jim Liddell.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreensaver.DataModels
{
    #region Usings

    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.Client;

    #endregion

    public class BuildServerQuery : IBuildServerQuery
    {
        #region Fields

        private Timer backgroundTimer;
        private Hashtable builds;
        private string tfsUrl;
        private int period = 30000;

        #endregion

        #region Indexer

        public IBuildDetail this[string key]
        {
            get
            {
                return this.builds[key] as IBuildDetail;
            }
        }

        #endregion

        #region Constructors

        public BuildServerQuery(string tfsUrl)
        {
            this.tfsUrl = tfsUrl;
            this.builds = new Hashtable();
        }

        public BuildServerQuery(string tfsUrl, int period) : this(tfsUrl)
        {
            this.period = period;
        }

        #endregion

        #region Methods

        public void AddBuild(string key)
        {
            lock (this.builds)
            {
                this.builds.Add(key, null);
            }
        }

        public void Start()
        {
            if (this.backgroundTimer == null)
            {
                this.backgroundTimer = new Timer(new TimerCallback(this.backgroundTimer_TimerCallback), null, 0, this.period);
            }
        }

        private void backgroundTimer_TimerCallback(Object stateInfo)
        {
            TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer(this.tfsUrl);

            IBuildServer buildServer = (IBuildServer)tfs.GetService(typeof(IBuildServer));
            Hashtable latestBuilds = new Hashtable();

            lock (this.builds)
            {
                foreach (string key in this.builds.Keys)
                {
                    string[] build = key.Split(';');
                    string teamProject = build[0];
                    string definitionName = build[1];

                    IBuildDefinitionSpec spec = buildServer.CreateBuildDefinitionSpec(teamProject, definitionName);

                    IBuildDetail[] builds = buildServer.QueryBuilds(spec);

                    if (builds.Length > 0)
                    {
                        IBuildDetail latestBuild = builds.Last();

                        latestBuilds.Add(key, latestBuild);
                    }
                }

                foreach (string key in latestBuilds.Keys)
                {
                    this.builds[key] = latestBuilds[key];
                }
            }

            this.OnQueryCompleted();
        }

        private void OnQueryCompleted()
        {
            if (this.QueryCompleted != null)
            {
                this.QueryCompleted(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Events

        public event EventHandler QueryCompleted;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.backgroundTimer != null)
            {
                this.backgroundTimer.Dispose();
            }
        }

        #endregion
    }
}