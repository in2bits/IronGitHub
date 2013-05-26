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
    public class UserTests
    {
        [TestMethod]
        async public Task GetCurrentUser()
        {
            var api = GitHubApi.Create();
            await api.Account01();
            var user = await api.Users.GetCurrent();
        }
    }
}
