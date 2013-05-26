using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IronGitHub.Exceptions;
using ServiceStack.Text;

namespace IronGitHub
{
    public static class HttpExtensions
    {
        static HttpExtensions()
        {
            JsConfig<Scopes>.SerializeFn = s => s.ToJsonValue();
        }

        private const string ApplicationJson = "application/json";
        private const string POST = "POST";

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

        async public static Task<T> Deserialize<T>(this HttpWebResponse response)
        {
            if (response == null)
                throw new ArgumentNullException("response");
            var stream = response.GetResponseStream();
            if (stream == null)
                return default(T);
            return JsonSerializer.DeserializeResponse<T>(response);
        }

        async public static Task PostAsJson(this HttpWebRequest request, object body)
        {
            request.Method = "POST";
            request.ContentType = ApplicationJson;
            var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false);

            requestStream.WriteAsJson(body);
        }

        async public static Task<T> Complete<T>(this HttpWebRequest request, Action<GitHubErrorResponse> handleGitHubError = null)
        {
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
                var auth = await response.Deserialize<T>();
                return auth;
            }
            catch (WebException wex)
            {
                response = wex.Response as HttpWebResponse;
                if (response == null)
                    throw;
            }
            var errorResponse = await response.Deserialize<GitHubErrorResponse>();
            if (handleGitHubError == null)
                throw GitHubErrorExceptionFactory.From(response, errorResponse);
            handleGitHubError(errorResponse);
            return default(T);
        }
    }
}
