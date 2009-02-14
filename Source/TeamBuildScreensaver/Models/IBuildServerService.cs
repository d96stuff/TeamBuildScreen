﻿//-----------------------------------------------------------------------
// <copyright file="IBuildServerService.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.Models
{
    #region Usings

    using System;
    using Microsoft.TeamFoundation.Build.Client;

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

        #endregion

        #region Methods

        /// <summary>
        /// Adds the build with the specified key to the query list.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        void AddBuild(string key);

        /// <summary>
        /// Gets the <see cref="Microsoft.TeamFoundation.Build.Client.IBuildDetail"/> for the build with the specified key.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        /// <returns>The <see cref="Microsoft.TeamFoundation.Build.Client.IBuildDetail"/> for the build with the specified key.</returns>
        IBuildDetail GetBuildDetail(string key);

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

        #endregion
    }
}