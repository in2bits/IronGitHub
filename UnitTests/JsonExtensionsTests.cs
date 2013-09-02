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
    class JsonExtensionsTests
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
}
