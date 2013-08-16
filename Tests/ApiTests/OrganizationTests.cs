using IronGitHub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class OrganizationTests
    {
        [TestMethod]
        async public Task GetOrganization()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest(new[] { Scopes.Repo });
            var organization = await api.Organizations.GetOrganization("in2bits");
            Assert.AreEqual("in2bits.org", organization.Name);
            Assert.AreEqual(null, organization.Company);
            Assert.AreEqual(null, organization.Blog);
            Assert.AreEqual(null, organization.Location);
            Assert.AreEqual("tim@in2bits.org", organization.Email);
            Assert.AreEqual(5, organization.PublicRepositoryCount);
            Assert.AreEqual(0, organization.PublicGistCount);
            Assert.AreEqual(0, organization.FollowersCount);
            Assert.AreEqual(0, organization.FollowingCount);
            Assert.AreEqual(new DateTime(2013, 5, 26, 0, 11, 14, DateTimeKind.Utc), organization.CreatedAt);
            Assert.AreEqual("Organization", organization.Type);
            Assert.AreEqual("4529897", organization.Id);
            Assert.AreEqual("https://secure.gravatar.com/avatar/d41d8cd98f00b204e9800998ecf8427e?d=https://a248.e.akamai.net/assets.github.com%2Fimages%2Fgravatars%2Fgravatar-org-420.png", organization.AvatarUrl);
        }
    }
}
