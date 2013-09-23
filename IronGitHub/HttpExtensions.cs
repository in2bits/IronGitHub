using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Text;

namespace IronGitHub
{
    public static class HttpExtensions
    {
        public static void AddAuthorizationCredential(this HttpWebRequest request, NetworkCredential credential)
        {
            var encodedCreds = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", credential.UserName, credential.Password)));
            request.Headers.Add("Authorization", "Basic " + encodedCreds);
        }

        async public static Task<Stream> GetRequestStreamAsync(this HttpWebRequest request)
        {
            return await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream, request.EndGetRequestStream, null).ConfigureAwait(false);
        }

        async public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            return (await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null).ConfigureAwait(false)) as HttpWebResponse;
        }

        public static T Deserialize<T>(this HttpWebResponse response)
        {
            if (response == null)
                throw new ArgumentNullException("response");
            var stream = response.GetResponseStream();
            if (stream == null)
                return default(T);
            return JsonSerializer.DeserializeResponse<T>(response);
        }

        public static T Deserialize<T>(this string json)
        {
            return JsonSerializer.DeserializeFromString<T>(json);
        }

        public static string Serialize<T>(this T obj)
        {
            return JsonSerializer.SerializeToString(obj);
        }
    }
}
