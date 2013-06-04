using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IronGitHub.Exceptions;
using ServiceStack.Text;

namespace IronGitHub
{
    public static class HttpExtensions
    {
        private const string ApplicationJson = "application/json";
// ReSharper disable InconsistentNaming
        private const string DELETE = "DELETE";
        private const string POST = "POST";
// ReSharper restore InconsistentNaming

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

        async public static Task<ApiResponse> Delete(this HttpWebRequest request)
        {
            request.Method = DELETE;
            return await request.Complete();
        }

        async public static Task PostAsJson<T>(this HttpWebRequest request, T body)
        {
            request.Method = POST;
            request.ContentType = ApplicationJson;
            var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false);

            requestStream.WriteAsJson(body);
        }

        async public static Task<ApiResponse> Complete(this HttpWebRequest request)
        {
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync();
                return new ApiResponse {HttpResponse = response};
            }
            catch (WebException wex)
            {
                response = wex.Response as HttpWebResponse;
                if (response == null)
                    throw;
            }
            var errorResponse = response.Deserialize<GitHubErrorResponse>();
            throw GitHubErrorExceptionFactory.From(response, errorResponse);
        }

        async public static Task<ApiResponse<T>> Complete<T>(this HttpWebRequest request)
        {
            var apiResponse = await request.Complete();
            var result = apiResponse.HttpResponse.Deserialize<T>();
            return new ApiResponse<T>{HttpResponse = apiResponse.HttpResponse, Result = result};
        }
    }

    public class ApiResponse
    {
        public HttpWebResponse HttpResponse { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }
    }
}
