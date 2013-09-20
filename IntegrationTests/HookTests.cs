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
    using System.Threading;

    [TestFixture]
    public class HookTests : WithGitHubApi
    {
        private object _lockObject = new object();
        private static readonly string _testUsername = IntegrationTestParameters.GitHubUsername;
        private const string _testRepo = "IronGitHub";

        private Dictionary<string, string> _config;
        private SupportedEvents[] _events;

        private Hook _tempHook;

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

            Api = GitHubApi.Create();

            Authorize(new[] { Scopes.Repo })
                .ContinueWith(t =>
                    this.ClearHooks())
                .ContinueWith(
                    t =>
                    {
                        Task.Delay(10000);
                        _tempHook =
                            Api.Hooks.Create(
                                _testUsername,
                                _testRepo,
                                new HookBase()
                                {
                                    Name = HookName.Web,
                                    IsActive = true,
                                    Events = _events,
                                    Config = _config
                                }).Result;
                    }).Wait();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            this.ClearHooks().Wait();
        }

        [Test]
        [Category("Authenticated")]
        public void GetHooks()
        {
            // Get that hook and make sure it's right.
            var hooksPreDelete = Api.Hooks.Get(_testUsername, _testRepo).Result;
            hooksPreDelete.Count().Should().Be(1);

            var hook = hooksPreDelete.FirstOrDefault();
            hook.Config.ShouldBeEquivalentTo(_config);
            hook.Events.ShouldBeEquivalentTo(_events);
            hook.Name.Should().Be(HookName.Web);
            hook.IsActive.Should().BeTrue();
            hook.Url.Should().Be("https://api.github.com/repos/in2bitstest/IronGitHub/hooks/" + hook.Id);
        }

        [Test]
        [Category("Authenticated")]
        public void GetSingleHook()
        {
            var hook = Api.Hooks.GetById(_testUsername, _testRepo, _tempHook.Id).Result;
            hook.Config.ShouldBeEquivalentTo(_config);
            hook.Events.ShouldBeEquivalentTo(_events);
            hook.Name.Should().Be(HookName.Web);
            hook.IsActive.Should().BeTrue();
            hook.Url.Should().Be("https://api.github.com/repos/in2bitstest/IronGitHub/hooks/" + _tempHook.Id);
        }

        [Test]
        [Category("Authenticated")]
        public void CreateWebHook()
        {
            var hook = Api.Hooks.GetById(_testUsername, _testRepo, _tempHook.Id).Result;
            hook.Config.ShouldAllBeEquivalentTo(_config);
            hook.Events.ShouldAllBeEquivalentTo(_events);
            hook.Id.Should().Be(_tempHook.Id);
            hook.Name.Should().Be(HookName.Web);
            hook.IsActive.Should().BeTrue();
            hook.Url.Should().Be("https://api.github.com/repos/in2bitstest/IronGitHub/hooks/" + _tempHook.Id);
        }

        [Test]
        [Category("Authenticated")]
        public void EditWebHook()
        {
            const string newUrl = "http://www.yahoo.com";
            var newConfig = new Dictionary<string, string>() { { "url", newUrl }, { "content-type", "json" } };

            Hook editedHook = null;
            Api.Hooks
                .Edit(_testUsername,
                        _testRepo,
                        _tempHook.Id,
                        new Hook.PatchHook()
                        {
                            IsActive = true,
                            AddEvents = new[] { SupportedEvents.PullRequest },
                            Config = newConfig,
                        })
                .ContinueWith(t =>
                {
                    editedHook = Api.Hooks.GetById(_testUsername, _testRepo, t.Result.Id).Result;
                })
                .Wait();
            
            editedHook.Id.Should().Be(_tempHook.Id);
            editedHook.IsActive.Should().BeTrue();
            editedHook.Name.Should().Be(HookName.Web);
            editedHook.Events.ShouldBeEquivalentTo(
                new[] { SupportedEvents.Push, SupportedEvents.PullRequest });
            editedHook.Config.ShouldAllBeEquivalentTo(newConfig);
            
            //TODO: Figure out why GitHub isn't updating the UpdatedAt field post-update
            //editedHook.UpdatedAt.Should().BeAfter(_tempHook.UpdatedAt);

            // We need to tell the shared state that the config has changed
            _config = newConfig;
            _events = new[] { SupportedEvents.Push, SupportedEvents.PullRequest };
        }

        // We test the delete functionality in TestFixtureSetUp and TestFixtureTearDown.  However
        // this test causes the whole suite to hang no matter how it is written.
        //[Test]
        //[Category("Authenticated")]
        //[ExpectedException(typeof(NotFoundException))]
        //public void DeleteWebHook()
        //{
        //    // Create your hook
        //    Api.Hooks
        //            .Create(_testUsername, _testRepo, new HookBase()
        //                    {
        //                        Name = HookName.Toggl,
        //                        IsActive = true,
        //                        Events = _events,
        //                        Config = _config
        //                    })
        //            .ContinueWith(t => Api.Hooks.Delete(_testUsername, _testRepo, t.Result.Id))
        //            .ContinueWith(t => Api.Hooks.GetById(_testUsername, _testRepo, t.Result.Id))
        //            .Wait();

        //    // Clean up your mess
        //    //await Api.Hooks.Delete(_testUsername, _testRepo, tempHook.Id);
        //    //await Api.Hooks.GetById(_testUsername, _testRepo, tempHook.Id);
        //}

        async private Task ClearHooks()
        {
            var hooksPreDelete = Api.Hooks.Get(_testUsername, _testRepo).Result;

            foreach (var hook in hooksPreDelete)
            {
                Api.Hooks.Delete(_testUsername, _testRepo, hook.Id).Wait();
            }
        }
    }
}
