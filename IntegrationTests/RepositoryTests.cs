using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public class RepositoryTests : WithGitHubApi
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            this.CreateGitHubApi();
        }

        [Test]
        async public Task GetRepository()
        {
            var repo = await Api.Repositories.Get("apitestaccount", "apitest");

            repo.Name.Should().Be("apitest");
            repo.FullName.Should().Be("apitestaccount/apitest");
            repo.Description.Should().Be("Repository for testing the GitHub API");
            repo.Homepage.Should().Be("http://google.com");
            repo.HtmlUrl.Should().Be("https://github.com/apitestaccount/apitest");
            repo.MasterBranch.Should().Be("master");
            repo.DefaultBranch.Should().Be("master");
            repo.Size.Should().BeGreaterThan(0);
            repo.OpenIssuesCount.Should().BeGreaterOrEqualTo(2);
            repo.ForksCount.Should().NotBe(null);
            repo.Forks.Should().NotBe(null);
            repo.Owner.ShouldBeApiTestAccount();

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
        }

        [Test]
        async public Task ListRepositores()
        {
            var repos = await Api.Repositories.List();

            repos.Should().NotBeEmpty();
        }

        [Test]
        async public Task ListRepositoresSince()
        {
            var repos = await Api.Repositories.List(300);

            repos.Should().NotBeEmpty();
            repos.First().Id.Should().BeGreaterOrEqualTo(300);
        }
    }
}
