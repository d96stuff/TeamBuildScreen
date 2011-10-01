using System.Collections.Generic;
using System.Linq;
using OpenRasta.Web;
using TeamBuildScreen.Core;
using TeamBuildScreen.Core.DataTransfer;
using TeamBuildScreen.Demo;

namespace TeamBuildScreen.Server.Handlers
{
    public class BuildHandler
    {
        private readonly TeamBuildServer server;
        private bool error;

        public BuildHandler()
        {
            var buildServerService = new MockBuildServerService();
            buildServerService.Error += OnError;

            this.server = new TeamBuildServer(buildServerService);
        }

        private void OnError(object sender, System.EventArgs e)
        {
            this.error = true;
        }

        public OperationResult Get()
        {
            this.server.Update();

            if (this.error)
            {
                return new OperationResult.InternalServerError();
            }

            return new OperationResult.OK(this.server.BuildData.Select(x => x.ToDto()).ToList());
        }
    }
}