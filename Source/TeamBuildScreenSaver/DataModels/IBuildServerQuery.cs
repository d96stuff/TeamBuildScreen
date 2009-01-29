//-----------------------------------------------------------------------
// <copyright file="IBuildServerQuery.cs" company="Jim Liddell">
//    Copyright © Jim Liddell. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.DataModels
{
    #region Usings

    using System;
    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    /// <summary>
    /// Provides an interface for a class that periodically queries the status of current build definitions against a Team Foundation Server.
    /// </summary>
    public interface IBuildServerQuery : IDisposable
    {
        #region Methods

        /// <summary>
        /// Adds the build with the specified key to the query list.
        /// </summary>
        /// <param name="key">The key of the build definition.</param>
        void AddBuild(string key);

        /// <summary>
        /// Starts querying the server.
        /// </summary>
        void Start();

        #endregion

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

        #region Indexer

        IBuildDetail this[string key] { get; }

        #endregion
    }
}