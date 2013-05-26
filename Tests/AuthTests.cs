using System;
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
        async public Task ReadCredentials()
        {
            var creds = TestExtensions.LoadCredentials("test.creds.01.txt");
            var api = GitHubApi.Create();
            await api.Authorize(creds, Enumerable.Empty<Scopes>(), "testing");
        }
    }
}
