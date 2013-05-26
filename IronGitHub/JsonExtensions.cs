using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IronGitHub
{
    public static class JsonExtensions
    {
        private static readonly JsonSerializer _serializer = new JsonSerializer();

        public static T Deserialize<T>(this TextReader reader)
        {
            using (var jsonReader = new JsonTextReader(reader))
            {
                var result = _serializer.Deserialize<T>(jsonReader);
                return result;
            }
        }

        async public static Task WriteAsJson<T>(this Stream stream, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            await jsonStream.CopyToAsync(stream).ConfigureAwait(false);
        }
    }
}
