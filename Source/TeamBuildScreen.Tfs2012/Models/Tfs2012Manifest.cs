namespace TeamBuildScreen.Tfs2012.Models
{
    using System.ComponentModel.Composition;

    using TeamBuildScreen.Core.Models;

    [Export(typeof(IServerManifest))]
    public class Tfs2012Manifest : ServerManifest<BuildServerService, TeamProjectPickerAdapter>
    {
        public Tfs2012Manifest() : base("Team Foundation Server 2012") { }
    }
}