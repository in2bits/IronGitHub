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
    public class UserTests : WithGitHubApi
    {
        [SetUp]
        public void Setup()
        {
            this.CreateGitHubApi();
        }

        [Test]
        async public Task GetUserFromId()
        {
            var user = await Api.Users.Get(5274545);

            user.ShouldBeApiTestAccount();
            CheckExtendedInfo(user);
        }

        [Test]
        [Category("Authenticated")]
        async public Task GetAuthenticatedUser()
        {
            await Authorize();

            var user = await Api.Users.GetAuthenticatedUser();

            if (IntegrationTestParameters.GitHubUsername != "apitestaccount")
            {
                Assert.Inconclusive("The GetAuthenticatedUser test is written for apitestaccount");
            }

            user.ShouldBeApiTestAccount();
            CheckExtendedInfo(user);
        }

        [Test]
        [ExpectedException(typeof(NotFoundException))]
        async public Task GetInvalidUser()
        {
            await Api.Users.Get(0);
        }

        private static void CheckExtendedInfo(User user)
        {
            // Extended info
            user.PublicRepos.Should().NotBe(null);
            user.Followers.Should().NotBe(null);
            user.Following.Should().NotBe(null);
            user.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 08, 21, 01, 37, 40, DateTimeKind.Utc));
            user.UpdatedAt.ToUniversalTime().Should().BeAfter(user.CreatedAt);
            user.PublicGists.Should().NotBe(null);

            // Bio
            user.Name.Should().Be("Test Account");
            user.Company.Should().Be("Test, Inc.");
            user.Blog.Should().Be("http://example.com");
            user.Location.Should().Be("The Moon");
            user.Hireable.Should().BeFalse();
            user.Email.Should().Be("apitestaccount@example.com");
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
