using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IronGitHub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        async public Task AuthorizeWithCredential()
        {
            var api = GitHubApi.Create();
            Assert.IsNull(api.Context.Authorization);
            await api.Account01();
            Assert.IsNotNull(api.Context.Authorization);
        }

        [TestMethod]
        async public Task AuthorizeWithGistScope()
        {
            var api = GitHubApi.Create();
            await api.Account01(new[] {Scopes.Gist});
        }

        [TestMethod]
        async public Task AuthorizeWithUserEmailScope()
        {
            var api = GitHubApi.Create();
            await api.Account01(new[] { Scopes.UserEmail });
        }

        [TestMethod]
        async public Task AuthorizeWithGistAndUserEmailScopes()
        {
            var api = GitHubApi.Create();
            await api.Account01(new[] {Scopes.Gist, Scopes.UserEmail});
        }
    }
}
