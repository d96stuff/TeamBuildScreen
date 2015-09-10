//-----------------------------------------------------------------------
// <copyright file="BuildServerService.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Threading;
using Microsoft.TeamFoundation;

namespace TeamBuildScreen.Tfs.Models
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.TeamFoundation.Build.Client;
	using Microsoft.TeamFoundation.Client;
	using Microsoft.TeamFoundation.Server;
	using Microsoft.TeamFoundation.TestManagement.Client;
	using Microsoft.TeamFoundation.VersionControl.Client;
	using TeamBuildScreen.Core.Models;

	#endregion

	/// <summary>
	/// Periodically queries the status of current build definitions against a Team Foundation Server.
	/// </summary>
	public class BuildServerService : BuildServerServiceBase, IBuildServerService
	{
		#region Fields

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
		/// Provides access to test runs.
		/// </summary>
		private ITestManagementService testManagementService;

		private ICommonStructureService commonStructureService;

		#endregion

		#region Properties

		public int StaleThreshold
		{
			get;
			private set;
		}

		/// <summary>
		/// Sets the URL of the Team Foundation Server to query.
		/// </summary>
		public string TfsUrl
		{
			set
			{
				var tfs = new TfsTeamProjectCollection(TfsTeamProjectCollection.GetFullyQualifiedUriForName(value));
				this.buildServer = tfs.GetService<IBuildServer>();
				this.testManagementService = tfs.GetService<ITestManagementService>();
				this.commonStructureService = tfs.GetService<ICommonStructureService>();
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="BuildServerService"/> class. The server will be queried every 30 seconds.
		/// </summary>
		public BuildServerService() : base(30000)
		{
			this.Init(7);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BuildServerService"/> class.
		/// </summary>
		/// <param name="period">The interval between queries (in milliseconds).</param>
		/// <param name="staleThreshold">The number of days elapsed before a build is considered 'stale'.</param>
		public BuildServerService(int period, int staleThreshold)
			: base(period)
		{
			this.Init(staleThreshold);
		}

		#endregion

		#region Methods

		private void Init(int staleThreshold)
		{
			this.builds = new Dictionary<IBuildDetailSpec, IBuildDetail>();
			this.buildQueues = new List<IQueuedBuildsView>();
			this.StaleThreshold = staleThreshold;
		}

		/// <summary>
		/// Gets the <see cref="Microsoft.TeamFoundation.Build.Client.IBuildDetail"/> for the build with the specified key.
		/// </summary>
		/// <param name="key">The key of the build definition.</param>
		/// <param name="configuration"></param>
		/// <param name="platform"></param>
		/// <returns>The <see cref="Microsoft.TeamFoundation.Build.Client.IBuildDetail"/> for the build with the specified key.</returns>
		public IBuildInfo GetBuildInfo(string key, string configuration, string platform)
		{
			string teamProject;
			string definitionName;

			ParseBuild(key, out teamProject, out definitionName);

			var buildDetail = this.builds.SingleOrDefault(x =>
				x.Key.DefinitionSpec.Name == definitionName &&
				x.Key.DefinitionSpec.TeamProject == teamProject).Value;

			if (buildDetail == null)
				return BuildInfo.Empty;

			for (int connectionAttempt = 1; connectionAttempt <= 3; connectionAttempt++)
			{
				IBuildInfo buildInfo = CreateBuildInfo(configuration, platform, teamProject, buildDetail);
				if (buildInfo != null)
				{
					return buildInfo;
				}
				Thread.Sleep(100);
			}

			return BuildInfo.Empty;
		}

		private IBuildInfo CreateBuildInfo(string configuration, string platform, string teamProject, IBuildDetail buildDetail)
		{
			try
			{
				var project = this.testManagementService.GetTeamProject(teamProject);
				var testRuns = project.TestRuns.ByBuild(buildDetail.Uri);

				return new TfsBuildInfo(buildDetail, configuration, platform, testRuns, project.CoverageAnalysisManager);
			}
			catch (TeamFoundationServiceUnavailableException)
			{
				return null;
			}
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

			ParseBuild(key, out teamProject, out definitionName);

			return this.buildQueues.First(q => q.TeamProjectFilter == teamProject).QueuedBuilds.Any(b => b.BuildDefinition.Name == definitionName);
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

				ParseBuild(key, out teamProject, out definitionName);

				var buildDetailSpec = this.buildServer.CreateBuildDetailSpec(teamProject, definitionName);

				// only interested in the most recently started build
				buildDetailSpec.MaxBuildsPerDefinition = 1;
				buildDetailSpec.QueryOrder = BuildQueryOrder.StartTimeDescending;
				buildDetailSpec.QueryOptions = QueryOptions.Definitions | QueryOptions.BatchedRequests;
				buildDetailSpec.InformationTypes = null;

				this.builds.Add(buildDetailSpec, null);

				// check if a build queue exists for the team project
				if (this.buildQueues.All(q => q.TeamProjectFilter != teamProject))
				{
					var view = this.buildServer.CreateQueuedBuildsView(teamProject);

					// only interested in queued builds
					view.StatusFilter = QueueStatus.Queued;

					this.buildQueues.Add(view);
				}
			}
		}

		/// <summary>
		/// Removes all builds from the query list.
		/// </summary>
		public void ClearBuilds()
		{
			this.builds.Clear();
		}

		/// <summary>
		/// Loads the available builds from the build server.
		/// </summary>
		/// <param name="buildsToLoad">The collection to populate.</param>
		public void LoadBuilds(ICollection<BuildSetting> buildsToLoad)
		{
			buildsToLoad.Clear();

			var teamProjects = this.commonStructureService.ListAllProjects();

			foreach (var project in teamProjects)
			{
				IBuildDefinition[] projectBuilds = this.buildServer.QueryBuildDefinitions(project.Name);

				foreach (var definition in projectBuilds)
				{
					BuildSetting buildSettingDataModel = new BuildSetting
					{
						DefinitionName = definition.Name,
						TeamProject = project.Name
					};

					buildsToLoad.Add(buildSettingDataModel);
				}
			}
		}

		/// <summary>
		/// Issues a query against the server.
		/// </summary>
		public override void Query()
		{
			if (this.buildServer == null)
			{
				this.OnNotConfigured();
			}

			lock (this.builds)
			{
				var buildDetailSpecs = (from b in this.builds select b.Key).ToArray();

				if (!buildDetailSpecs.Any())
				{
					return;
				}

				IBuildQueryResult[] results;

				try
				{
					// get latest builds
					results = this.buildServer.QueryBuilds(buildDetailSpecs);

					// refresh build queueus
					foreach (var buildQueue in this.buildQueues)
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
				for (int i = 0; i < this.builds.Count; i++)
				{
					KeyValuePair<IBuildDetailSpec, IBuildDetail> build = this.builds.ElementAt(i);

					string teamProject = build.Key.DefinitionSpec.TeamProject;
					string definitionName = build.Key.DefinitionSpec.Name;

					// select the first build that corresponds to this build detail spec, or null
					var result =
						results.FirstOrDefault(
						x => x.Builds.Any(
							b => b.BuildDefinition.TeamProject == teamProject &&
							b.BuildDefinition.Name == definitionName));

					this.builds[build.Key] = result != null ? result.Builds[0] : null;
				}
			}

			this.OnQueryCompleted();
		}

		#endregion
	}
}