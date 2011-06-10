namespace TeamBuildScreen.Demo
{
    using System.ComponentModel.Composition;

    using TeamBuildScreen.Core.Models;

    [Export(typeof(IServerManifest))]
    public class HudsonManifest : ServerManifest<MockBuildServerService, MockDomainProjectPicker> { }
}