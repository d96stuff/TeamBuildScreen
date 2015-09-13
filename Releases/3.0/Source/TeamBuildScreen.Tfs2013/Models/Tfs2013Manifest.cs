namespace TeamBuildScreen.Tfs2013.Models
{
    using System.ComponentModel.Composition;

    using TeamBuildScreen.Core.Models;

    [Export(typeof(IServerManifest))]
    public class Tfs2013Manifest : ServerManifest<BuildServerService, TeamProjectPickerAdapter>
    {
        public Tfs2013Manifest() : base("Team Foundation Server 2013") { }
    }
}