using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenRasta.Web;

namespace TeamBuildScreen.Server.Handlers
{
    public class EmbeddedResourceHandler
    {
        private readonly ICommunicationContext context;
        private Assembly assembly;
        private Dictionary<string, Stream> cache;

        public EmbeddedResourceHandler(ICommunicationContext context)
        {
            this.context = context;
            this.assembly = this.GetType().Assembly;
            this.cache = new Dictionary<string, Stream>();
        }

        public Stream Get(string path)
        {
            var fileName = "TeamBuildScreen.Server.Views" + this.context.Request.Uri.AbsolutePath.Substring(8).Replace('/', '.');

            if (!this.cache.ContainsKey(fileName))
            {
                this.cache[fileName] = this.assembly.GetManifestResourceStream(fileName);
            }
            
            return this.cache[fileName];
        }
    }
}