using System;
using System.Windows;
using System.Windows.Threading;
using TeamBuildScreen.Core.Models;
using TeamBuildScreen.Tfs2015.Models;
using TeamBuildScreen.Core;

namespace TeamBuildScreen.SaverTfs2015
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private TeamBuildScreenSaver screenSaver;

		private void OnStartup(Object sender, StartupEventArgs e)
		{
			IBuildServerService service = new BuildServerService();
			IDomainProjectPicker projectPicker = new TeamProjectPickerAdapter();
			this.screenSaver = new TeamBuildScreenSaver(service, projectPicker);

			string[] args = e.Args;

			this.screenSaver.Startup(args);
		}

		private void OnExit(object sender, ExitEventArgs e)
		{
			this.screenSaver.Exit();
		}

		private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			// TODO: Log details
			MessageBox.Show(
				string.Format(
				"An error has been detected in the application that has caused the application to shutdown:\n\n{0}\n\nApologies for any inconvenience.",
				e.Exception.Message),
				"Team Build Screen Saver",
				MessageBoxButton.OK,
				MessageBoxImage.Error);

			e.Handled = true;

			if (Application.Current != null)
			{
				Application.Current.Shutdown();
			}
		}
	}
}
