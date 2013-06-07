using System.Collections.Generic;
using System.Threading.Tasks;
using IronGitHub.Entities;

namespace IronGitHub.Apis
{
    public class RepositoriesApi : GitHubApiBase
    {
        public RepositoriesApi(GitHubApiContext context) : base(context)
        {
        }

        async public Task<Repository> Get(string owner, string repo)
        {
            var request = CreateRequest("/repos/" + owner + "/" + repo);

            var response = await Complete<Repository>(request);

            return response.Result;
        }

        async public Task<IEnumerable<Repository>> List(uint sinceId = 0)
        {
            var path = "/repositories";
            if (sinceId != 0)
                path += "?since=" + sinceId;
            
            var request = CreateRequest(path);

            var response = await Complete<IEnumerable<Repository>>(request);

            return response.Result;
        }
    }
}
