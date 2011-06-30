using System;
using System.Collections.Generic;
using System.Linq;
using TeamBuildScreen.Core.DataTransfer;
using TeamBuildScreen.Core.Models;
using TeamBuildScreen.Core.ViewModels;
using TeamBuildScreen.Demo;

namespace TeamBuildScreen.Server.Handlers
{
    public class BuildHandler
    {
        private IBuildServerService buildServerService;
        private IList<BuildInfoViewModel> builds;

        public BuildHandler()
        {
            this.buildServerService = new MockBuildServerService();
            this.builds = new List<BuildInfoViewModel>();

            var dataModel = new BuildInfoModel("TeamProject", "MyFirstBuild", null, null, this.buildServerService);
            this.builds.Add(new BuildInfoViewModel(dataModel));
            dataModel = new BuildInfoModel("TeamProject", "MySecondBuild", null, null, this.buildServerService);
            this.builds.Add(new BuildInfoViewModel(dataModel));
        }

        public IList<BuildInfoViewModelDto> Get()
        {
            this.buildServerService.Query();

            return this.builds.Select(x => x.ToDto()).ToList();
        }

        public BuildInfoViewModelDto Get(int id)
        {
            return new BuildInfoViewModelDto
            {
                Description = "Team Project: MyFirstBuild",
                Status = "In Progress",
                RequestedBy = "Joe Bloggs",
                StartedOn = DateTime.Now.ToString(),
                CompletedOn = DateTime.Now.ToString(),
                TestResults = "No test result.",
                IsQueued = false,
                IsFinished = false,
                Progress = (decimal)0.25
            };
        }
    }
}