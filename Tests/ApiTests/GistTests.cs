using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronGitHub;
using IronGitHub.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Text;
using JsonExtensions = IronGitHub.JsonExtensions;

namespace Tests
{
    [TestClass]
    public class GistTests
    {
        [TestMethod]
        async public Task CreateNewAnonymousGist()
        {
            var files = new Dictionary<string, string>
                {
                    {"theAnswer", "42"},
                    {"the Question", "I dunno"}
                };
            var api = GitHubApi.Create();
            var gist = await api.Gists.New(files);
        }

        [TestMethod]
        async public Task CreateAndDeleteUserGist()
        {
            var files = new Dictionary<string, string>
                {
                    {"theAnswer", "42"},
                    {"the Question", "I dunno"}
                };
            var api = GitHubApi.Create();
            await api.in2bitstest(new []{Scopes.Gist});
            var gist = await api.Gists.New(files);
            await api.Gists.Delete(gist);
        }

        [TestMethod]
        async public Task GetGist()
        {
            const long id = 5731704;
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var gist = await api.Gists.Get(id);
            Assert.AreEqual("https://api.github.com/gists/5731704/comments", gist.CommentsUrl);
            Assert.AreEqual("https://api.github.com/gists/5731704/commits", gist.CommitsUrl);
            Assert.AreEqual(new DateTime(2013, 6, 7, 19, 22, 52, DateTimeKind.Utc), gist.CreatedAt);
            Assert.IsNull(gist.Description);
            Assert.AreEqual(2, gist.Files.Count);
            Assert.IsTrue(gist.Files.ContainsKey("theAnswer"));
            var file = gist.Files["theAnswer"];
            Assert.AreEqual("42", file.Content);
            Assert.AreEqual("theAnswer", file.Filename);
            Assert.IsNull(file.Language);
            Assert.AreEqual("https://gist.github.com/raw/5731704/f70d7bba4ae1f07682e0358bd7a2068094fc023b/theAnswer", file.RawUrl);
            Assert.AreEqual(2, file.Size);
            Assert.AreEqual("text/plain", file.Type);
            Assert.AreEqual(0, gist.Forks.Count());
            Assert.AreEqual("https://api.github.com/gists/5731704/forks", gist.ForksUrl);
            Assert.AreEqual("https://gist.github.com/5731704.git", gist.GitPullUrl);
            Assert.AreEqual("https://gist.github.com/5731704.git", gist.GitPushUrl);
            Assert.AreEqual(1, gist.History.Count());
            Assert.AreEqual("https://gist.github.com/5731704", gist.HtmlUrl);
            Assert.AreEqual(id, gist.Id);
            Assert.AreEqual(true, gist.Public);
            Assert.IsTrue(gist.CreatedAt <= gist.UpdatedAt);
            Assert.AreEqual("https://api.github.com/gists/5731704", gist.Url);
            Assert.IsNull(gist.User);
        }

        [TestMethod]
        async public Task PatchGist()
        {
            var files = new Dictionary<string, string>
                {
                    {"theAnswer", "42"},
                    {"the Question", "I dunno"}
                };
            var api = GitHubApi.Create();
            await api.in2bitstest(new[] { Scopes.Gist });
            var gist = await api.Gists.New(files);
            Assert.AreEqual("42", gist.Files["theAnswer"].Content);
            var patch = new Gist.EditGistPost(gist);
            var patchFile = patch.Files["theAnswer"];
            patchFile.Filename = "theWrongAnswer";
            patchFile.Content = "43";
            patch.Files["the Question"] = null;
            var patchedGist = await api.Gists.Edit(patch);
            Assert.AreEqual(1, patchedGist.Files.Count);
            var file = patchedGist.Files["theWrongAnswer"];
            Assert.IsNotNull(file);
            Assert.AreEqual("43", file.Content);
            Assert.AreEqual(gist.Id, patchedGist.Id);
        }

        [TestMethod]
        public void DictionaryShouldSerializeNullEntry()
        {
            var d = new Dictionary<string, Gist.NewGistPost.NewGistFile>();
            d.Add("foo", new Gist.NewGistPost.NewGistFile());
            d.Add("bar", null);
            JsonExtensions.Init();
            var json = JsonSerializer.SerializeToString(d);
            Assert.IsTrue(-1 != json.IndexOf("bar"));
        }

        [TestMethod]
        public void ParseIdFromUrl()
        {
            var url = "https://api.github.com/gists/5731704";
            var id = Gist.ParseIdFromUrl(url);
            Assert.AreEqual(5731704, id);
        }

        [TestMethod]
        public void ParseIdFromHtmlUrl()
        {
            var url = "https://gist.github.com/5731704";
            var id = Gist.ParseIdFromUrl(url);
            Assert.AreEqual(5731704, id);
        }

        [TestMethod]
        public void ParseIdFromFileRawUrl()
        {
            var url = "https://gist.github.com/raw/5731704/f70d7bba4ae1f07682e0358bd7a2068094fc023b/theAnswer";
            var id = Gist.ParseIdFromUrl(url);
            Assert.AreEqual(5731704, id);
        }
    }
}
