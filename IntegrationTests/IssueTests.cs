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
    public class IssueTests : WithGitHubApi
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            this.CreateGitHubApi();
        }

        [Test]
        async public Task GetIssueWithAssignedUserAndMilestone()
        {
            var issue = await Api.Issues.Get("apitestaccount", "apitest", 1);

            issue.Number.Should().Be(1);
            issue.Id.Should().Be(18332016);
            issue.Title.Should().Be("Open issue");
            issue.State.Should().Be(IssueStates.Open);
            issue.Body.Should().Be("This is an open issue assigned to me with a milesone");
            issue.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 8, 21, 1, 47, 15, DateTimeKind.Utc));
            issue.UpdatedAt.ToUniversalTime().Should().BeAfter(issue.CreatedAt);
            issue.Comments.Should().BeGreaterThan(0);
            issue.ClosedAt.Should().Be(null);

            // URLs
            issue.Url.Should().Be("https://api.github.com/repos/apitestaccount/apitest/issues/1");
            issue.LabelsUrl.Should().Be("https://api.github.com/repos/apitestaccount/apitest/issues/1/labels{/name}");
            issue.CommentsUrl.Should().Be("https://api.github.com/repos/apitestaccount/apitest/issues/1/comments");
            issue.EventsUrl.Should().Be("https://api.github.com/repos/apitestaccount/apitest/issues/1/events");
            issue.HtmlUrl.Should().Be("https://github.com/apitestaccount/apitest/issues/1");

            // User
            // WTF why is this not a user object
            issue.User.Should().NotBeNullOrEmpty();

            // Assignee
            issue.Assignee.ShouldBeApiTestAccount();

            // Labels
            issue.Labels.Should().HaveCount(2);
            var label = issue.Labels.First();
            label.Url.Should().Be("https://api.github.com/repos/apitestaccount/apitest/labels/label1");
            label.Name.Should().Be("label1");
            label.Color.Should().Be("207de5");

            // Milestone
            issue.Milestone.Should().NotBeNull();
            issue.Milestone.Title.Should().Be("milestone1");
            issue.Milestone.Description.Should().Be("Milestone with a description");
            issue.Milestone.Number.Should().Be(1);
            issue.Milestone.Creator.ShouldBeApiTestAccount();
            issue.Milestone.OpenIssues.Should().Be(1);
            issue.Milestone.ClosedIssues.Should().Be(0);
            issue.Milestone.State.Should().Be(MilestoneStates.Open);
            issue.Milestone.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 8, 21, 01, 47, 15, DateTimeKind.Utc));
            issue.Milestone.UpdatedAt.ToUniversalTime().Should().BeAfter(issue.Milestone.CreatedAt);
            issue.Milestone.DueOn.Should().HaveValue();
            issue.Milestone.DueOn.Value.ToUniversalTime()
                .Should()
                .Be(new DateTime(2013, 8, 25, 7, 0, 0, DateTimeKind.Utc));
            issue.Milestone.Url.Should().Be("https://api.github.com/repos/apitestaccount/apitest/milestones/1");
            issue.Milestone.LabelsUrl.Should()
                .Be("https://api.github.com/repos/apitestaccount/apitest/milestones/1/labels");

            // Pull request
            issue.PullRequest.HtmlUrl.Should().BeNull();
            issue.PullRequest.DiffUrl.Should().BeNull();
            issue.PullRequest.PatchUrl.Should().BeNull();
        }

        [Test]
        async public Task GetIssueWithPullRequest()
        {
            var issue = await Api.Issues.Get("apitestaccount", "apitest", 2);

            // Pull request
            issue.PullRequest.HtmlUrl.Should().Be("https://github.com/apitestaccount/apitest/pull/2");
            issue.PullRequest.DiffUrl.Should().Be("https://github.com/apitestaccount/apitest/pull/2.diff");
            issue.PullRequest.PatchUrl.Should().Be("https://github.com/apitestaccount/apitest/pull/2.patch");
        }

        [Test]
        async public Task GetClosedIssue()
        {
            var issue = await Api.Issues.Get("apitestaccount", "apitest", 3);

            issue.State.Should().Be(IssueStates.Closed);
            issue.ClosedAt.Should().BeAfter(issue.CreatedAt);
        }
    }
}
