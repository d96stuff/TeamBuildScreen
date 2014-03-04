namespace TeamBuildScreen.Hudson.Models
{
    using System.ComponentModel.Composition;

    using TeamBuildScreen.Core.Models;

    [Export(typeof(IServerManifest))]
    public class JenkinsManifest : ServerManifest<HudsonBuildServerService, HudsonDomainProjectPicker>
    {
        public JenkinsManifest() : base("Jenkins") { }
    }
}