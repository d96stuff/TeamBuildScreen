namespace TeamBuildScreen.Tfs2015.Models
{
	using System.ComponentModel.Composition;

	using TeamBuildScreen.Core.Models;

	[Export(typeof(IServerManifest))]
	public class Tfs2015Manifest : ServerManifest<BuildServerService, TeamProjectPickerAdapter>
	{
		public Tfs2015Manifest() : base("Team Foundation Server 2015") { }
	}
}