using FluentAssertions;
using IronGitHub.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Authorization = IronGitHub.Entities.Authorization;
using TestConfiguration = IronGitHub.Tests.Helpers.TestConfiguration;

namespace IronGitHub.Tests
{
    [TestClass]
    public class Auth : AuthenticationBoilerPlate
    {
        [TestMethod]
        public void Should_Setup_GitHub_Api()
        {
            this.CreateGitHubApi();
            this.Api.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Require_No_Authentication()
        {
            Should_Setup_GitHub_Api();
            this.Api.Context.Authorization.Should().Be(Authorization.Anonymous);
        }

        [TestMethod]
        public void Should_Authenticate_With_GitHub_Credentials()
        {
            Should_Setup_GitHub_Api();

            this.Authorize();

            var user = this.Api.Users.GetAuthenticatedUser().GetAwaiter().GetResult();

            Api.Context.Authorization.Should().NotBeNull();
            user.Login.Should().Be(TestConfiguration.GitHubUsername);
        }

        [TestMethod]
        public void Should_Authenticate_With_One_Scope()
        {
            Should_Setup_GitHub_Api();

            this.Authorize(new[] { Scopes.Gist });

            Api.Context.Authorization.Scopes.Should()
                .ContainSingle(scope => scope == Scopes.Gist);
        }

        [TestMethod]
        public void Should_Authenticate_With_Two_Scopes()
        {
            Should_Setup_GitHub_Api();

            this.Authorize(new[] { Scopes.Gist, Scopes.UserEmail });

            Api.Context.Authorization.Scopes.Should()
                .OnlyContain(scope => scope == Scopes.Gist || scope == Scopes.UserEmail);
        }
    }
}