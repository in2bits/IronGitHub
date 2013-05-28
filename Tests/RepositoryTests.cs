using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronGitHub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        async public Task GetRepository()
        {
            var api = GitHubApi.Create();
            var repo = await api.Repositories.Get(123);
        }

        [TestMethod]
        async public Task ListRepositores()
        {
            var api = GitHubApi.Create();
            var repos = await api.Repositories.List();

            Assert.IsTrue(1 < repos.Count());
        }

        [TestMethod]
        async public Task ListRepositoriesSince10()
        {
            var api = GitHubApi.Create();
            var repos = await api.Repositories.List(10);
            Assert.IsTrue(10 <= repos.ElementAt(0).Id);
        }

        [TestMethod]
        async public Task ListForUser()
        {
            var api = GitHubApi.Create();
            var user = api.Users.GetCurrent();
        }
    }
}
