using System;
using System.Runtime.Remoting.Contexts;
using IronGitHub.Entities;

namespace IronGitHub
{
    public class GitHubApiContext
    {
        public GitHubApiContext(Configuration configuration = null)
        {
            if (configuration == null)
                configuration = new Configuration();
            Configuration = configuration;
            Authorization = Authorization.Anonymous;
        }

        public Configuration Configuration { get; private set; }

        public Authorization Authorization { get; private set; }

        public void ClearAuthorization()
        {
            if (Authorization == null)
                return;
            Authorization.Invalidate();
            Authorization = null;
        }

        public void Authorize(Authorization authorization)
        {
            if (authorization == null)
                throw new ArgumentNullException("authorization");
            Authorization = authorization;
        }
    }
}