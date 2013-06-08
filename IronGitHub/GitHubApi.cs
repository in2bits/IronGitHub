using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IronGitHub.Apis;
using Authorization = IronGitHub.Entities.Authorization;

namespace IronGitHub
{
    public class GitHubApi : GitHubApiBase
    {
        public GitHubApi(GitHubApiContext context) : base(context)
        {
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

        private RepositoriesApi _repositoriesApi;
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

            var response = await PostAsJson<Authorization.AuthorizeRequest, Authorization>(request, authRequest);

            Context.Authorize(response.Result);
            return Context.Authorization;
        }
    }
}