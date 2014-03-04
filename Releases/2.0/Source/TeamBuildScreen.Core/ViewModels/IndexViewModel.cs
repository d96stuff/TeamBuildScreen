using System.Collections.Generic;

namespace TeamBuildScreen.Core.ViewModels
{
    public class IndexViewModel
    {
        public IList<BuildInfoViewModel> BuildData { get; set; }

        public int UpdateInterval { get; set; }

        public int Columns { get; set; }
    }
}