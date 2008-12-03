//-----------------------------------------------------------------------
// <copyright file="BuildServerQuery.cs" company="Jim Liddell">
//    Copyright © Jim Liddell.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreensaver.DataModels
{
    #region Usings

    using System;
    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    public interface IBuildServerQuery : IDisposable
    {
        void AddBuild(string key);
        event EventHandler QueryCompleted;
        IBuildDetail this[string key] { get; }
        void Start();
    }
}