using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Primitives;
using IronGitHub;
using IronGitHub.Entities;
using IronGitHub.Exceptions;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    class UserTests
    {
        [Test]
        async public Task GetUserFromId()
        {
            var api = GitHubApi.Create();

            var user = await api.Users.Get(5274545);

            user.ShouldBeApiTestAccount();

            // Extended info
            user.PublicRepos.Should().NotBe(null);
            user.Followers.Should().NotBe(null);
            user.Following.Should().NotBe(null);
            user.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 08, 21, 01, 37, 40, DateTimeKind.Utc));
            user.UpdatedAt.ToUniversalTime().Should().BeAfter(user.CreatedAt);
            user.PublicGists.Should().NotBe(null);
        }

        [Test]
        [ExpectedException(typeof(NotFoundException))]
        async public Task GetInvalidUser()
        {
            var api = GitHubApi.Create();

            await api.Users.Get(0);
        }
    }

    static class UserTestExtensions
    {
        public static void ShouldBeApiTestAccount(this Entity user)
        {
            user.Should().NotBeNull();

            // Generic entity tests
            user.Login.Should().Be("apitestaccount");
            user.Id.Should().Be(5274545.ToString()); // wtf?
            user.Type.Should().Be("User");
            user.GravatarId.Should().Be("9894bc04988dffccbc00e20037eda4ba");

            // URLs
            user.AvatarUrl.Should().Contain("gravatar.com");
            user.Url.Should().Be("https://api.github.com/users/apitestaccount");
            user.HtmlUrl.Should().Be("https://github.com/apitestaccount");
        }
    }
}
