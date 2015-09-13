using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuildScreen.Core.Models;
using TeamBuildScreen.Core.ViewModels;

namespace TeamBuildScreen.Core.DataTransfer
{
    public static class BuildInfoViewModelExtensions
    {
        public static BuildInfoViewModelDto ToDto(this BuildInfoViewModel viewModel)
        {
            return new BuildInfoViewModelDto
                       {
                           CompletedOn = viewModel.CompletedOn.HasValue ? viewModel.CompletedOn.Value.ToString() : null,
                           Description = viewModel.Description,
                           IsFinished = viewModel.Status != BuildStatus.InProgress,
                           IsQueued = viewModel.IsQueued,
                           IsStale = viewModel.IsStale,
                           Progress = (decimal) 0.5,
                           RequestedBy = viewModel.RequestedBy,
                           StartedOn = viewModel.StartedOn.HasValue ? viewModel.StartedOn.Value.ToString() : null,
                           Status = viewModel.Status.ToString(),
                           TestResults = viewModel.TestResults
                       };
        }
    }
}
