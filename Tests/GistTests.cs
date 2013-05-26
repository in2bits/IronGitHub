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
        }

        [TestMethod]
        async public Task GetGist()
        {
            const long id = 5651796;
            var api = GitHubApi.Create();
            var gist = await api.Gists.Get(id);
            Assert.AreEqual("https://api.github.com/gists/5651796/comments", gist.CommentsUrl);
            Assert.AreEqual("https://api.github.com/gists/5651796/commits", gist.CommitsUrl);
            Assert.AreEqual(new DateTime(2013, 5, 26, 5, 23, 30, DateTimeKind.Utc), gist.CreatedAt);
            Assert.IsNull(gist.Description);
            Assert.AreEqual(2, gist.Files.Count);
            Assert.IsTrue(gist.Files.ContainsKey("theAnswer"));
            var file = gist.Files["theAnswer"];
            Assert.AreEqual("42", file.Content);
            Assert.AreEqual("theAnswer", file.Filename);
            Assert.IsNull(file.Language);
            Assert.AreEqual("https://gist.github.com/raw/5651796/f70d7bba4ae1f07682e0358bd7a2068094fc023b/theAnswer", file.RawUrl);
            Assert.AreEqual(2, file.Size);
            Assert.AreEqual("text/plain", file.Type);
            Assert.AreEqual(0, gist.Forks.Count());
            Assert.AreEqual("https://api.github.com/gists/5651796/forks", gist.ForksUrl);
            Assert.AreEqual("https://gist.github.com/5651796.git", gist.GitPullUrl);
            Assert.AreEqual("https://gist.github.com/5651796.git", gist.GitPushUrl);
            Assert.AreEqual(1, gist.History.Count());
            Assert.AreEqual("https://gist.github.com/5651796", gist.HtmlUrl);
            Assert.AreEqual(id, gist.Id);
            Assert.AreEqual(true, gist.Public);
            Assert.IsTrue(gist.CreatedAt <= gist.UpdatedAt);
            Assert.AreEqual("https://api.github.com/gists/5651796", gist.Url);
            Assert.IsNotNull(gist.User);
        }
    }
}
