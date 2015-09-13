using System.Collections.Generic;
namespace TeamBuildScreen.Server.Resources
{
    public class IndexViewModelDto
    {
        public string BuildData { get; set; }

        public SettingsViewModelDto Settings { get; set; }
    }

    public class SettingsViewModelDto
    {
        public DisplayOptions DisplayOptions { get; set; }
    }

    public class DisplayOptions
    {
        public int Columns { get; set; }

        public int UpdateInterval { get; set; }

        public int StaleThreshold { get; set; }
    }
}