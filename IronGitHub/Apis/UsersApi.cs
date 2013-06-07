using System.Threading.Tasks;
using IronGitHub.Entities;

namespace IronGitHub.Apis
{
    public class UsersApi : GitHubApiBase
    {
        public UsersApi(GitHubApiContext context) 
            : base(context)
        {
        }

        async public Task<User> GetCurrent()
        {
            var request = CreateRequest("/user");
            var response = await Complete<User>(request);
            return response.Result;
        }

        async public Task<User> Get(int id)
        {
            var request = CreateRequest("/user/" + id);
            var response = await Complete<User>(request);
            return response.Result;
        }
    }
}