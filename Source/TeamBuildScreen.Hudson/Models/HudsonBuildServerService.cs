﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using TeamBuildScreen.Core.Models;

namespace TeamBuildScreen.Hudson.Models
{
    using TeamBuildScreen.Hudson.Models.Tasks.JUnit;

    public class HudsonBuildServerService : BuildServerServiceBase, IBuildServerService
    {
        private IDictionary<string, IBuildInfo> builds;
        private Hudson hudson;
        private string hudsonUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="HudsonBuildServerService"/> class. The server will be queried every 30 seconds.
        /// </summary>
        public HudsonBuildServerService()
            : base(30000)
        {
            this.Init(7);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HudsonBuildServerService"/> class.
        /// </summary>
        /// <param name="period">The interval between queries (in milliseconds).</param>
        /// <param name="staleThreshold">The number of days elapsed before a build is considered 'stale'.</param>
        public HudsonBuildServerService(int period, int staleThreshold)
            : base(period)
        {
            this.Init(staleThreshold);
        }

        private void Init(int staleThreshold)
        {
            this.builds = new Dictionary<string, IBuildInfo>();
            this.StaleThreshold = staleThreshold;
        }

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
                if (!string.IsNullOrEmpty(value))
                {
                    var client = new WebClient();
                    var hudsonString = client.DownloadString(value + "api/xml");
                    var reader = new StringReader(hudsonString);
                    this.hudson = new Hudson();
                    var serializer = new XmlSerializer(hudson.GetType());
                    this.hudson = (Hudson)serializer.Deserialize(reader);
                    this.hudsonUri = value;
                }
            }
        }

        #endregion

        protected override void Query(object stateInfo)
        {
            if (this.hudson == null)
            {
                this.OnNotConfigured();
            }

            lock (this.builds)
            {
                var keys = this.builds.Keys.ToArray();

                // update the IBuildInfo associated with each build
                foreach (var name in keys)
                {
                    var buildInfo = this.GetBuildInfo(name);

                    this.builds[name] = buildInfo;
                }
            }

            this.OnQueryCompleted();
        }

        private HudsonBuildInfo GetBuildInfo(string name)
        {
            var build = this.GetBuild(name);
            var testResults = this.GetTestResultsForBuild(name, build.Number);

            return new HudsonBuildInfo(build, testResults);
        }

        private FreeStyleBuild GetBuild(string name)
        {
            var client = new WebClient();
            var buildString = client.DownloadString(this.hudsonUri + "job/" + Uri.EscapeUriString(name) + "/lastBuild/api/xml");
            var reader = new StringReader(buildString);
            var build = new FreeStyleBuild();
            var serializer = new XmlSerializer(build.GetType());
            build = (FreeStyleBuild)serializer.Deserialize(reader);

            return build;
        }

        private TestResult GetTestResultsForBuild(string name, int number)
        {
            WebRequest request = HttpWebRequest.Create(this.hudsonUri + "job/" + Uri.EscapeUriString(name) + "/" + number + "/testReport/api/xml");
            request.Method = "GET";
            request.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var stream = response.GetResponseStream();
                        var reader = new StreamReader(stream);
                        var testResult = new TestResult();
                        var serializer = new XmlSerializer(testResult.GetType());
                        testResult = (TestResult)serializer.Deserialize(reader);

                        return testResult;
                    }
                }
            }
            catch (WebException) { }

            return null;
        }

        public void AddBuild(string key)
        {
            lock (this.builds)
            {
                string teamProject;
                string definitionName;

                ParseBuild(key, out teamProject, out definitionName);

                this.builds.Add(definitionName, this.GetBuildInfo(definitionName));
            }
        }

        /// <summary>
        /// Removes all builds from the query list.
        /// </summary>
        public void ClearBuilds()
        {
            this.builds.Clear();
        }

        public IBuildInfo GetBuildInfo(string key, string configuration, string platform)
        {
            string teamProject;
            string definitionName;

            ParseBuild(key, out teamProject, out definitionName);

            if (this.builds.ContainsKey(definitionName))
            {
                return this.builds[definitionName];
            }

            return BuildInfo.Empty;
        }

        public bool IsQueued(string key)
        {
            // TODO: Implement
            return false;
        }

        public void LoadBuilds(ICollection<BuildSetting> builds)
        {
            builds.Clear();

            foreach (var job in this.hudson.Job.Cast<Job>())
            {
                builds.Add(new BuildSetting { DefinitionName = job.Name });
            }
        }
    }
}