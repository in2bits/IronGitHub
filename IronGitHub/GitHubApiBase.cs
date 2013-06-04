using System;
using System.Net;

namespace IronGitHub
{
    public abstract class GitHubApiBase
    {
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

        protected HttpWebRequest CreateRequest(string path)
        {
            var uri = new Uri("https://" + Context.Configuration.Domain + path);
            var request = WebRequest.CreateHttp(uri);
            var auth = Context.Authorization;
            if (auth != null)
            {
                request.Headers["Authorization"] = "token " + auth.Token;
            }
            request.UserAgent = Context.Configuration.UserAgent;
            return request;
        }
    }
}