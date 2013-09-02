using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IronGitHub;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class GitHubApiTests
    {
        [Test]
        public void GitHubApiCreate()
        {
            var api = GitHubApi.Create();

            api.Should().BeOfType<GitHubApi>();
        }

        [Test]
        public void DefaultContext()
        {
            var api = new GitHubApi();

            api.Context.Should().BeOfType<GitHubApiContext>();
        }

        [Test]
        public void CustomContext()
        {
            var context = new GitHubApiContext();

            var api = new GitHubApi(context);

            api.Context.Should().BeSameAs(context);
        }
    }
}
