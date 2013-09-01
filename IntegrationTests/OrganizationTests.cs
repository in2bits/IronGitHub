using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests
{
    class OrganizationTests : WithGitHubApi
    {
        [Test]
        async public Task GetOrganization()
        {
            var organization = await Api.Organizations.GetOrganization("apitestorganization");

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
            // No updated_at?
        }
    }
}
