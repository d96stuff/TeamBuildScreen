using System.ServiceProcess;

namespace TeamBuildScreen.Service
{
    using System.ComponentModel.Composition.Hosting;

    using TeamBuildScreen.Core.Models;

    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
            var catalog = new AggregateCatalog(
                new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()),
                new DirectoryCatalog("providers"));
            var container = new CompositionContainer(catalog);
            var serverProvider = container.GetExport<ServerProvider>();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
