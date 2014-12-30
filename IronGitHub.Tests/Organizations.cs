using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronGitHub.Tests.Helpers;
using FluentAssertions;

namespace IronGitHub.Tests
{
    [TestClass]
    public class Organizations : AuthenticationBoilerPlate
    {
        [TestMethod]
        public void Should_Setup_GitHub_Api()
        {
            this.CreateGitHubApi();
            this.Api.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Get_Organization()
        {
            Should_Setup_GitHub_Api();

            var organization = Api.Organizations.GetOrganization("apitestorganization").GetAwaiter().GetResult();

            organization.Name.Should().Be("Api Test Org");
            organization.Login.Should().Be("apitestorganization");
            organization.Id.Should().Be(5359692.ToString());
            organization.Type.Should().Be("Organization");
            organization.Blog.Should().Be("http://example.com");
            organization.Location.Should().Be("The Moon");
            organization.Email.Should().Be("testorg@example.com");
            organization.HtmlUrl.Should().Be("https://github.com/apitestorganization");
            organization.PublicRepositoryCount.Should().Be(0);
            organization.PublicGistCount.Should().Be(0);
            organization.FollowingCount.Should().Be(0);
            organization.FollowersCount.Should().BeGreaterOrEqualTo(0);
            organization.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 9, 1, 18, 43, 0, DateTimeKind.Utc));
            organization.UpdatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 9, 1, 18, 50, 48, DateTimeKind.Utc));
        }

    }
}
