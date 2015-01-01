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

        private static Uri PostUrl = new Uri("https://www.google.com/");

        #endregion Test Variables

        [TestMethod]
        public void Should_Setup_GitHub_Api()
        {
            this.CreateGitHubApi();
            this.Api.Should().NotBeNull();
            
            SetupHooks();

            Events.Should().NotBeNull();
            WebHook.Should().NotBeNull();
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
