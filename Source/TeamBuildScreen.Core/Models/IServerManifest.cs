namespace TeamBuildScreen.Core.Models
{
    public interface IServerManifest
    {
        IBuildServerService BuildServerService { get; }

        IDomainProjectPicker DomainProjectPicker { get; }
    }
}