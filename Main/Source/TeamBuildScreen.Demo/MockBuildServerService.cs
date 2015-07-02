//-----------------------------------------------------------------------
// <copyright file="MockBuildServerService.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Demo
{
    #region Usings

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using TeamBuildScreen.Core.Models;

    #endregion

    public class MockBuildServerService : IBuildServerService
    {
        #region Fields

        private Timer backgroundTimer;
        private Hashtable builds;
        private int period = 10000;

        #endregion

        #region Properties

        public int StaleThreshold
        {
            get;
            private set;
        }

        public string TfsUrl
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public MockBuildServerService()
        {
            this.builds = new Hashtable();
            this.StaleThreshold = 7;
        }

        #endregion

        #region Methods

        public void Start()
        {
            if (this.backgroundTimer == null)
            {
                this.backgroundTimer = new Timer(new TimerCallback(this.Query), null, 0, this.period);
            }
        }

        /// <summary>
        /// Loads the available builds from the build server.
        /// </summary>
        /// <param name="builds">The collection to populate.</param>
		public void LoadBuilds(ICollection<BuildSetting> buildsToLoad)
        {
			buildsToLoad.Clear();

			for (int i = 0; i < 10; i++)
			{
				BuildSetting buildSettingDataModel = new BuildSetting
				{
					DefinitionName = "Build " + i,
					TeamProject = "TeamProject"
				};

				buildsToLoad.Add(buildSettingDataModel);
			}
		}

        public void Query()
        {
            this.Query(null);
        }

        private void Query(Object stateInfo)
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

                    int rand = random.Next(8);
                    BuildStatus? status = BuildStatus.NotStarted;

                    switch (rand)
                    {
                        case 0:
                            status = BuildStatus.InProgress;
                            break;
                        case 1:
                            status = BuildStatus.Succeeded;
                            break;
                        case 2:
                            status = BuildStatus.Succeeded;
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
                        case 6:
                            status = BuildStatus.Succeeded;
                            break;
                        case 7:
                            status = BuildStatus.PartiallySucceeded;
                            break;
							//this.OnError();

							//return;
                    }

                    MockBuildInfo latestBuild = null;

                    if (status.HasValue)
                    {
						var start = GetRandomDateTimeForStart();
                        latestBuild = new MockBuildInfo(status.Value, "DOMAIN\\Joe Bloggs", start, true, GetRandomDateTimeForEnd(start));
						latestBuild.FailedTests.Clear();

                        switch (status)
                        {
                            case BuildStatus.Succeeded:
                                latestBuild.TestsFailed = 0;
                                latestBuild.TestsPassed = 1337;
                                break;
                            case BuildStatus.PartiallySucceeded:
                                latestBuild.TestsFailed = 42;
                                latestBuild.TestsPassed = 1066;
								latestBuild.FailedTests.Add("TestDatabase");
								latestBuild.FailedTests.Add("TestLogin");
								latestBuild.FailedTests.Add("TestPurchase");
                                break;
                        }
                    }

                    latestBuilds.Add(key, latestBuild);
                }

                foreach (string key in latestBuilds.Keys)
                {
                    this.builds[key] = latestBuilds[key];
                }
            }

            this.OnQueryCompleted();
        }

        private static DateTime GetRandomDateTimeForStart()
        {
			Random random = new Random(Environment.TickCount);
			
			DateTime start = DateTime.UtcNow.AddDays(-14);
            DateTime end = DateTime.UtcNow.AddMinutes(-random.Next(60));

            int range = ((TimeSpan)(end - start)).Days;

            return start.AddDays(random.Next(range));
        }

		private static DateTime GetRandomDateTimeForEnd(DateTime start)
		{
			Random random = new Random(Environment.TickCount);
			return start.AddMinutes(random.Next(60));
		}

		private void OnQueryCompleted()
        {
            if (this.QueryCompleted != null)
            {
                this.QueryCompleted(this, EventArgs.Empty);
            }
        }

        private void OnError()
        {
            if (this.Error != null)
            {
                this.Error(this, new StringEventArgs(string.Empty));
            }
        }

        public void RaiseError()
        {
            this.OnError();
        }

        public void RaiseQueryCompleted()
        {
            this.OnQueryCompleted();
        }

        public void SetBuildInfo(string key, IBuildInfo detail)
        {
            this.builds[key] = detail;
        }

        #endregion

        #region IBuildServerService Members

        public bool IsQueued(string key)
        {
            Random random = new Random();

            return Convert.ToBoolean(random.Next(2));
        }

        public void AddBuild(string key)
        {
            lock (this.builds)
            {
                this.builds.Add(key, null);
            }
        }

        public void ClearBuilds()
        {
            this.builds.Clear();
        }

        public event EventHandler QueryCompleted;

        public event EventHandler<StringEventArgs> Error;

        public event EventHandler NotConfigured;

        public IBuildInfo GetBuildInfo(string key, string configuration, string platform)
        {
            return this.builds[key] as IBuildInfo;
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

        public event EventHandler Stopped;

        public void Stop()
        {
            this.OnStopped();
        }

        private void OnStopped()
        {
            if (this.Stopped != null)
            {
                this.Stopped(this, EventArgs.Empty);
            }
        }
    }
}