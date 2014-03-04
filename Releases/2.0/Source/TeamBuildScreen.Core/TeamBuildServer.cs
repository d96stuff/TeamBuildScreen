using System.Collections.Generic;
using TeamBuildScreen.Core.Models;
using TeamBuildScreen.Core.ViewModels;

namespace TeamBuildScreen.Core
{
    public class TeamBuildServer
    {
        private readonly IBuildServerService service;

        public TeamBuildServer(IBuildServerService service)
        {
            this.service = service;
            this.BuildData = new List<BuildInfoViewModel>();
            var builds = Settings.Default.Builds;

            foreach (var build in builds)
            {
                this.BuildData.Add(BuildInfoViewModel.FromString(build, this.service));   
            }
        }

        public void Update()
        {
            this.service.Query();
        }

        public IList<BuildInfoViewModel> BuildData { get; private set; }

        public IndexViewModel GetIndexData()
        {
            var viewModel = new IndexViewModel
                                {
                                    BuildData = this.BuildData,
                                    UpdateInterval = Settings.Default.UpdateInterval,
                                    Columns = Settings.Default.Columns
                                };

            return viewModel;
        }
    }
}
