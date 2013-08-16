using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IronGitHub;
using IronGitHub.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.ApiTests
{
    using System.Linq;

    [TestClass]
    public class HookTests
    {
        private const string _testUsername  = "in2bitstest";
        private const string _testRepo      = "IronGitHub";

        private Hook _tempHook;

        private string _requestBinUrl;
        private string RequestBinUrl
        {
            get
            {
                //If we've made one for this test run let's use that instead of making a new one
                return _requestBinUrl ?? BuildNewRequestBinUrl().Result;
            }
        }

        async private Task<string> BuildNewRequestBinUrl()
        {
            const string uriString = "http://requestb.in/";

            var uri = new Uri(uriString + "api/v1/bins");
            var request = WebRequest.CreateHttp(uri);
            request.Method = "POST";
            var requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false);
            requestStream.WriteAsJson("{'private': false}");
            var response = (HttpWebResponse)await request.GetResponseAsync();
            var payload = response.Deserialize<RequestBinResponse>();
            return uriString + payload.Name;
        }

        /// <summary>
        /// We need to ensure that a hook exists for our tests so we create one. We don't create
        /// a "Web" hook because that's the type of hook we want to run our tests against and only
        /// one hook of a given type can exist for a user+repo combination.
        /// </summary>
        /// <returns></returns>
        [TestInitialize]
        async public void Setup()
        {
            //Step through this and copy the value of RequestBinUrl if you want to visit
            //your Requestb.in.  Remember to put ?inspect at the end of the URL to view in a browser
            Assert.IsFalse(string.IsNullOrEmpty(RequestBinUrl));

            var api = GitHubApi.Create();
            await api.in2bitstest();
            _tempHook = await api.Hooks.Create(_testUsername, _testRepo, new HookBase()
                                                {
                                                    Name = HookName.Weblate,
                                                    IsActive = true,
                                                    Events = new[] { SupportedEvents.Push },
                                                    Config = new Dictionary<string, string>()
                                                             {
                                                                 {"url", RequestBinUrl},
                                                                 {"content_type", "json"}
                                                             }
                                                });
        }

        /// <summary>
        /// We don't want to leave the Setup Hook laying around, so let's clean it up
        /// </summary>
        /// <returns></returns>
        [TestCleanup]
        async public void TearDown()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            await api.Hooks.Delete(_testUsername, _testRepo, _tempHook.Id);
        }


        [TestMethod]
        async public Task GetHooks()
        {
            var api = GitHubApi.Create();
            await api.in2bitstest();
            var hooks = await api.Hooks.Get(_testUsername, _testRepo);
            Assert.Equals(1, hooks.Count());
        }
    }

    class RequestBinResponse
    {
        public string Name { get; set; }
        public int RequestCount { get; set; }
        public bool Private { get; set; }
    }
}
