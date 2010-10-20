using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Proxy;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TeamBuildScreensaver.Views
{
    /// <summary>
    /// Interaction logic for EditSettings.xaml
    /// </summary>
    public partial class EditSettings : Window
    {
        public EditSettings()
        {
            InitializeComponent();

            this.serverUriTextBox.Text = Settings.Default.TfsUri;
            this.columnsComboBox.SelectedIndex = Settings.Default.Columns - 1;

            switch (Settings.Default.UpdateInterval)
            {
                case 15000:
                    this.refreshComboBox.SelectedIndex = 0;
                    break;
                case 30000:
                    this.refreshComboBox.SelectedIndex = 1;
                    break;
                case 45000:
                    this.refreshComboBox.SelectedIndex = 2;
                    break;
                case 60000:
                    this.refreshComboBox.SelectedIndex = 3;
                    break;
                default:
                    this.refreshComboBox.SelectedIndex = 1;
                    break;
            }

            this.Update();
        }

        private void Update()
        {
            if (!string.IsNullOrEmpty(this.serverUriTextBox.Text))
            {
                this.buildDefinitionsTreeView.IsEnabled = true;

                TeamFoundationServer tfs = TeamFoundationServerFactory.GetServer(this.serverUriTextBox.Text);

                IBuildServer buildServer = (IBuildServer)tfs.GetService(typeof(IBuildServer));
                VersionControlServer versionControlServer = (VersionControlServer)tfs.GetService(typeof(VersionControlServer));

                TeamProject[] teamProjects = versionControlServer.GetAllTeamProjects(true);

                foreach (TeamProject project in teamProjects)
                {
                    TreeViewItem projectNode = new TreeViewItem();
                    projectNode.Header = project.Name;
                    int projectIndex = this.buildDefinitionsTreeView.Items.Add(projectNode);

                    IBuildDefinition[] projectBuilds = buildServer.QueryBuildDefinitions(project.Name);

                    foreach (IBuildDefinition definition in projectBuilds)
                    {
                        CheckBox buildCheckbox = new CheckBox();
                        buildCheckbox.Content = definition.Name;

                        if (DefinitionSelected(project.Name, definition.Name))
                        {
                            buildCheckbox.IsChecked = true;
                        }

                        ((TreeViewItem)this.buildDefinitionsTreeView.Items[projectIndex]).Items.Add(buildCheckbox);
                    }
                }
            }
            else
            {
                this.buildDefinitionsTreeView.IsEnabled = false;
            }
        }

        private bool DefinitionSelected(string projectName, string definitionName)
        {
            string combined = string.Format("{0};{1}", projectName, definitionName);

            foreach (string definition in Settings.Default.Builds)
            {
                if (definition == combined)
                {
                    return true;
                }
            }

            return false;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.SaveSettings();

            this.Close();
        }

        private void SaveSettings()
        {
            Settings.Default.TfsUri = this.serverUriTextBox.Text;
            Settings.Default.Columns = Convert.ToInt32(((ComboBoxItem)this.columnsComboBox.SelectedValue).Content);
            Settings.Default.UpdateInterval = Convert.ToInt32(((ComboBoxItem)this.refreshComboBox.SelectedValue).Content) * 1000;
            Settings.Default.Builds.Clear();

            foreach (TreeViewItem projectNode in this.buildDefinitionsTreeView.Items)
            {
                string projectName = projectNode.Header.ToString();

                foreach (CheckBox definition in projectNode.Items)
                {
                    if (definition.IsChecked.Value)
                    {
                        Settings.Default.Builds.Add(string.Format("{0};{1}", projectName, definition.Content));
                    }
                }
            }

            Settings.Default.Save();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void selectServerButton_Click(object sender, RoutedEventArgs e)
        {
            DomainProjectPicker dpp = new DomainProjectPicker(DomainProjectPickerMode.None);

            if (dpp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.serverUriTextBox.Text = dpp.SelectedServer.Uri.ToString();
            }

            this.Update();
        }
    }
}