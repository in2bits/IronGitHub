using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using IronGitHub;
using IronGitHub.Entities;
using IronGitHub.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests
{
    public class HookTests : WithGitHubApi
    {
        private const string _testUsername = "in2bitstest";
        private const string _testRepo = "IronGitHub";

        private Hook _tempHook;

        private string _requestBinUrl;

        private string RequestBinUrl
        {
            get
            {
                //If we've made one for this test run let's use that instead of making a new one
                return this._requestBinUrl ?? this.BuildNewRequestBinUrl();
            }
        }

        /// <summary>
        /// We need to ensure that a hook exists for our tests so we create one. We don't create
        /// a "Web" hook because that's the type of hook we want to run our tests against and only
        /// one hook of a given type can exist for a user+repo combination.
        /// </summary>
        /// <returns></returns>
        [SetUp]
        async public void Setup()
        {
            //Step through this and copy the value of RequestBinUrl if you want to visit
            //your Requestb.in.  Remember to put ?inspect at the end of the URL to view in a browser
            Assert.IsFalse(string.IsNullOrEmpty(this.RequestBinUrl));

            //this.Api = GitHubApi.Create();

            //await Authorize(new[] { Scopes.Repo });

            //this._tempHook = await this.Api.Hooks.Create(_testUsername, _testRepo, new HookBase()
            //                                    {
            //                                        Name = HookName.Weblate,
            //                                        IsActive = true,
            //                                        Events = new[] { SupportedEvents.Push },
            //                                        Config = new Dictionary<string, string>()
            //                                                 {
            //                                                     {"url", this.RequestBinUrl}
            //                                                 }
            //                                    });
        }

        /// <summary>
        /// We don't want to leave the Setup Hook laying around, so let's clean it up
        /// </summary>
        /// <returns></returns>
        [TearDown]
        async public void TearDown()
        {
            //await this.Api.Hooks.Delete(_testUsername, _testRepo, this._tempHook.Id);
        }

        [Test]
        async public Task GetHooks()
        {
            await Authorize(new[] { Scopes.Repo });
            var hooks = await Api.Hooks.Get(_testUsername, _testRepo);
            hooks.Count().Should().Be(1);
        }

        [Test]
        async public Task GetSingleHook()
        {
            var hook = await this.Api.Hooks.GetById(_testUsername, _testRepo, this._tempHook.Id);

            var expectedConfig = new Dictionary<string, string>() { { "url", this.RequestBinUrl } };

            Assert.AreEqual(HookName.Weblate, hook.Name);
            Assert.IsTrue(hook.IsActive);
            Assert.AreEqual(new[] { SupportedEvents.Push }, hook.Events);
            Assert.AreEqual(expectedConfig, hook.Config);
        }

        [Test]
        async public Task CreateWebHook()
        {
            var now = DateTime.Now;
            var hook = await this.Api.Hooks.Create(_testUsername,
                                              _testRepo,
                                              new HookBase()
                                                {
                                                    Name = HookName.Web,
                                                    IsActive = true,
                                                    Events = new[] { SupportedEvents.Push },
                                                    Config = new Dictionary<string, string>()
                                                                {
                                                                    {"url", this.RequestBinUrl},
                                                                    {"content-type", "json"}
                                                                }
                                                });
            Assert.IsTrue(hook.Id > 0);
            Assert.IsTrue(hook.CreatedAt > now);
        }

        [Test]
        async public Task EditWebHook()
        {
            var newRequestBinUrl = this.BuildNewRequestBinUrl();

            var expectedConfig = new Dictionary<string, string>() { { "url", newRequestBinUrl }, { "content-type", "json" } };

            var existingHook = (await this.Api.Hooks.Get(_testUsername, _testRepo)).First(s => s.Name == HookName.Web);

            var editedHook = await this.Api.Hooks.Edit(_testUsername,
                                            _testRepo,
                                            existingHook.Id,
                                            new Hook.PatchHook()
                                            {
                                                AddEvents = new[] { SupportedEvents.PullRequest },
                                                Config = expectedConfig
                                            });

            Assert.AreEqual(existingHook.Id, editedHook.Id);
            Assert.AreEqual(existingHook.IsActive, editedHook.IsActive);
            Assert.AreEqual(HookName.Web, editedHook.Name);
            Assert.IsTrue(editedHook.IsActive);
            Assert.AreEqual(new[] { SupportedEvents.Push, SupportedEvents.PullRequest }, editedHook.Events);
            Assert.AreEqual(expectedConfig, editedHook.Config);
            Assert.IsTrue(editedHook.UpdatedAt > existingHook.UpdatedAt);
        }

        [Test]
        [ExpectedException(typeof(GitHubErrorException))]
        async public Task DeleteWebHook()
        {
            var existingHook = (await this.Api.Hooks.Get(_testUsername, _testRepo)).First(s => s.Name == HookName.Web);

            await this.Api.Hooks.Delete(_testUsername, _testRepo, existingHook.Id);

            await this.Api.Hooks.GetById(_testUsername, _testRepo, existingHook.Id);
        }

        private string BuildNewRequestBinUrl()
        {
            const string uriString = "http://requestb.in/";

            var uri = new Uri(uriString + "api/v1/bins");
            var request = WebRequest.CreateHttp(uri);
            request.Method = "POST";
            var requestStream = request.GetRequestStream();
            requestStream.WriteAsJson("{'private': false}");
            var response = (HttpWebResponse)request.GetResponse();
            var payload = response.Deserialize<RequestBinResponse>();
            var output = uriString + payload.Name;

            Console.WriteLine(output);
            return output;
        }
    }

    class RequestBinResponse
    {
        public string Name { get; set; }
        public int RequestCount { get; set; }
        public bool Private { get; set; }
    }
}
