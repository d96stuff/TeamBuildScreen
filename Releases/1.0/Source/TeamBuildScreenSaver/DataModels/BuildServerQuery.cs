//-----------------------------------------------------------------------
// <copyright file="BuildServerQuery.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.DataModels
{
    #region Usings

    using System;
    using System.Collections;
    using System.Linq;
    using System.Threading;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.Client;

    #endregion

    /// <summary>
    /// Periodically queries the status of current build definitions against a Team Foundation Server.
    /// </summary>
    public class BuildServerQuery : IBuildServerQuery
    {
        #region Fields

        /// <summary>
        /// Controls the timing of queries against the server.
        /// </summary>
        private Timer queryTimer;

        /// <summary>
        /// Stores the list of build definitions to query.
        /// </summary>
        private Hashtable builds;

        /// <summary>
        /// The URL of the Team Foundation Server to query.
        /// </summary>
        private string tfsUrl;

        /// <summary>
        /// The interval between queries.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildServerQuery"/> class. The server will be queried every 30 seconds.
        /// </summary>
        /// <param name="tfsUrl">The URL of the Team Foundation Server to query.</param>
        public BuildServerQuery(string tfsUrl)
        {
            this.tfsUrl = tfsUrl;
            this.builds = new Hashtable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildServerQuery"/> class.
        /// </summary>
        /// <param name="tfsUrl">The URL of the Team Foundation Server to query.</param>
        /// <param name="period">The interval between queries (in milliseconds).</param>
        public BuildServerQuery(string tfsUrl, int period) : this(tfsUrl)
        {
            this.period = period;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the build with the specified key to the query list.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        public void AddBuild(string key)
        {
            lock (this.builds)
            {
                this.builds.Add(key, null);
            }
        }

        /// <summary>
        /// Starts querying the server.
        /// </summary>
        public void Start()
        {
            if (this.queryTimer == null)
            {
                this.queryTimer = new Timer(new TimerCallback(this.Query), null, 0, this.period);
            }
        }

        /// <summary>
        /// Issues a query against the server.
        /// </summary>
        /// <param name="stateInfo"></param>
        private void Query(object stateInfo)
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

        /// <summary>
        /// Raises the <see cref="BuildServerQuery.QueryCompleted"/> event.
        /// </summary>
        private void OnQueryCompleted()
        {
            if (this.QueryCompleted != null)
            {
                this.QueryCompleted(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a query is completed.
        /// </summary>
        public event EventHandler QueryCompleted;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.queryTimer != null)
            {
                this.queryTimer.Dispose();
            }
        }

        #endregion
    }
}