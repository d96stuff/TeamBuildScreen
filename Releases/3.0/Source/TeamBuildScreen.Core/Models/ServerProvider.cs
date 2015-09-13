using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace TeamBuildScreen.Core.Models
{
    using System.ComponentModel.Composition;

    public class ServerProvider
    {
        private static ServerProvider instance;

        [ImportMany]
        public IEnumerable<IServerManifest> Manifests { get; set; }

        public static ServerProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    var catalog = new DirectoryCatalog(".");
                    var container = new CompositionContainer(catalog);

                    instance = new ServerProvider();

                    container.ComposeParts(instance);
                }

                return instance;
            }
        }
    }
}