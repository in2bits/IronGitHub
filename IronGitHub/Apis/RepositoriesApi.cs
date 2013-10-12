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

        /// <summary>
        /// Get a Repository
        /// </summary>
        /// <param name="owner">The owner of the Repository to get</param>
        /// <param name="repo">The Repository to get</param>
        /// <returns>The Repository.  The parent and source objects are present 
        /// when the repo is a fork. parent is the repo this repo was forked 
        /// from, source is the ultimate source for the network.</returns>
        async public Task<Repository> Get(string owner, string repo)
        {
            var request = CreateRequest("/repos/" + owner + "/" + repo);

            var response = await Complete<Repository>(request);

            return response.Result;
        }

        /// <summary>
        /// List all Repositories
        /// </summary>
        /// <param name="since">The integer ID of the last Repository that 
        /// you’ve seen.</param>
        /// <returns>This provides a dump of every repository, in the order 
        /// that they were created.</returns>
        async public Task<IEnumerable<Repository>> List(uint since = 0)
        {
            var path = "/repositories";
            if (since != 0)
                path += "?since=" + since;
            
            var request = CreateRequest(path);

            var response = await Complete<IEnumerable<Repository>>(request);

            return response.Result;
        }

        /// <summary>
        /// List Repositories of the Authenticated user
        /// </summary>
        /// <param name="since">The integer ID of the last Repository that 
        /// you’ve seen.</param>
        /// <param name="sort">The sort</param>
        /// <param name="sortDirection">Direction to sort</param>
        /// <returns>This provides a dump of the current users repository</returns>
        async public Task<IEnumerable<Repository>> ListMine(uint since = 0, string sort = "updated", string sortDirection = null)
        {
            var path = "/user/repos?sort=" + sort;
            if (since != 0)
                path += "&since=" + since;
            if (sortDirection != null)
                path += "&direction=" + sortDirection;

            var request = CreateRequest(path);
            var response = await Complete<IEnumerable<Repository>>(request);
            return response.Result;
        }
    }
}
