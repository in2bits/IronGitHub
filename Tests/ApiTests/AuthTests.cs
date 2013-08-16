using System.Threading.Tasks;
using IronGitHub;
using IronGitHub.Entities;
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
            Assert.AreEqual(Authorization.Anonymous, api.Context.Authorization);
            await api.in2bitstest();
            Assert.IsNotNull(api.Context.Authorization);
        }

        [TestMethod]
        async public Task AuthorizeWithGistScope()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest(new[] {Scopes.Gist});
            Assert.IsTrue(api.Context.Authorization.Scopes.Matches(new []{Scopes.Gist}));
        }

        [TestMethod]
        async public Task AuthorizeWithUserEmailScope()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest(new[] { Scopes.UserEmail });
            Assert.IsTrue(api.Context.Authorization.Scopes.Matches(new[] { Scopes.UserEmail }));
        }

        [TestMethod]
        async public Task AuthorizeWithGistAndUserEmailScopes()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest(new[] { Scopes.Gist, Scopes.UserEmail });
            Assert.IsTrue(api.Context.Authorization.Scopes.Matches(new[] { Scopes.Gist, Scopes.UserEmail }));
        }
    }
}
