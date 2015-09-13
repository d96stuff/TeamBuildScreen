using System.Windows;
using TeamBuildScreen.Core.Models;
using TeamBuildScreen.Tfs.Models;
using TeamBuildScreen.Core;

namespace TeamBuildScreen.Tfs.Desktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void OnStartup(object sender, StartupEventArgs e)
		{
			IBuildServerService service = new BuildServerService();
			IDomainProjectPicker projectPicker = new TeamProjectPickerAdapter();

			TeamBuildDesktop teamBuildDesktop = new TeamBuildDesktop(service, projectPicker, "Team Build Screen");
			teamBuildDesktop.Startup();
		}
	}
}
