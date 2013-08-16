using System;
using System.Linq;
using System.Threading.Tasks;
using IronGitHub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        async public Task SearchUsers()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var userList = await api.Search.Users("timerickson");
            var user = userList.Users.FirstOrDefault(x => x.Id == "user-57726");
            Assert.IsNotNull(user);

            Assert.AreEqual(new DateTime(2009, 2, 25, 3, 57, 50, DateTimeKind.Utc), user.Created);
            Assert.AreEqual(new DateTime(2009, 2, 25, 3, 57, 50, DateTimeKind.Utc), user.CreatedAt);
            Assert.AreEqual(4, user.Followers);
            Assert.AreEqual(4, user.FollowersCount);
            Assert.AreEqual("Tim Erickson", user.FullName);
            Assert.AreEqual("e9fbbfd2de96fdb0cec592a4b6792f0e", user.GravatarId);
            Assert.AreEqual("user-57726", user.Id);
            Assert.AreEqual("JavaScript", user.Language);
            Assert.AreEqual("Seattle, WA", user.Location);
            Assert.AreEqual("timerickson", user.Login);
            Assert.AreEqual("Tim Erickson", user.Name);
            Assert.AreEqual(4, user.PublicRepoCount);
            Assert.AreEqual(4, user.Repos);
            Assert.IsTrue(0 < user.Score);
            Assert.AreEqual("user", user.Type);
            Assert.AreEqual("timerickson", user.Username);
        }

        [TestMethod]
        async public Task SearchRepositories()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var repoList = await api.Search.Repositories("IronGitHub");
            var repo = repoList.Repositories.FirstOrDefault(x => x.Name == "IronGitHub");
            Assert.IsNotNull(repo);

            Assert.AreEqual(new DateTime(2013, 5, 26, 2, 25, 58, DateTimeKind.Utc), repo.Created);
            Assert.AreEqual(new DateTime(2013, 5, 26, 2, 25, 58, DateTimeKind.Utc), repo.CreatedAt);
            Assert.AreEqual("C# GitHub Api", repo.Description);
            Assert.AreEqual(1, repo.Followers);
            Assert.AreEqual(false, repo.Fork);
            Assert.AreEqual(0, repo.Forks);
            Assert.AreEqual(true, repo.HasDownloads);
            Assert.AreEqual(true, repo.HasIssues);
            Assert.AreEqual(true, repo.HasWiki);
            Assert.AreEqual(null, repo.Homepage);
            Assert.AreEqual("C#", repo.Language);
            Assert.AreEqual("IronGitHub", repo.Name);
            Assert.AreEqual(2, repo.OpenIssues);
            Assert.AreEqual("in2bits", repo.Owner);
            Assert.AreEqual(false, repo.Private);
            Assert.IsTrue(new DateTime(2013, 6, 4, 0, 13, 8, DateTimeKind.Utc) <= repo.Pushed);
            Assert.IsTrue(new DateTime(2013, 6, 4, 0, 13, 8, DateTimeKind.Utc) <= repo.PushedAt);
            Assert.IsTrue(0 < repo.Score);
            Assert.IsTrue(260 <= repo.Size);
            Assert.AreEqual("repo", repo.Type);
            Assert.AreEqual("https://github.com/in2bits/IronGitHub", repo.Url);
            Assert.AreEqual("in2bits", repo.Username);
            Assert.IsTrue(1 <= repo.Watchers);
        }

        [TestMethod]
        async public Task SearchIssues()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var text = "TEST Open Issue";
            var issueList = await api.Search.Issues("in2bits", "IronGitHub", IssueStates.Open, text);
            Assert.IsTrue(1 <= issueList.Issues.Count(x => x.Title == text));
            var issue = issueList.Issues.First();
            Assert.AreEqual("This is a test issue that should remain open.", issue.Body);
            Assert.AreEqual(1, issue.Comments);
            Assert.AreEqual(new DateTime(2013,6,17,23,30,44,DateTimeKind.Utc), issue.CreatedAt);
            Assert.AreEqual("e9fbbfd2de96fdb0cec592a4b6792f0e", issue.GravatarId);
            Assert.AreEqual("https://github.com/in2bits/IronGitHub/issues/2", issue.HtmlUrl);
            Assert.AreEqual(2, issue.Labels.Count());
            Assert.AreEqual("TEST", issue.Labels.ElementAt(0));
            Assert.AreEqual("TEST2", issue.Labels.ElementAt(1));
            Assert.AreEqual(2, issue.Number);
            Assert.AreEqual(1, issue.Position);
            Assert.AreEqual(IssueStates.Open, issue.State);
            Assert.AreEqual(text, issue.Title);
            Assert.AreEqual(new DateTime(2013,6,19,19,42,0,DateTimeKind.Utc), issue.UpdatedAt);
            Assert.AreEqual("timerickson", issue.User);
            Assert.AreEqual(0, issue.Votes);
        }

        [TestMethod]
        async public Task SearchEmail()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var email = "tim@in2bits.org";
            var results = await api.Search.Email(email);
            Assert.IsTrue(results.Users.Any());
            var user = results.Users.First();

            Assert.AreEqual("http://myxls.in2bits.org", user.Blog);
            Assert.AreEqual("", user.Company);
            Assert.AreEqual(new DateTime(2009, 2, 25, 3, 57, 50, DateTimeKind.Utc), user.Created);
            Assert.AreEqual(new DateTime(2009, 2, 25, 3, 57, 50, DateTimeKind.Utc), user.CreatedAt);
            Assert.AreEqual("tim@in2bits.org", user.Email);
            Assert.AreEqual(4, user.FollowersCount);
            Assert.AreEqual(3, user.FollowingCount);
            Assert.AreEqual("e9fbbfd2de96fdb0cec592a4b6792f0e", user.GravatarId);
            Assert.AreEqual(57726, user.Id);
            Assert.AreEqual("Seattle, WA", user.Location);
            Assert.AreEqual("timerickson", user.Login);
            Assert.AreEqual("Tim Erickson", user.Name);
            Assert.IsTrue(1 <= user.PublicGistCount);
            Assert.AreEqual(4, user.PublicRepoCount);
            Assert.AreEqual("User", user.Type);
        }
    }
}
