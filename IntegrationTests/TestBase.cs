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
        //private static List<Authorization> _authorizations = new List<Authorization>();

        private GitHubApiContext _context;
        
        protected GitHubApi Api;

        [SetUp]
        public void CreateGitHubApi()
        {
            _context = new GitHubApiContext();
            Api = new GitHubApi(_context);
        }

        /// <summary>
        /// Logs into the GitHub API using the test account for integration testing. If there is a matching authorization for the given scopes from a previous test, it is reused
        /// </summary>
        /// <param name="scopes">The scopes to request access to</param>
        protected async Task Authorize(IEnumerable<Scopes> scopes = null)
        {
            //var authorization = Api.Context.Authorization.FirstOrDefault(x => x.Scopes.Matches(scopes));

            //if (authorization == null)
            //{
                var authorization = await Api.Authorize(
                    new NetworkCredential(IntegrationTestParameters.GitHubUsername, IntegrationTestParameters.GitHubPassword),
                    scopes,
                    "IronGithub Integration Test");
                Api.Context.Authorize(authorization);
                //_authorizations.Add(authorization);
            //}
            //else
            //{
            //    Api.Context.Authorize(authorization);
            //}
        }
    }
}