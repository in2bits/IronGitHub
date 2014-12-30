using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronGitHub.Tests.Helpers;
using FluentAssertions;

namespace IronGitHub.Tests
{
    [TestClass]
    public class Issues : AuthenticationBoilerPlate
    {
        [TestMethod]
        public void Should_Setup_GitHub_Api()
        {
            this.CreateGitHubApi();
            this.Api.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Get_Issue_And_Milestone()
        {
            Should_Setup_GitHub_Api();

            var issue = Api.Issues.Get("apitestaccount", "apitest", 1).GetAwaiter().GetResult();

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
            issue.User.Should().NotBeNull();

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
            issue.Milestone.OpenIssues.Should().Be(1);
            issue.Milestone.ClosedIssues.Should().Be(0);
            issue.Milestone.State.Should().Be(MilestoneStates.Open);
            issue.Milestone.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 8, 21, 01, 47, 15, DateTimeKind.Utc));
            issue.Milestone.UpdatedAt.ToUniversalTime().Should().BeAfter(issue.Milestone.CreatedAt);
            issue.Milestone.DueOn.Should().HaveValue();
            issue.Milestone.DueOn.Value.ToUniversalTime().Should().Be(new DateTime(2013, 8, 25, 7, 0, 0, DateTimeKind.Utc));
            issue.Milestone.Url.Should().Be("https://api.github.com/repos/apitestaccount/apitest/milestones/1");
            issue.Milestone.LabelsUrl.Should().Be("https://api.github.com/repos/apitestaccount/apitest/milestones/1/labels");
        }

        [TestMethod]
        public void Should_Get_Issue_With_Pull_Request()
        {
            Should_Setup_GitHub_Api();

            var issue = Api.Issues.Get("apitestaccount", "apitest", 2).GetAwaiter().GetResult();

            // Pull request
            issue.PullRequest.HtmlUrl.Should().Be("https://github.com/apitestaccount/apitest/pull/2");
            issue.PullRequest.DiffUrl.Should().Be("https://github.com/apitestaccount/apitest/pull/2.diff");
            issue.PullRequest.PatchUrl.Should().Be("https://github.com/apitestaccount/apitest/pull/2.patch");
        }

        [TestMethod]
        public void Should_Get_Closed_Issue()
        {
            Should_Setup_GitHub_Api();

            var issue = Api.Issues.Get("apitestaccount", "apitest", 3).GetAwaiter().GetResult(); ;

            issue.State.Should().Be(IssueStates.Closed);
            issue.ClosedAt.Should().BeAfter(issue.CreatedAt);
        }

        [TestMethod]
        public void Should_Get_Issues_By_Repository()
        {
            Should_Setup_GitHub_Api();

            var issues = Api.Issues.GetForRepository("apitestaccount", "apitest").GetAwaiter().GetResult();
            issues.Count().Should().BeGreaterOrEqualTo(1);
        }

    }
}
