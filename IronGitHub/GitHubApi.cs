using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IronGitHub.Apis;
using IronGitHub.Exceptions;
using Authorization = IronGitHub.Entities.Authorization;

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

        private MembersApi _membersApi;
        public MembersApi Members
        {
            get { return _membersApi ?? (_membersApi = new MembersApi(Context)); }
        }

        private RepositoriesApi _repositoriesApi = null;
        public RepositoriesApi Repositories
        {
            get { return _repositoriesApi ?? (_repositoriesApi = new RepositoriesApi(Context)); }
        }

        private UsersApi _usersApi;
        public UsersApi Users
        {
            get { return _usersApi ?? (_usersApi = new UsersApi(Context)); }
        }

        private GistsApi _gistsApi;
        public GistsApi Gists
        {
            get { return _gistsApi ?? (_gistsApi = new GistsApi(Context)); }
        }

        private SearchApi _searchApi;
        public SearchApi Search
        {
            get { return _searchApi ?? (_searchApi = new SearchApi(Context)); }
        }

        public GitHubApiContext Context { get; private set; }

        protected HttpWebRequest CreateRequest(string path)
        {
            var uri = new Uri("https://" + Context.Configuration.Domain + path);
            var request = WebRequest.CreateHttp(uri);
            var auth = Context.Authorization;
            if (auth != null)
            {
                request.Headers["Authorization"] = "token " + auth.Token;
            }
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

            var response = await request.Complete<Authorization>();
            Context.Authorization = response.Result;
            return Context.Authorization;
        }

        public void Authorize(Authorization authorization)
        {
            if (authorization == null)
                throw new ArgumentNullException("authorization");
            Context.Authorization = authorization;
        }
    }
}