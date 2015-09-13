using System.ServiceProcess;
using TeamBuildScreen.Server;

namespace TeamBuildScreen.Service
{
    public partial class Service : ServiceBase
    {
        private HttpListenerHostWithConfiguration host;

        public Service()
        {
            InitializeComponent();
            //var catalog = new AggregateCatalog(
            //    new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly()),
            //    new DirectoryCatalog("providers"));
            //var container = new CompositionContainer(catalog);
            //var serverProvider = container.GetExport<ServerProvider>();

            this.host = new HttpListenerHostWithConfiguration { Configuration = new Configurator() };
            host.Initialize(new[] { "http://+:9222/" }, "/", null);
        }

        protected override void OnStart(string[] args)
        {
            host.StartListening();
        }

        protected override void OnStop()
        {
            host.StopListening();
            host.Close();
        }
    }
}
