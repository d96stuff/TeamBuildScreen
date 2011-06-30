using System.Collections.Generic;
using System.IO;
using OpenRasta.Codecs;
using OpenRasta.Configuration;
using OpenRasta.Configuration.Fluent;
using TeamBuildScreen.Core.DataTransfer;
using TeamBuildScreen.Server.Handlers;

namespace TeamBuildScreen.Server
{
    public class Configurator : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                //ResourceSpace.Uses.ViewsEmbeddedInTheAssembly(Assembly.GetExecutingAssembly(), "TeamBuildScreen.Console.Views");

                ResourceSpace.Has
                    .ResourcesOfType<BuildInfoViewModelDto>()
                    .AtUri("/builds/{id}")
                    .HandledBy<BuildHandler>()
                    .AsJsonDataContract();
                ResourceSpace.Has
                    .ResourcesOfType<IList<BuildInfoViewModelDto>>()
                    .AtUri("/builds")
                    .HandledBy<BuildHandler>()
                    .AsJsonDataContract();
                ResourceSpace.Has
                    .ResourcesOfType<Stream>()
                    .AtUri("/static/{path}")
                    .And.AtUri("/static/images/{path}")
                    .And.AtUri("/static/colors/{path}")
                    .HandledBy<EmbeddedResourceHandler>()
                    .TranscodedBy<ApplicationOctetStreamCodec>().ForMediaType("text/html").ForExtension(".html")
                    .And.TranscodedBy<ApplicationOctetStreamCodec>().ForMediaType("text/javascript").ForExtension(".js")
                    .And.TranscodedBy<ApplicationOctetStreamCodec>().ForMediaType("text/css").ForExtension(".css")
                    .And.TranscodedBy<ApplicationOctetStreamCodec>().ForMediaType("image/png").ForExtension(".png");
            }
        }
    }
}