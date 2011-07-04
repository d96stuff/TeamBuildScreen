using System.Collections.Generic;
using System.Linq;
using TeamBuildScreen.Core;
using TeamBuildScreen.Core.DataTransfer;
using TeamBuildScreen.Demo;

namespace TeamBuildScreen.Server.Handlers
{
    public class BuildHandler
    {
        private readonly TeamBuildServer server;

        public BuildHandler()
        {
            this.server = new TeamBuildServer(new MockBuildServerService());
        }

        public IList<BuildInfoViewModelDto> Get()
        {
            this.server.Update();

            return this.server.BuildData.Select(x => x.ToDto()).ToList();
        }
    }
}