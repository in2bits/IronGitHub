using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IronGitHub.Entities;
using NUnit.Framework;
using ServiceStack.Text;
using JsonExtensions = IronGitHub.JsonExtensions;

namespace UnitTests
{
    [TestFixture]
    class GistTests
    {
        [Test]
        public void DictionaryShouldSerializeNullEntry()
        {
            var d = new Dictionary<string, Gist.NewGistPost.NewGistFile>();
            d.Add("foo", new Gist.NewGistPost.NewGistFile());
            d.Add("bar", null);
            JsonExtensions.Init();

            var json = JsonSerializer.SerializeToString(d);

            json.Should().Contain("bar");
        }
    }

    [TestFixture]
    public class GistUrlParseTests
    {
        [Datapoint]
        public string ApiUrl = "https://api.github.com/gists/5731704";

        [Datapoint]
        public string HtmlUrl = "https://api.github.com/gists/5731704";

        [Datapoint]
        public string RawUrl = "https://gist.github.com/raw/5731704/f70d7bba4ae1f07682e0358bd7a2068094fc023b/theAnswer";

        [Theory]
        public void ParseUrl(string url)
        {
            Gist.ParseIdFromUrl(url).Should().Be(5731704);
        }
    }
}
