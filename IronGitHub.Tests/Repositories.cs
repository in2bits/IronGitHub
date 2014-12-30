using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronGitHub.Tests.Helpers;
using FluentAssertions;

namespace IronGitHub.Tests
{
    [TestClass]
    public class Repositories : AuthenticationBoilerPlate
    {
        [TestMethod]
        public void Should_Setup_GitHub_Api()
        {
            this.CreateGitHubApi();
            this.Api.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Get_Repository()
        {
            Should_Setup_GitHub_Api();

            var repo = Api.Repositories.Get("apitestaccount", "apitest").GetAwaiter().GetResult();

            repo.Name.Should().Be("apitest");
            repo.FullName.Should().Be("apitestaccount/apitest");
            repo.Description.Should().Be("Repository for testing the GitHub API");
            repo.Homepage.Should().Be("http://google.com");
            repo.HtmlUrl.Should().Be("https://github.com/apitestaccount/apitest");
            repo.DefaultBranch.Should().Be("master");
            repo.Size.Should().BeGreaterThan(0);
            repo.OpenIssuesCount.Should().BeGreaterOrEqualTo(2);
            repo.ForksCount.Should().NotBe(null);
            repo.Forks.Should().NotBe(null);

            // Dates
            repo.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 08, 21, 01, 44, 13, DateTimeKind.Utc));
            repo.UpdatedAt.Should().BeAfter(repo.CreatedAt);
            repo.PushedAt.Should().BeAfter(repo.CreatedAt);

            // VCS URLs
            repo.GitUrl.Should().Be("git://github.com/apitestaccount/apitest.git");
            repo.SshUrl.Should().Be("git@github.com:apitestaccount/apitest.git");
            repo.CloneUrl.Should().Be("https://github.com/apitestaccount/apitest.git");
            repo.SvnUrl.Should().Be("https://github.com/apitestaccount/apitest");

            // Bools
            repo.Private.Should().BeFalse();
            repo.Fork.Should().BeFalse();
            repo.HasIssues.Should().BeTrue();
            repo.HasDownloads.Should().BeTrue();
            repo.HasWiki.Should().BeTrue();

            repo.Owner.Should().NotBeNull();

        }
    }
}
