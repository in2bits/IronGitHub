using System.Collections.Generic;
using System.Threading.Tasks;
using IronGitHub.Entities;

namespace IronGitHub.Apis
{
    public class RepositoriesApi : GitHubApi
    {
        public RepositoriesApi(GitHubApiContext context) : base(context)
        {
        }

        async public Task<Repository> Get(int id)
        {
            var request = CreateRequest("/repositories/" + id);

            var response = await request.Complete<Repository>();

            return response.Result;
        }

        async public Task<IEnumerable<Repository>> List(uint sinceId = 0)
        {
            var path = "/repositories";
            if (sinceId != 0)
                path += "?since=" + sinceId;
            
            var request = CreateRequest(path);

            var response = await request.Complete<IEnumerable<Repository>>();

            return response.Result;
        }
    }
}
