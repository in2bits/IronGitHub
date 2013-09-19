using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IronGitHub;
using IronGitHub.Entities;
using IronGitHub.Exceptions;

using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public class HookTests : WithGitHubApi
    {
        private const string _testUsername = "in2bitstest";
        private const string _testRepo = "IronGitHub";

        private Dictionary<string, string> _config;

        private SupportedEvents[] _events;

        private string _postUrl;

        private string PostUrl
        {
            get
            {
                // This used to leverage Requestb.in, but they got mad at us for hitting their API too much.
                // We don't actually hit the Hooks that are created in these tests so using Google seems ok.

                //If we've made one for this test run let's use that instead of making a new one
                if (string.IsNullOrEmpty(this._postUrl)) this._postUrl = "http://www.google.com";

                return this._postUrl;
            }
        }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _config = new Dictionary<string, string>() { { "url", this.PostUrl }, { "content-type", "json" } };
            _events = new[] { SupportedEvents.Push };
        }

        [Test]
        [Category("Authenticated")]
        async public Task GetHooks()
        {
            //await this.Setup();
            await Authorize(new[] { Scopes.Repo });
            await Api.Hooks.Create(_testUsername, _testRepo, new HookBase()
            {
                Name = HookName.Web,
                IsActive = true,
                Events = _events,
                Config = _config
            });

            // Get that hook and make sure it's right.
            var hooksPreDelete = await Api.Hooks.Get(_testUsername, _testRepo);
            hooksPreDelete.Count().Should().Be(1);

            var hook = hooksPreDelete.FirstOrDefault();
            hook.Config.ShouldBeEquivalentTo(_config);
            hook.Events.ShouldBeEquivalentTo(_events);
            hook.Name.Should().Be(HookName.Web);
            hook.IsActive.Should().BeTrue();
            hook.Url.Should().Be("https://api.github.com/repos/in2bitstest/IronGitHub/hooks/" + hook.Id);

            //await this.Api.Hooks.Delete(_testUsername, _testRepo, hook.Id);
            await this.ClearHooks();
        }

        [Test]
        [Category("Authenticated")]
        async public Task GetSingleHook()
        {
            //await this.Setup();
            await Authorize(new[] { Scopes.Repo });

            // Create a hook so we know there is one.
            var tempHook = await Api.Hooks.Create(_testUsername, _testRepo, new HookBase()
                            {
                                Name = HookName.Twilio,
                                IsActive = true,
                                Events = _events,
                                Config = _config
                            });

            var hook = await Api.Hooks.GetById(_testUsername, _testRepo, tempHook.Id);
            hook.Config.ShouldBeEquivalentTo(_config);
            hook.Events.ShouldBeEquivalentTo(_events);
            hook.Name.Should().Be(HookName.Twilio);
            hook.IsActive.Should().BeTrue();
            hook.Url.Should().Be("https://api.github.com/repos/in2bitstest/IronGitHub/hooks/" + tempHook.Id);

            //await this.Api.Hooks.Delete(_testUsername, _testRepo, hook.Id);
            await this.ClearHooks();
        }

        [Test]
        [Category("Authenticated")]
        async public Task CreateWebHook()
        {
            await Authorize(new[] { Scopes.Repo });

            var tempHook = await Api.Hooks.Create(_testUsername, _testRepo, new HookBase()
            {
                Name = HookName.Twitter,
                IsActive = true,
                Events = _events,
                Config = _config
            });

            var hook = await Api.Hooks.GetById(_testUsername, _testRepo, tempHook.Id);
            hook.Config.ShouldAllBeEquivalentTo(_config);
            hook.Events.ShouldAllBeEquivalentTo(_events);
            hook.Id.Should().Be(tempHook.Id);
            hook.Name.Should().Be(HookName.Twitter);
            hook.IsActive.Should().BeTrue();
            hook.Url.Should().Be("https://api.github.com/repos/in2bitstest/IronGitHub/hooks/" + tempHook.Id);

            //await this.Api.Hooks.Delete(_testUsername, _testRepo, hook.Id);
            await this.ClearHooks();
        }

        [Test]
        [Category("Authenticated")]
        async public Task EditWebHook()
        {
            await Authorize(new[] { Scopes.Repo });

            const string newUrl = "http://www.yahoo.com";
            var newConfig = new Dictionary<string, string>() { { "url", newUrl }, { "content-type", "json" } };

            // Create your hook
            var tempHook = await Api.Hooks.Create(_testUsername, _testRepo, new HookBase()
                            {
                                Name = HookName.Trello,
                                IsActive = true,
                                Events = _events,
                                Config = _config
                            });

            var hook = await Api.Hooks.Edit(_testUsername, _testRepo, tempHook.Id,
                                            new Hook.PatchHook()
                                            {
                                                IsActive = true,
                                                AddEvents = new[] { SupportedEvents.PullRequest },
                                                Config = newConfig,
                                            });

            hook.Id.Should().Be(tempHook.Id);
            hook.IsActive.Should().BeTrue();
            hook.Name.Should().Be(HookName.Trello);
            hook.Events.ShouldBeEquivalentTo(new[] { SupportedEvents.Push, SupportedEvents.PullRequest });
            hook.Config.ShouldAllBeEquivalentTo(newConfig);

            //TODO: Figure out why GitHub isn't updating the UpdatedAt field post-update
            //editedHook.UpdatedAt.Should().BeAfter(tempHook.UpdatedAt);

            //await this.Api.Hooks.Delete(_testUsername, _testRepo, hook.Id);
            await this.ClearHooks();
        }

        [Test]
        [Category("Authenticated")]
        [ExpectedException(typeof(NotFoundException))]
        async public Task DeleteWebHook()
        {
            await Authorize(new[] { Scopes.Repo });

            // Create your hook
            var tempHook = await Api.Hooks.Create(_testUsername, _testRepo, new HookBase()
                            {
                                Name = HookName.Toggl,
                                IsActive = true,
                                Events = _events,
                                Config = _config
                            });

            // Clean up your mess
            //await Api.Hooks.Delete(_testUsername, _testRepo, tempHook.Id);
            await this.ClearHooks();
            await Api.Hooks.GetById(_testUsername, _testRepo, tempHook.Id);
        }

        async private Task ClearHooks()
        {
            // Remove any leftover hooks out there
            var hooksPreDelete = await Api.Hooks.Get(_testUsername, _testRepo);

            foreach (var hook in hooksPreDelete)
            {
                await this.Api.Hooks.Delete(_testUsername, _testRepo, hook.Id);
            }
        }
    }
}
