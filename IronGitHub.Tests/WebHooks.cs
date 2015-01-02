using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronGitHub.Tests.Helpers;
using FluentAssertions;
using TestConfiguration = IronGitHub.Tests.Helpers.TestConfiguration;
using IronGitHub.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IronGitHub.Tests
{
    [TestClass]
    public class WebHooks : AuthenticationBoilerPlate
    {

        # region Test Variables

        private static string Username = TestConfiguration.GitHubUsername;

        private static string Repository = TestConfiguration.GitHubRepository;

        private static Hook WebHook { get; set; }

        private static Dictionary<string, string> BaseConfiguration { get; set; }

        private static SupportedEvents[] Events { get; set; }

        private static Uri PostUrl = new Uri(string.Format("https://api.github.com/repos/{0}/{1}", Username, Repository));

        #endregion Test Variables

        [TestMethod]
        public void Should_Setup_GitHub_Api()
        {
            this.CreateGitHubApi();
            this.Api.Should().NotBeNull();

            Username.Should().NotBeNullOrEmpty();
            Repository.Should().NotBeNullOrEmpty();

            SetupHooks();

            Events.Should().NotBeNull();
            WebHook.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Get_WebHooks()
        {

            Should_Setup_GitHub_Api();

            // Get that hook and make sure it's right.
            var hooksPreDelete = Api.Hooks.Get(Username, Repository)
                .GetAwaiter()
                .GetResult();
            
            hooksPreDelete.Count().Should().Be(1);

            var hook = hooksPreDelete.FirstOrDefault();

            hook.Config.ShouldBeEquivalentTo(BaseConfiguration);
            hook.Events.ShouldBeEquivalentTo(Events);
            hook.Name.Should().Be(HookName.Web);
            hook.IsActive.Should().BeTrue();
            hook.Url.Should().Be(string.Format("https://api.github.com/repos/{0}/{1}/hooks/{2}", Username, Repository, hook.Id));
        }


        # region Hook helpers
        private void SetupHooks()
        {
            Should_Setup_GitHub_Api();

            BaseConfiguration = new Dictionary<string, string>() { 
                { "url", PostUrl.ToString() }, 
                { "content-type", "json" } 
            };

            Events = new[] { 
                SupportedEvents.Push 
            };

            this.Authorize(new[] { Scopes.Repo });

            this.ClearHooks();

            WebHook = Api.Hooks.Create(Username, Repository, new HookBase()
            {
                Name = HookName.Web,
                IsActive = true,
                Events = Events,
                Config = BaseConfiguration
            })
            .GetAwaiter()
            .GetResult();

        }

        private void ClearHooks()
        {
            var hooksPreDelete = Api.Hooks.Get(Username, Repository)
                .GetAwaiter()
                .GetResult();

            List<Task> tasks = new List<Task>();

            foreach (var hook in hooksPreDelete)
            {
                tasks.Add(Api.Hooks.Delete(Username, Repository, hook.Id));
            }

            Task.WhenAll(tasks)
                .GetAwaiter()
                .GetResult();

            Thread.Sleep(1000);

        }
        #endregion Hook helpers
    }
}
