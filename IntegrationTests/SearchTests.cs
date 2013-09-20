using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IronGitHub;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    class SearchTests : WithGitHubApi
    {
        [Test]
        async public Task SearchUsers()
        {
            var userList = await Api.Search.Users("apitestaccount");
            var user = userList.Users.FirstOrDefault(x => x.Username == "apitestaccount");

            user.Should().NotBeNull();
            user.Id.Should().Be("user-5274545");
            user.GravatarId.Should().Be("9894bc04988dffccbc00e20037eda4ba");
            user.Username.Should().Be("apitestaccount");
            user.Login.Should().Be("apitestaccount");
            user.Name.Should().Be("Test Account");
            user.FullName.Should().Be("Test Account");
            user.Location.Should().Be("The Moon");
            user.Type.Should().Be("user");
            user.PublicRepoCount.Should().BeGreaterOrEqualTo(1);
            user.Repos.Should().Be(1);
            user.Followers.Should().BeGreaterOrEqualTo(0);
            user.FollowersCount.Should().BeGreaterOrEqualTo(0);
            user.Score.Should().BeGreaterThan(0);
            user.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 08, 21, 01, 37, 40, DateTimeKind.Utc));
            user.Created.ToUniversalTime().Should().Be(new DateTime(2013, 08, 21, 01, 37, 40, DateTimeKind.Utc));
        }

        [Test]
        async public Task SearchRepositories()
        {
            var repoList = await Api.Search.Repositories("IronGithub"); // apitest is too generic
            var repo = repoList.Repositories.FirstOrDefault(x => x.Url == "https://github.com/in2bits/IronGitHub");

            repo.Should().NotBeNull();
            repo.Type.Should().Be("repo");
            repo.Username.Should().Be("in2bits");
            repo.Owner.Should().Be("in2bits");
            repo.Name.Should().Be("IronGitHub");
            repo.Homepage.Should().BeNullOrEmpty();
            repo.Description.Should().Be("C# GitHub Api v3");
            repo.Language.Should().Be("C#");
            repo.Watchers.Should().BeGreaterThan(0);
            repo.Followers.Should().BeGreaterThan(0);
            repo.Forks.Should().BeGreaterThan(0);
            repo.Size.Should().BeGreaterThan(0);
            repo.OpenIssues.Should().BeGreaterThan(0);
            repo.Score.Should().BeGreaterThan(0);
            repo.HasDownloads.Should().BeTrue();
            repo.HasIssues.Should().BeTrue();
            repo.HasWiki.Should().BeTrue();
            repo.Fork.Should().BeFalse();
            repo.Private.Should().BeFalse();
            repo.Url.Should().Be("https://github.com/in2bits/IronGitHub");
            repo.Created.ToUniversalTime().Should().Be(new DateTime(2013, 5, 26, 2, 25, 58, DateTimeKind.Utc));
            repo.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 5, 26, 2, 25, 58, DateTimeKind.Utc));
            repo.Pushed.ToUniversalTime().Should().BeAfter(repo.Created);
            repo.PushedAt.ToUniversalTime().Should().BeAfter(repo.Created);
        }

        [Test]
        async public Task SearchIssues()
        {
            const string issueName = "Open issue";

            var issueList = await Api.Search.Issues("apitestaccount", "apitest", IssueStates.Open, issueName);
            var issue = issueList.Issues.FirstOrDefault(x => x.Title == issueName);

            issue.Should().NotBeNull();
            issue.Labels.Should().Contain(new[] {"label1", "label2"});
            issue.Votes.Should().BeGreaterOrEqualTo(0);
            issue.Number.Should().Be(1);
            issue.Position.Should().BeGreaterThan(0);
            issue.Title.Should().Be("Open issue");
            issue.Body.Should().Be("This is an open issue assigned to me with a milesone");
            issue.User.Should().Be("apitestaccount");
            issue.GravatarId.Should().Be("9894bc04988dffccbc00e20037eda4ba");
            issue.State.Should().Be(IssueStates.Open);
            issue.Comments.Should().BeGreaterThan(0);
            issue.HtmlUrl.Should().Be("https://github.com/apitestaccount/apitest/issues/1");
            issue.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 8, 21, 1, 47, 15, DateTimeKind.Utc));
            issue.UpdatedAt.ToUniversalTime().Should().BeAfter(issue.CreatedAt);
        }

        [Test]
        async public Task SearchEmail()
        {
            var email = "apitestaccount@example.com";

            var results = await Api.Search.Email(email);
            var user = results.Users.FirstOrDefault();

            user.Should().NotBeNull();
            user.PublicRepoCount.Should().BeGreaterOrEqualTo(1);
            user.PublicGistCount.Should().BeGreaterOrEqualTo(0);
            user.FollowersCount.Should().BeGreaterOrEqualTo(0);
            user.FollowingCount.Should().Be(0);
            user.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 08, 21, 01, 37, 40, DateTimeKind.Utc));
            user.Created.ToUniversalTime().Should().Be(new DateTime(2013, 08, 21, 01, 37, 40, DateTimeKind.Utc));
            user.Name.Should().Be("Test Account");
            user.Company.Should().Be("Test, Inc.");
            user.Blog.Should().Be("http://example.com");
            user.Location.Should().Be("The Moon");
            user.Email.Should().Be(email);
            user.Id.Should().Be(5274545);
            user.Login.Should().Be("apitestaccount");
            user.Type.Should().Be("User");
            user.GravatarId.Should().Be("9894bc04988dffccbc00e20037eda4ba");
        }
    }
}
