using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IronGitHub.Entities;

namespace IronGitHub.Apis
{
    public class SearchApi : GitHubApi
    {
        public SearchApi(GitHubApiContext context) : base(context)
        {
        }

        async public Task<User.UserList> Users(string matching)
        {
            var request = CreateRequest("/legacy/user/search/:" + Uri.EscapeDataString(matching));

            var response = await request.Complete<User.UserList>();

            return response.Result;
        }
    }
}