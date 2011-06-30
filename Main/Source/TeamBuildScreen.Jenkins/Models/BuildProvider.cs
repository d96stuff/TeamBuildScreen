// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildProvider.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.IO;
    using System.Net;
    using System.Xml.Serialization;

    public static class BuildProvider
    {
        public static AbstractBuild Get(string hudsonUri, string name)
        {
            var client = new WebClient();
            AbstractBuild build = null;

            try
            {
                var buildString =
                    client.DownloadString(hudsonUri + "job/" + Uri.EscapeUriString(name) + "/lastBuild/api/xml");
                
                var reader = new StringReader(buildString);

                if (buildString.StartsWith("<freeStyleBuild>"))
                {
                    build = new FreeStyleBuild();
                    var serializer = new XmlSerializer(build.GetType());
                    build = (FreeStyleBuild)serializer.Deserialize(reader);
                }
                else if (buildString.StartsWith("<matrixBuild>"))
                {
                    build = new MatrixBuild();
                    var serializer = new XmlSerializer(build.GetType());
                    build = (MatrixBuild)serializer.Deserialize(reader);
                }
                else
                {
                    var buildType = buildString.Substring(1, buildString.IndexOf('>'));
                    throw new NotSupportedException(string.Format("Unrecognised build type '{0}'", buildType));
                }
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)wex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        // 404
                        return null;
                    }
                }
            }

            return build;
        }
    }
}