namespace TeamBuildScreen.Core.Models
{
    public interface IDomainProjectPicker
    {
        bool Show();

        string TfsUri { get; }
    }
}
