namespace TeamBuildScreen.Hudson.Models
{
    using System.ComponentModel.Composition;

    using TeamBuildScreen.Core.Models;

    [Export(typeof(IServerManifest))]
    public class HudsonManifest : ServerManifest<HudsonBuildServerService, HudsonDomainProjectPicker> { }
}