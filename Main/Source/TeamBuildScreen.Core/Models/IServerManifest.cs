namespace TeamBuildScreen.Core.Models
{
    public interface IServerManifest
    {
        string DisplayName { get; }

        IBuildServerService BuildServerService { get; }

        IDomainProjectPicker DomainProjectPicker { get; }
    }
}