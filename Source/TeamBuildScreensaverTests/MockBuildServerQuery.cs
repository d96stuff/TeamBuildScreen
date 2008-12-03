using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Microsoft.TeamFoundation.Build.Client;
using System.Collections;
using System.Threading;
using TeamBuildScreensaver.DataModels;

namespace TeamBuildScreensaverTests
{
    class MockBuildServerQuery : IBuildServerQuery
    {
        private Timer backgroundTimer;
        private Hashtable builds;
        private int period = 10000;

        public MockBuildServerQuery()
        {
            this.builds = new Hashtable();
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
            Hashtable latestBuilds = new Hashtable();
            Random random = new Random();

            lock (this.builds)
            {
                foreach (string key in this.builds.Keys)
                {
                    string[] build = key.Split(';');
                    string teamProject = build[0];
                    string definitionName = build[1];

                    int rand = random.Next(6);
                    BuildStatus status = BuildStatus.NotStarted;

                    switch (rand)
                    {
                        case 0:
                            status = BuildStatus.InProgress;
                            break;
                        case 1:
                            status = BuildStatus.Succeeded;
                            break;
                        case 2:
                            status = BuildStatus.PartiallySucceeded;
                            break;
                        case 3:
                            status = BuildStatus.Failed;
                            break;
                        case 4:
                            status = BuildStatus.Stopped;
                            break;
                        case 5:
                            status = BuildStatus.NotStarted;
                            break;
                    }

                    IBuildDetail latestBuild = new MockBuildDetail(status, "DOMAIN\\Joe Blogs", DateTime.Now, true, DateTime.Now, new MockBuildDefinition(teamProject, definitionName));

                    latestBuilds.Add(key, latestBuild);
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

        #region IBuildServerQuery Members

        public void AddBuild(string key)
        {
            lock (this.builds)
            {
                this.builds.Add(key, null);
            }
        }

        public event EventHandler QueryCompleted;

        public IBuildDetail this[string key]
        {
            get
            {
                return this.builds[key] as IBuildDetail;
            }
        }

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
