using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronGitHub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        async public Task GetRepository()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var repo = await api.Repositories.Get("in2bits", "IronGitHub");
            Assert.IsNotNull(repo);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/{archive_format}{/ref}", repo.ArchiveUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/assignees{/user}", repo.AssigneesUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/git/blobs{/sha}", repo.BlobsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/branches{/branch}", repo.BranchesUrl);
            Assert.AreEqual("https://github.com/in2bits/IronGitHub.git", repo.CloneUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/collaborators{/collaborator}", repo.CollaboratorsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/comments{/number}", repo.CommentsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/commits{/sha}", repo.CommitsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/compare/{base}...{head}", repo.CompareUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/contents/{+path}", repo.ContentsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/contributors", repo.ContributorsUrl);
            Assert.AreEqual(new DateTime(2013, 5, 26, 2, 25, 58, DateTimeKind.Utc), repo.CreatedAt);
            Assert.AreEqual("master", repo.DefaultBranch);
            Assert.AreEqual("C# GitHub Api", repo.Description);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/downloads", repo.DownloadsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/events", repo.EventsUrl);
            Assert.AreEqual(false, repo.Fork);
            Assert.AreEqual(0, repo.Forks);
            Assert.AreEqual(0, repo.ForksCount);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/forks", repo.ForksUrl);
            Assert.AreEqual("in2bits/IronGitHub", repo.FullName);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/git/commits{/sha}", repo.GitCommitsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/git/refs{/sha}", repo.GitRefsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/git/tags{/sha}", repo.GitTagsUrl);
            Assert.AreEqual("git://github.com/in2bits/IronGitHub.git", repo.GitUrl);
            Assert.AreEqual(true, repo.HasDownloads);
            Assert.AreEqual(true, repo.HasIssues);
            Assert.AreEqual(true, repo.HasWiki);
            Assert.AreEqual(null, repo.Homepage);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/hooks", repo.HooksUrl);
            Assert.AreEqual("https://github.com/in2bits/IronGitHub", repo.HtmlUrl);
            Assert.AreEqual(10292608, repo.Id);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/issues/comments/{number}", repo.IssueCommentUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/issues/events{/number}", repo.IssueEventsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/issues{/number}", repo.IssuesUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/keys{/key_id}", repo.KeysUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/labels{/name}", repo.LabelsUrl);
            Assert.AreEqual("C#", repo.Language);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/languages", repo.LanguagesUrl);
            Assert.AreEqual("master", repo.MasterBranch);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/merges", repo.MergesUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/milestones{/number}", repo.MilestonesUrl);
            Assert.AreEqual(null, repo.MirrorUrl);
            Assert.AreEqual("IronGitHub", repo.Name);
            Assert.AreEqual(0, repo.NetworkCount);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/notifications{?since,all,participating}", repo.NotificationsUrl);
            Assert.AreEqual(0, repo.OpenIssues);
            Assert.AreEqual(0, repo.OpenIssuesCount);
            Assert.IsNotNull(repo.Permissions);
            var perms = repo.Permissions;
            Assert.AreEqual(false, perms.Admin);
            Assert.AreEqual(true, perms.Pull);
            Assert.AreEqual(false, perms.Push);
            Assert.AreEqual(false, repo.Private);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/pulls{/number}", repo.PullsUrl);
            Assert.IsTrue(new DateTime(2013, 6, 4, 0, 13, 8, DateTimeKind.Utc) <= repo.PushedAt);
            Assert.AreEqual(260, repo.Size);
            Assert.AreEqual("git@github.com:in2bits/IronGitHub.git", repo.SshUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/stargazers", repo.StargazersUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/statuses/{sha}", repo.StatusesUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/subscribers", repo.SubscribersUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/subscription", repo.SubscriptionUrl);
            Assert.AreEqual("https://github.com/in2bits/IronGitHub", repo.SvnUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/tags", repo.TagsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/teams", repo.TeamsUrl);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub/git/trees{/sha}", repo.TreesUrl);
            Assert.IsTrue(new DateTime(2013,6,4,0,13,11) <= repo.UpdatedAt);
            Assert.AreEqual("https://api.github.com/repos/in2bits/IronGitHub", repo.Url);
            Assert.AreEqual(0, repo.Watchers);
            Assert.AreEqual(0, repo.WatchersCount);
        }

        [TestMethod]
        async public Task ListRepositores()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var repos = await api.Repositories.List();

            Assert.IsTrue(1 < repos.Count());
        }

        [TestMethod]
        async public Task ListRepositoriesSince10()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var repos = await api.Repositories.List(10);
            Assert.IsTrue(10 <= repos.ElementAt(0).Id);
        }

        [TestMethod]
        async public Task ListForUser()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var user = api.Users.GetCurrent();
        }
    }
}
