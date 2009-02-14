//-----------------------------------------------------------------------
// <copyright file="BuildServerService.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Models
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.TeamFoundation.Client;
    using System.Collections;
    using System.Windows.Documents;

    #endregion

    /// <summary>
    /// Periodically queries the status of current build definitions against a Team Foundation Server.
    /// </summary>
    public class BuildServerService : IBuildServerService
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
        /// Stores the list of build queues to refresh.
        /// </summary>
        private IList<IQueuedBuildsView> buildQueues;

        /// <summary>
        /// The Team Foundation Server to query.
        /// </summary>
        private IBuildServer buildServer;

        /// <summary>
        /// The interval between queries.
        /// </summary>
        private int period = 30000;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildServerService"/> class. The server will be queried every 30 seconds.
        /// </summary>
        /// <param name="tfsUrl">The URL of the Team Foundation Server to query.</param>
        public BuildServerService(string tfsUrl)
        {
            TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer(tfsUrl);
            this.buildServer = (IBuildServer)tfs.GetService(typeof(IBuildServer));
            this.builds = new Dictionary<IBuildDetailSpec, IBuildDetail>();
            this.buildQueues = new List<IQueuedBuildsView>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildServerService"/> class.
        /// </summary>
        /// <param name="tfsUrl">The URL of the Team Foundation Server to query.</param>
        /// <param name="period">The interval between queries (in milliseconds).</param>
        public BuildServerService(string tfsUrl, int period) : this(tfsUrl)
        {
            this.period = period;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the <see cref="Microsoft.TeamFoundation.Build.Client.IBuildDetail"/> for the build with the specified key.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        /// <returns>The <see cref="Microsoft.TeamFoundation.Build.Client.IBuildDetail"/> for the build with the specified key.</returns>
        public IBuildDetail GetBuildDetail(string key)
        {
            string teamProject;
            string definitionName;

            BuildServerService.ParseBuild(key, out teamProject, out definitionName);

            return this.builds.Single(x =>
                x.Key.DefinitionSpec.Name == definitionName &&
                x.Key.DefinitionSpec.TeamProject == teamProject).Value;
        }

        /// <summary>
        /// Returns a value that indicates whether the build with the specified key has any builds queued.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        /// <returns>true if the specified build has any builds queued, otherwise; false.</returns>
        public bool IsQueued(string key)
        {
            string teamProject;
            string definitionName;

            BuildServerService.ParseBuild(key, out teamProject, out definitionName);

            return this.buildQueues.First(q => q.TeamProject == teamProject).QueuedBuilds.Any(b => b.BuildDefinition.Name == definitionName);
        }

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

                BuildServerService.ParseBuild(key, out teamProject, out definitionName);

                IBuildDetailSpec buildDetailSpec = this.buildServer.CreateBuildDetailSpec(teamProject, definitionName);

                // only interested in the most recently started build
                buildDetailSpec.MaxBuildsPerDefinition = 1;
                buildDetailSpec.QueryOrder = BuildQueryOrder.StartTimeDescending;

                this.builds.Add(buildDetailSpec, null);

                // check if a build queue exists for the team project
                if (!this.buildQueues.Any(q => q.TeamProject == teamProject))
                {
                    IQueuedBuildsView view = this.buildServer.CreateQueuedBuildsView(teamProject);

                    // only interested in queued builds
                    view.StatusFilter = QueueStatus.Queued;

                    this.buildQueues.Add(view);
                }
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
                    // get latest builds
                    results = this.buildServer.QueryBuilds(buildDetailSpecs);

                    // refresh build queueus
                    foreach (IQueuedBuildsView buildQueue in this.buildQueues)
                    {
                        buildQueue.Refresh(false);
                    }
                }
                catch (Exception)
                {
                    this.OnError();

                    return;
                }

                // update the IBuildDetail associated with each IBuildDetailSpec
                foreach (IBuildQueryResult result in results)
                {
                    if (result.Builds.Count() == 0)
                    {
                        continue;
                    }

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
        /// Raises the <see cref="TeamBuildScreenSaver.Models.IBuildServerService.QueryCompleted"/> event.
        /// </summary>
        private void OnQueryCompleted()
        {
            if (this.QueryCompleted != null)
            {
                this.QueryCompleted(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="TeamBuildScreenSaver.Models.IBuildServerService.Error"/> event.
        /// </summary>
        private void OnError()
        {
            if (this.Error != null)
            {
                this.Error(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Parses the team project and definition name from the given string representing a build.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        /// <param name="teamProject">The name of the team project.</param>
        /// <param name="definitionName">The name of the definition.</param>
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