using System.Linq;
using TeamBuildScreen.Core;
using TeamBuildScreen.Core.DataTransfer;
using TeamBuildScreen.Core.ViewModels;

namespace TeamBuildScreen.Server.Resources
{
    public static class IndexViewModelExtensions
    {
        public static IndexViewModelDto ToDto(this IndexViewModel viewModel)
        {
            return new IndexViewModelDto
                       {
                           BuildData = viewModel.BuildData.Select(x => x.ToDto()).ToJson(),
                           Columns = viewModel.Columns,
                           UpdateInterval = viewModel.UpdateInterval
                       };
        }
    }
}
