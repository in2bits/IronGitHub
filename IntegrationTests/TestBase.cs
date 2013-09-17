using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IronGitHub;
using NUnit.Framework;
using Authorization = IronGitHub.Entities.Authorization;

namespace IntegrationTests
{
    public class WithGitHubApi
    {
        private static List<Authorization> _authorizations = new List<Authorization>();
        protected GitHubApi Api;

        [SetUp]
        public void CreateGitHubApi()
        {
            Api = new GitHubApi();
        }

        /// <summary>
        /// Logs into the GitHub API using the test account for integration testing. If there is a matching authorization for the given scopes from a previous test, it is reused
        /// </summary>
        /// <param name="scopes">The scopes to request access to</param>
        protected async Task Authorize(IEnumerable<Scopes> scopes = null)
        {
            var authorization = _authorizations.FirstOrDefault(x => x.Scopes.Matches(scopes));

            if (authorization == null)
            {
                authorization = await Api.Authorize(
                    new NetworkCredential(IntegrationTestParameters.GitHubUsername, IntegrationTestParameters.GitHubPassword),
                    scopes,
                    "IronGithub Integration Test");
                _authorizations.Add(authorization);
            }
            else
            {
                Api.Context.Authorize(authorization);
            }
        }
    }
}