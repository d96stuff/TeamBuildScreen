//-----------------------------------------------------------------------
// <copyright file="IBuildServerService.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreen.Core.Models
{
    #region Usings

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Provides an interface for a class that periodically queries the status of current build definitions against a Team Foundation Server.
    /// </summary>
    public interface IBuildServerService : IDisposable
    {
        #region Events

        /// <summary>
        /// Occurs when a query is completed.
        /// </summary>
        event EventHandler QueryCompleted;

        /// <summary>
        /// Occurs when an error occurs.
        /// </summary>
        event EventHandler Error;

        /// <summary>
        /// Occurs when the service has not been configured.
        /// </summary>
        event EventHandler NotConfigured;

        #endregion

        #region Properties

        int StaleThreshold
        {
            get;
        }

        /// <summary>
        /// Sets the URL of the Team Foundation Server to query.
        /// </summary>
        string TfsUrl
        {
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the build with the specified key to the query list.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        void AddBuild(string key);

        /// <summary>
        /// Removes all builds from the query list.
        /// </summary>
        void ClearBuilds();

        /// <summary>
        /// Gets the <see cref="Microsoft.TeamFoundation.Build.Client.IBuildDetail"/> for the build with the specified key.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        /// <returns>The <see cref="Microsoft.TeamFoundation.Build.Client.IBuildDetail"/> for the build with the specified key.</returns>
        IBuildInfo GetBuildInfo(string key, string configuration, string platform);

        /// <summary>
        /// Returns a value that indicates whether the build with the specified key has any builds queued.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        /// <returns>true if the specified build has any builds queued, otherwise; false.</returns>
        bool IsQueued(string key);

        /// <summary>
        /// Starts querying the server.
        /// </summary>
        void Start();

        /// <summary>
        /// Loads the available builds from the build server.
        /// </summary>
        /// <param name="builds">The collection to populate.</param>
        void LoadBuilds(ICollection<BuildSetting> builds);

        #endregion
    }
}