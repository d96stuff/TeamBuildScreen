namespace TeamBuildScreen.Core.Models
{
    public class ServerManifest<TServer, TPicker> : IServerManifest
        where TServer : IBuildServerService, new()
        where TPicker : IDomainProjectPicker, new()
    {
        public ServerManifest(string displayName)
        {
            this.BuildServerService = new TServer();
            this.DomainProjectPicker = new TPicker();
            this.DisplayName = displayName;
        }

        public IBuildServerService BuildServerService { get; private set; }

        public IDomainProjectPicker DomainProjectPicker { get; private set; }

        public string DisplayName
        {
            get;
            private set;
        }
    }
}