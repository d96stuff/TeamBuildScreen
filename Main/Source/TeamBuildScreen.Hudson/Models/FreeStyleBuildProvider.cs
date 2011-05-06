namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.IO;
    using System.Net;
    using System.Xml.Serialization;

    public static class FreeStyleBuildProvider
    {
        public static FreeStyleBuild Get(string hudsonUri, string name)
        {
            var client = new WebClient();
            var buildString =
                client.DownloadString(hudsonUri + "job/" + Uri.EscapeUriString(name) + "/lastBuild/api/xml");
            var reader = new StringReader(buildString);
            var build = new FreeStyleBuild();
            var serializer = new XmlSerializer(build.GetType());
            build = (FreeStyleBuild)serializer.Deserialize(reader);

            return build;
        }
    }
}