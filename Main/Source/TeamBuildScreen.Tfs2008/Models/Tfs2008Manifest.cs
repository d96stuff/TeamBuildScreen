namespace TeamBuildScreen.Tfs2008.Models
{
    using System.ComponentModel.Composition;

    using TeamBuildScreen.Core.Models;

    [Export(typeof(IServerManifest))]
    public class Tfs2008Manifest : ServerManifest<BuildServerService, DomainProjectPickerAdapter>
    {
        public Tfs2008Manifest() : base("Team Foundation Server 2008") { }
    }
}