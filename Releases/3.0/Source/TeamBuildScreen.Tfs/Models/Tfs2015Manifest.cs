namespace TeamBuildScreen.Tfs.Models
{
	using System.ComponentModel.Composition;

	using TeamBuildScreen.Core.Models;

	[Export(typeof(IServerManifest))]
	public class TfsManifest : ServerManifest<BuildServerService, TeamProjectPickerAdapter>
	{
		public TfsManifest() : base("Team Foundation Server") { }
	}
}