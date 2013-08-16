using System;
using System.Threading.Tasks;
using IronGitHub;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        async public Task GetAuthenticatedUser()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var user = await api.Users.GetAuthenticatedUser();
        }

        [TestMethod]
        async public Task Get57726()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var user = await api.Users.Get(57726);
            Assert.AreEqual("https://secure.gravatar.com/avatar/e9fbbfd2de96fdb0cec592a4b6792f0e?d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-user-420.png", user.AvatarUrl);
            Assert.AreEqual(null, user.Bio);
            Assert.AreEqual("http://myxls.in2bits.org", user.Blog);
            Assert.AreEqual("", user.Company);
            Assert.AreEqual(new DateTime(2009,2,25,3,57,50, DateTimeKind.Utc), user.CreatedAt);
            Assert.AreEqual("tim@in2bits.org", user.Email);
            Assert.AreEqual("https://api.github.com/users/timerickson/events{/privacy}", user.EventsUrl);
            Assert.AreEqual(4, user.Followers);
            Assert.AreEqual("https://api.github.com/users/timerickson/followers", user.FollowersUrl);
            Assert.AreEqual(3, user.Following);
            Assert.AreEqual("https://api.github.com/users/timerickson/following{/other_user}", user.FollowingUrl);
            Assert.AreEqual("https://api.github.com/users/timerickson/gists{/gist_id}", user.GistsUrl);
            Assert.AreEqual("e9fbbfd2de96fdb0cec592a4b6792f0e", user.GravatarId);
            Assert.AreEqual(false, user.Hireable);
            Assert.AreEqual("https://github.com/timerickson", user.HtmlUrl);
            //Assert.AreEqual(57726, user.Id);
            Assert.AreEqual("Seattle, WA", user.Location);
            Assert.AreEqual("timerickson", user.Login);
            Assert.AreEqual("Tim Erickson", user.Name);
            Assert.AreEqual("https://api.github.com/users/timerickson/orgs", user.OrganizationsUrl);
            Assert.IsTrue(1 <= user.PublicGists);
            Assert.AreEqual(4, user.PublicRepos);
            Assert.AreEqual("https://api.github.com/users/timerickson/received_events", user.ReceivedEventsUrl);
            Assert.AreEqual("https://api.github.com/users/timerickson/repos", user.ReposUrl);
            Assert.AreEqual("https://api.github.com/users/timerickson/starred{/owner}{/repo}", user.StarredUrl);
            Assert.AreEqual("https://api.github.com/users/timerickson/subscriptions", user.SubscriptionsUrl);
            Assert.AreEqual("User", user.Type);
            Assert.IsTrue(new DateTime(2013,5,27,7,27,32,DateTimeKind.Utc) <= user.UpdatedAt);
            Assert.AreEqual("https://api.github.com/users/timerickson", user.Url);
        }
    }
}
