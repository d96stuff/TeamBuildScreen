using TeamBuildScreen.Core;
using TeamBuildScreen.Demo;
using TeamBuildScreen.Server.Resources;

namespace TeamBuildScreen.Server.Handlers
{
    public class IndexHandler
    {
        private readonly TeamBuildServer server;

        public IndexHandler()
        {
            this.server = new TeamBuildServer(new MockBuildServerService());
        }

        public IndexViewModelDto Get()
        {
            return this.server.GetIndexData().ToDto();
        }
    }
}