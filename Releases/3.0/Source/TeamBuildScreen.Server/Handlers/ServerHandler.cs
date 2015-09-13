using System.Linq;
using OpenRasta.Web;
using TeamBuildScreen.Core.Models;
using TeamBuildScreen.Server.Resources;
using System.Collections.Generic;

namespace TeamBuildScreen.Server.Handlers
{
    public class ServerHandler
    {
        private readonly ServerProvider serverProvider;

        public ServerHandler()
        {
            this.serverProvider = ServerProvider.Instance;
        }

        public IList<ServerDto> Get()
        {
            return this.serverProvider.Manifests.Select(m => new ServerDto { DisplayName = m.DisplayName }).ToList();
        }
    }
}