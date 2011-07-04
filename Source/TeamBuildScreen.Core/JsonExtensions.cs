using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TeamBuildScreen.Core
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T o)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, o);
            var result = Encoding.Default.GetString(stream.ToArray());
            stream.Close();

            return result;
        }
    }
}
