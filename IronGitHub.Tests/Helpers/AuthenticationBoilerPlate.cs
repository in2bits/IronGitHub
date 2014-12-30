using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Authorization = IronGitHub.Entities.Authorization;

namespace IronGitHub.Tests.Helpers
{
    public class AuthenticationBoilerPlate
    {
        private object _lockObject = new object();

        protected GitHubApi Api;

        public void CreateGitHubApi()
        {
            Api = GitHubApi.Create();
        }

        protected bool IsAnonymous { get { return Api.Context.Authorization == Authorization.Anonymous; } }

        protected bool HasScope { get { return Api.Context.Authorization.Scopes != null; } }

        /// <summary>
        /// Logs into the GitHub API using the test account for integration testing. If there is a matching authorization for the given scopes from a previous test, it is reused
        /// </summary>
        /// <param name="scopes">The scopes to request access to</param>
        protected void Authorize(IEnumerable<Scopes> scopes = null)
        {
            if (IsAnonymous || (scopes != null && HasScope && !Api.Context.Authorization.Scopes.Intersect(scopes).Any()))
            {
                // Read from App.Settings
                var credentials = new NetworkCredential(TestConfiguration.GitHubUsername, TestConfiguration.GitHubPassword);

                var authorization = Api.Authorize(credentials, scopes, TestConfiguration.TestRequestIdentifierDescription).GetAwaiter().GetResult();

                lock (_lockObject)
                {
                    Api.Context.Authorize(authorization);
                }
            }
        }
    }
}