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
    public class GistTests
    {
        [TestMethod]
        async public Task CreateNewGist()
        {
            var files = new Dictionary<string, string>
                {
                    {"theAnswer", "42"},
                    {"the Question", "I dunno"}
                };
            var content = "The answer is 42.";
            var api = GitHubApi.Create();
            await api.Account01(new []{Scopes.Gist});
            var gist = await api.Gists.New(files);
            var i = 42;
            //Assert.AreEqual(content, gist.Content);
        }
    }
}
