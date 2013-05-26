using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub
{
    public class GitHubApi
    {
        public GitHubApi(GitHubApiContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
        }

        public static GitHubApi Create()
        {
            return new GitHubApi(new GitHubApiContext());
        }

        private MembersApi _membersApi = null;
        public MembersApi Members
        {
            get
            {
                if (_membersApi == null)
                    _membersApi = new MembersApi(Context);
                return _membersApi;
            }
        }

        public GitHubApiContext Context { get; private set; }

        protected HttpWebRequest CreateRequest(string path)
        {
            var uri = new Uri("https://" + Context.Configuration.Domain + path);
            var request = WebRequest.CreateHttp(uri);
            request.UserAgent = Context.Configuration.UserAgent;
            return request;
        }

        async public Task<Authorization> AuthorizeApplication(NetworkCredential credential, Application application, IEnumerable<Scopes> scopes, string note = null)
        {
            var authRequest = new Authorization.AuthorizeAppRequestPost
            {
                ClientId = application.ClientId,
                ClientSecret = application.ClientSecret,
                Scopes = scopes
            };
            if (note != null)
                authRequest.Note = note;

            return await Authorize(credential, authRequest);
        }

        async public Task<Authorization> Authorize(NetworkCredential credential, IEnumerable<Scopes> scopes, string note = null)
        {
            var authRequest = new Authorization.AuthorizeRequest
            {
                Scopes = scopes
            };
            if (note != null)
                authRequest.Note = note;

            return await Authorize(credential, authRequest);
        }

        async private Task<Authorization> Authorize(NetworkCredential credential, Authorization.AuthorizeRequest authRequest)
        {
            var request = CreateRequest("/authorizations");
            request.AddAuthorizationCredential(credential);

            await request.PostAsJson(authRequest);

            var auth = await request.Complete<Authorization>();
            Context.Authorization = auth;
            return auth;
        }
    }
}