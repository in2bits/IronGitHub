using System;
using System.Linq;
using System.Threading.Tasks;
using IronGitHub;
using IronGitHub.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class IssueTests
    {
        [TestMethod]
        async public Task GetAll()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest(new[] { Scopes.Repo, Scopes.PublicRepo });
            //await api.in2bitstest();
            var issueList = await api.Issues.Get();
            Assert.IsTrue(1 <= issueList.Count());
            var issue = issueList.First();
            AssertSecondOpenIssue(issue);
        }

        private static void AssertSecondOpenIssue(Issue issue)
        {
            Assert.IsNotNull(issue.Assignee);
            Assert.AreEqual("This is a second open issue, which should also remain open.", issue.Body);
            Assert.IsNull(issue.ClosedAt);
            Assert.AreEqual(0, issue.Comments);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/issues/3/comments", issue.CommentsUrl);
            Assert.AreEqual(new DateTime(2013, 6, 19, 2, 40, 39, DateTimeKind.Utc), issue.CreatedAt);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/issues/3/events", issue.EventsUrl);
            Assert.AreEqual("https://github.com/in2bits/IronGitHub/issues/3", issue.HtmlUrl);
            Assert.AreEqual(15724626, issue.Id);
            Assert.AreEqual(2, issue.Labels.Count());
            var label = issue.Labels.First();
            Assert.AreEqual("fad8c7", label.Color);
            Assert.AreEqual("TEST", label.Name);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/labels/TEST", label.Url);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/issues/3/labels{/name}", issue.LabelsUrl);
            Assert.IsNotNull(issue.Milestone);
            
            var milestone = issue.Milestone;
            Assert.AreEqual(0, milestone.ClosedIssues);
            Assert.AreEqual(new DateTime(2013, 6, 19, 19, 39, 54, DateTimeKind.Utc), milestone.CreatedAt);
            Assert.IsNotNull(milestone.Creator);
            Assert.AreEqual("Milestone used for testing purposes only.", milestone.Description);
            Assert.AreEqual(new DateTime(2013, 6, 19, 7, 0, 0, DateTimeKind.Utc), milestone.DueOn);
            Assert.AreEqual(359514, milestone.Id);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/milestones/1/labels", milestone.LabelsUrl);
            Assert.AreEqual(1, milestone.Number);
            Assert.AreEqual(2, milestone.OpenIssues);
            Assert.AreEqual(MilestoneStates.Open, milestone.State);
            Assert.AreEqual("TEST Milestone", milestone.Title);
            Assert.AreEqual(new DateTime(2013, 6, 19, 19, 42, 0, DateTimeKind.Utc), milestone.UpdatedAt);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/milestones/1", milestone.Url);

            Assert.AreEqual(3, issue.Number);
            Assert.IsNotNull(issue.PullRequest);
            Assert.IsNull(issue.PullRequest.DiffUrl);
            Assert.IsNull(issue.PullRequest.HtmlUrl);
            Assert.IsNull(issue.PullRequest.PatchUrl);
            if (issue is Issue.IssueItem)
                Assert.IsNotNull((issue as Issue.IssueItem).Repository);
            Assert.AreEqual(IssueStates.Open, issue.State);
            Assert.AreEqual("TEST Second Open Issue", issue.Title);
            Assert.AreEqual(new DateTime(2013, 6, 19, 19, 40, 46, DateTimeKind.Utc), issue.UpdatedAt);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/issues/3", issue.Url);
            Assert.IsNotNull(issue.User);
        }

        [TestMethod]
        async public Task GetIssue()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var issue = await api.Issues.Get("in2bits", "IronGitHub", 3);
            AssertSecondOpenIssue(issue);
        }
    }
}