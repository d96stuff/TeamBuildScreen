namespace TeamBuildScreen.Hudson.Models
{
    using System.IO;
    using System.Net;
    using System.Xml.Serialization;

    public static class HudsonProvider
    {
        public static Hudson Get(string url)
        {
            var client = new WebClient();
            var hudsonString = client.DownloadString(url + "api/xml");
            var reader = new StringReader(hudsonString);
            var hudson = new Hudson();
            var serializer = new XmlSerializer(hudson.GetType());
            hudson = (Hudson)serializer.Deserialize(reader);

            return hudson;
        }
    }
}