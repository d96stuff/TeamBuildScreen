using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenRasta.Codecs;
using OpenRasta.Codecs.Razor;
using OpenRasta.Configuration;
using TeamBuildScreen.Core.DataTransfer;
using TeamBuildScreen.Server.Codecs;
using TeamBuildScreen.Server.Handlers;
using TeamBuildScreen.Server.Resources;

namespace TeamBuildScreen.Server
{
    public class Configurator : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Uses.ViewsEmbeddedInTheAssembly(Assembly.GetExecutingAssembly(), "TeamBuildScreen.Server.Views");

                ResourceSpace.Has
                    .ResourcesOfType<IList<ServerDto>>()
                    .AtUri("/servers")
                    .HandledBy<ServerHandler>()
                    .AsJsonDataContract();
                ResourceSpace.Has
                    .ResourcesOfType<IList<BuildInfoViewModelDto>>()
                    .AtUri("/builds")
                    .HandledBy<BuildHandler>()
                    .TranscodedBy<NoCacheJsonDataContractCodec>();
                ResourceSpace.Has
                    .ResourcesOfType<IndexViewModelDto>()
                    .AtUri("/")
                    .HandledBy<IndexHandler>()
                    .RenderedByRazor("Index.cshtml");
                ResourceSpace.Has
                    .ResourcesOfType<Stream>()
                    .AtUri("/content/{path}")
                    .And.AtUri("/content/images/{path}")
                    .And.AtUri("/content/colors/{path}")
                    .HandledBy<EmbeddedResourceHandler>()
                    .TranscodedBy<ApplicationOctetStreamCodec>().ForMediaType("text/html").ForExtension(".html")
                    .And.TranscodedBy<ApplicationOctetStreamCodec>().ForMediaType("text/javascript").ForExtension(".js")
                    .And.TranscodedBy<ApplicationOctetStreamCodec>().ForMediaType("text/css").ForExtension(".css")
                    .And.TranscodedBy<ApplicationOctetStreamCodec>().ForMediaType("image/png").ForExtension(".png");
            }
        }
    }
}