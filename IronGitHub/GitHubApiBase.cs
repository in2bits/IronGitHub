using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IronGitHub.Exceptions;

using Authorization = IronGitHub.Entities.Authorization;

namespace IronGitHub
{
    public abstract class GitHubApiBase
    {
        private const string ApplicationJson = "application/json";
        private const string POST = "POST";
        private const string DELETE = "DELETE";
        private const string PATCH = "PATCH";

        static GitHubApiBase()
        {
            JsonExtensions.Init();
        }

        public GitHubApiBase(GitHubApiContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
        }

        public GitHubApiContext Context { get; private set; }

        protected HttpWebRequest CreateRequest(string path, IDictionary<string, string> parameters = null)
        {
            Context.Authorization.CheckRateLimit();

            var uriString = "https://" + Context.Configuration.Domain + path;
            if (parameters != null)
            {
                var separated = false;
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    if (string.IsNullOrEmpty(pair.Value))
                        continue;
                    if (!separated)
                    {
                        uriString += "?";
                        separated = true;
                    }
                    else
                    {
                        uriString += "&";
                    }
                    uriString += string.Format("{0}={1}", pair.Key, Uri.EscapeDataString(pair.Value));
                }
            }
            var uri = new Uri(uriString);
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var auth = Context.Authorization;
            if (auth != Authorization.Anonymous)
            {
                request.Headers["Authorization"] = "token " + auth.Token;
            }
            request.UserAgent = Context.Configuration.UserAgent;
            request.KeepAlive = false;
            return request;
        }

        protected void OnRequestCompleted(HttpWebResponse response)
        {
            Context.Authorization.UpdateRateLimit(response);
        }

        async protected Task<ApiResponse> Complete(HttpWebRequest request)
        {
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse) await request.GetResponseAsync().ConfigureAwait(false);
                return new ApiResponse {HttpResponse = response};
            }
            catch (WebException wex)
            {
                response = wex.Response as HttpWebResponse;
                if (response == null)
                    throw;
            }
            finally
            {
                if (response != null)
                    OnRequestCompleted(response);
            }

            var errorResponse = response.Deserialize<GitHubErrorResponse>();
            try
            {
                throw GitHubErrorExceptionFactory.From(response, errorResponse);
            }
            catch (UnauthorizedException)
            {
                Context.ClearAuthorization();
                throw;
            }
        }

        async protected Task<ApiResponse<T>> Complete<T>(HttpWebRequest request)
        {
            var apiResponse = await Complete(request);
            var result = apiResponse.HttpResponse.Deserialize<T>();
            return new ApiResponse<T> { HttpResponse = apiResponse.HttpResponse, Result = result };
        }

        async protected Task SendAsJson<TBody>(HttpWebRequest request, TBody body)
        {
            request.ContentType = ApplicationJson;
            var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false);
            requestStream.WriteAsJson(body);
        }

        async protected Task<ApiResponse<TResult>> PostAsJson<TPost, TResult>(HttpWebRequest request, TPost body)
        {
            request.Method = POST;
            await SendAsJson(request, body);
            return await Complete<TResult>(request);
        }

        async protected Task<ApiResponse> Delete(HttpWebRequest request)
        {
            request.Method = DELETE;
            return await Complete(request);
        }

        async protected Task<ApiResponse<TResult>> Patch<TPatch, TResult>(HttpWebRequest request, TPatch content)
        {
            request.Method = PATCH;
            await SendAsJson(request, content);
            return await Complete<TResult>(request);
        }
    }
}