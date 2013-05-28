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
        async public Task FindUser()
        {
            var api = GitHubApi.Create();
            var userList = await api.Search.Users("timerickson");
            Assert.IsTrue(1 == userList.Users.Count(x => x.Id == "user-57726"));
        }
    }
}
