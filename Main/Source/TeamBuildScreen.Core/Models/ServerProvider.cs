using System.Collections.Generic;

namespace TeamBuildScreen.Core.Models
{
    using System.ComponentModel.Composition;

    public class ServerProvider
    {
        [ImportMany]
        public IEnumerable<IServerManifest> Manifests { get; set; }
    }
}