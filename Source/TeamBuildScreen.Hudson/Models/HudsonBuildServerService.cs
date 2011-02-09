using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using TeamBuildScreen.Core.Models;

namespace TeamBuildScreen.Hudson.Models
{
    public class HudsonBuildServerService : BuildServerServiceBase, IBuildServerService
    {
        private IDictionary<string, IBuildInfo> builds;
        private hudson hudson;
        private string hudsonUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="HudsonBuildServerService"/> class. The server will be queried every 30 seconds.
        /// </summary>
        public HudsonBuildServerService()
            : base(30000)
        {
            this.Init();
        }

        public HudsonBuildServerService(int period)
            : base(period)
        {
            this.Init();
        }

        private void Init()
        {
            this.builds = new Dictionary<string, IBuildInfo>();
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
                    this.hudson = new hudson();
                    var serializer = new XmlSerializer(hudson.GetType());
                    this.hudson = (hudson)serializer.Deserialize(reader);
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
                    var build = GetBuild(name);
                    var testResults = this.GetTestResultsForBuild(name, build.number);
                    var buildInfo = new HudsonBuildInfo(build, testResults);

                    this.builds[name] = buildInfo;
                }
            }

            this.OnQueryCompleted();
        }

        private freeStyleBuild GetBuild(string name)
        {
            WebClient client = new WebClient();
            var buildString = client.DownloadString(this.hudsonUri + "job/" + Uri.EscapeUriString(name) + "/lastBuild/api/xml");
            StringReader reader = new StringReader(buildString);
            freeStyleBuild build = new freeStyleBuild();
            XmlSerializer serializer = new XmlSerializer(build.GetType());
            build = (freeStyleBuild)serializer.Deserialize(reader);
            return build;
        }

        private testResult GetTestResultsForBuild(string name, string number)
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
                        var testResult = new testResult();
                        var serializer = new XmlSerializer(testResult.GetType());
                        testResult = (testResult)serializer.Deserialize(reader);

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

                this.builds.Add(definitionName, null);
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

            return this.builds[definitionName];
        }

        public bool IsQueued(string key)
        {
            // TODO: Implement
            return false;
        }

        public void LoadBuilds(ICollection<BuildSetting> builds)
        {
            foreach (var job in this.hudson.job)
            {
                builds.Add(new BuildSetting { DefinitionName = job.name });
            }
        }
    }
}