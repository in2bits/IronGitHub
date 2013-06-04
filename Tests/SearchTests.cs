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
    public class SearchTests
    {
        [TestMethod]
        async public Task SearchUsers()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var userList = await api.Search.Users("timerickson");
            Assert.IsTrue(1 == userList.Users.Count(x => x.Id == "user-57726"));
        }

        [TestMethod]
        async public Task SearchRepositories()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var repoList = await api.Search.Repositories("IronGitHub");
            Assert.IsTrue(1 == repoList.Repositories.Count(x => x.Name == "IronGitHub"));
        }
    }
}
