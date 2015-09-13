namespace TeamBuildScreen.Demo
{
    using System.ComponentModel.Composition;

    using TeamBuildScreen.Core.Models;

    [Export(typeof(IServerManifest))]
    public class DemoManifest : ServerManifest<MockBuildServerService, MockDomainProjectPicker>
    {
        public DemoManifest() : base("Demo") { }
    }
}