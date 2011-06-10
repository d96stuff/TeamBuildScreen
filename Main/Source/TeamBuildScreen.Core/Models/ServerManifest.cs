namespace TeamBuildScreen.Core.Models
{
    public class ServerManifest<TServer, TPicker> : IServerManifest
        where TServer : IBuildServerService, new()
        where TPicker : IDomainProjectPicker, new()
    {
        public ServerManifest()
        {
            this.BuildServerService = new TServer();
            this.DomainProjectPicker = new TPicker();
        }

        public IBuildServerService BuildServerService { get; private set; }

        public IDomainProjectPicker DomainProjectPicker { get; private set; }
    }
}