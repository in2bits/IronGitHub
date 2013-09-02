using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IronGitHub;
using NUnit.Framework;
using Authorization = IronGitHub.Entities.Authorization;

namespace IntegrationTests
{
    [TestFixture]
    public class AuthTests : WithGitHubApi
    {
        [Test]
        public void AuthorizeDefaultCredential()
        {
            Api.Context.Authorization.Should().Be(Authorization.Anonymous);
        }

        [Test]
        [Category("Authenticated")]
        public async Task AuthorizeWithCredential()
        {
            await Authorize();

            var user = await Api.Users.GetAuthenticatedUser();

            Api.Context.Authorization.Should().NotBeNull();
            user.Login.Should().Be(IntegrationTestParameters.GitHubUsername);
        }

        [Test]
        [Category("Authenticated")]
        public async Task AuthorizeWithOneScope()
        {
            await Authorize(new[] {Scopes.Gist});

            Api.Context.Authorization.Scopes.Should()
                .ContainSingle(scope => scope == Scopes.Gist);
        }

        [Test]
        [Category("Authenticated")]
        public async Task AuthorizeWithTwoScopes()
        {
            await Authorize(new[] {Scopes.Gist, Scopes.UserEmail});

            Api.Context.Authorization.Scopes.Should()
                .OnlyContain(scope => scope == Scopes.Gist || scope == Scopes.UserEmail);
        }
    }
}
