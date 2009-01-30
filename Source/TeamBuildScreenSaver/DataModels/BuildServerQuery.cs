//-----------------------------------------------------------------------
// <copyright file="BuildServerQuery.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.DataModels
{
    #region Usings

    using System;
    using System.Collections.Generic;
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
        private IDictionary<IBuildDetailSpec, IBuildDetail> builds;

        /// <summary>
        /// The Team Foundation Server to query.
        /// </summary>
        private IBuildServer buildServer;

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
                string teamProject;
                string definitionName;

                BuildServerQuery.ParseBuild(key, out teamProject, out definitionName);

                return this.builds.Single(x =>
                    x.Key.DefinitionSpec.Name == definitionName &&
                    x.Key.DefinitionSpec.TeamProject == teamProject).Value;
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
            TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer(tfsUrl);
            this.buildServer = (IBuildServer)tfs.GetService(typeof(IBuildServer));
            this.builds = new Dictionary<IBuildDetailSpec, IBuildDetail>();
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
                string teamProject;
                string definitionName;

                BuildServerQuery.ParseBuild(key, out teamProject, out definitionName);

                IBuildDetailSpec spec = this.buildServer.CreateBuildDetailSpec(teamProject, definitionName);
                spec.MaxBuildsPerDefinition = 1;
                spec.QueryOrder = BuildQueryOrder.FinishTimeDescending;

                this.builds.Add(spec, null);
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
            lock (this.builds)
            {
                IBuildDetailSpec[] buildDetailSpecs = (from b in this.builds select b.Key).ToArray();

                if (buildDetailSpecs.Count() == 0)
                {
                    return;
                }

                IBuildQueryResult[] results;

                try
                {
                    results = this.buildServer.QueryBuilds(buildDetailSpecs);
                }
                catch (Exception)
                {
                    this.OnError();

                    return;
                }

                foreach (IBuildQueryResult result in results)
                {
                    IBuildDetail detail = result.Builds[0];

                    string teamProject = detail.BuildDefinition.TeamProject;
                    string definitionName = detail.BuildDefinition.Name;

                    IBuildDetailSpec spec =
                        this.builds.Keys.Single(x =>
                            x.DefinitionSpec.Name == definitionName &&
                            x.DefinitionSpec.TeamProject == teamProject);

                    this.builds[spec] = detail;
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

        /// <summary>
        /// Raises the <see cref="BuildServerQuery.Error"/> event.
        /// </summary>
        private void OnError()
        {
            if (this.Error != null)
            {
                this.Error(this, EventArgs.Empty);
            }
        }

        private static void ParseBuild(string key, out string teamProject, out string definitionName)
        {
            string[] build = key.Split(';');
            
            teamProject = build[0];
            definitionName = build[1];
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a query is completed.
        /// </summary>
        public event EventHandler QueryCompleted;

        /// <summary>
        /// Occurs when an error occurs.
        /// </summary>
        public event EventHandler Error;

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