using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TeamBuildScreen.Core.Models
{
    public abstract class BuildServerServiceBase
    {
        #region Fields

        /// <summary>
        /// Controls the timing of queries against the server.
        /// </summary>
        private Timer queryTimer;

        /// <summary>
        /// The interval between queries.
        /// </summary>
        private int period;

        #endregion

        #region Constructors

        protected BuildServerServiceBase(int period)
        {
            this.period = period;
        }

        #endregion

        #region Methods

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

        protected abstract void Query(object stateInfo);

        /// <summary>
        /// Raises the <see cref="TeamBuildScreenSaver.Models.IBuildServerService.QueryCompleted"/> event.
        /// </summary>
        protected void OnQueryCompleted()
        {
            if (this.QueryCompleted != null)
            {
                this.QueryCompleted(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="TeamBuildScreenSaver.Models.IBuildServerService.Error"/> event.
        /// </summary>
        protected void OnError()
        {
            if (this.Error != null)
            {
                this.Error(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="TeamBuildScreenSaver.Models.IBuildServerService.NotConfigured"/> event.
        /// </summary>
        protected void OnNotConfigured()
        {
            if (this.NotConfigured != null)
            {
                this.NotConfigured(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Parses the team project and definition name from the given string representing a build.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        /// <param name="teamProject">The name of the team project.</param>
        /// <param name="definitionName">The name of the definition.</param>
        protected static void ParseBuild(string key, out string teamProject, out string definitionName)
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

        /// <summary>
        /// Occurs when the service has not been configured.
        /// </summary>
        public event EventHandler NotConfigured;

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