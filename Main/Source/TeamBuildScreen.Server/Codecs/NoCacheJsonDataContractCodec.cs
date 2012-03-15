using OpenRasta.Codecs;
using OpenRasta.Web;

namespace TeamBuildScreen.Server.Codecs
{
    public class NoCacheJsonDataContractCodec : JsonDataContractCodec,  IMediaTypeWriter
	{
        void IMediaTypeWriter.WriteTo(object entity, IHttpEntity response, string[] paramneters)
        {
            base.WriteTo(entity, response, paramneters);
            response.Headers.Add("Cache-Control", "max-age=0,no-cache,no-store");
        }
	}
}