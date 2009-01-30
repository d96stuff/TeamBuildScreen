//-----------------------------------------------------------------------
// <copyright file="ConfigurationSummaryHandler.cs" company="Jim Liddell"> 
//    Copyright © Jim Liddell. All rights reserved. 
// </copyright>
//-----------------------------------------------------------------------

namespace TeamBuildScreenSaver.ViewModels
{
    #region Usings

    using Microsoft.TeamFoundation.Build.Client;

    #endregion

    /// <summary>
    /// Provides a delegate for a method that resolves the configuration summary for a specified build.
    /// </summary>
    /// <param name="build">The <see cref="IBuildDetail"/>.</param>
    /// <param name="flavour">The configuration for the build (e.g. 'Release').</param>
    /// <param name="platform"></param>
    /// <returns></returns>
    public delegate IConfigurationSummary ConfigurationSummaryHandler(IBuildDetail build, string flavour, string platform);
}