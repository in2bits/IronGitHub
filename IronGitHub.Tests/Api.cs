using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace IronGitHub.Tests
{
    [TestClass]
    public class Api
    {
        [TestMethod]
        public void Should_Construct_GitHub_Api()
        {
            var api = GitHubApi.Create();
            api.Should().BeOfType<GitHubApi>();
        }

        [TestMethod]
        public void Should_Construct_GitHub_Api_With_Context()
        {
            var api = new GitHubApi();
            api.Context.Should().BeOfType<GitHubApiContext>();
        }

        [TestMethod]
        public void Should_Construct_GitHub_Api_With_Custom_Context()
        {
            var context = new GitHubApiContext();
            var api = new GitHubApi(context);
            api.Context.Should().BeSameAs(context);
        }
    }
}
