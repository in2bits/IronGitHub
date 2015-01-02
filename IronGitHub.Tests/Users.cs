using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronGitHub.Tests.Helpers;
using FluentAssertions;
using IronGitHub.Exceptions;
using TestConfiguration = IronGitHub.Tests.Helpers.TestConfiguration;

namespace IronGitHub.Tests
{
    [TestClass]
    public class Users : AuthenticationBoilerPlate
    {
        [TestMethod]
        public void Should_Setup_GitHub_Api()
        {
            this.CreateGitHubApi();
            this.Api.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Get_User()
        {
            Should_Setup_GitHub_Api();

            var user = Api.Users.Get("5274545").GetAwaiter().GetResult();

            user.Should().NotBeNull();
            user.Login.Should().Be("apitestaccount");
            user.Id.Should().Be("5274545");
            user.Type.Should().Be("User");
            user.Url.Should().Be("https://api.github.com/users/apitestaccount");
            user.HtmlUrl.Should().Be("https://github.com/apitestaccount");
        }

        [TestMethod]
        public void Should_Get_Current_User()
        {
            Should_Setup_GitHub_Api();
            this.Authorize();

            var user = Api.Users.GetAuthenticatedUser().GetAwaiter().GetResult();

            user.Should().NotBeNull();
            user.Login.Should().Be(TestConfiguration.GitHubUsername);
            user.Type.Should().Be("User");
            user.Url.Should().Contain(TestConfiguration.GitHubUsername);
            user.HtmlUrl.Should().Contain(TestConfiguration.GitHubUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetInvalidUser()
        {
            Should_Setup_GitHub_Api();
            Api.Users.Get("NO_SUCH_USER").GetAwaiter().GetResult();
        }
    }
}
