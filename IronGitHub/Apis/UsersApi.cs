using System.Threading.Tasks;
using IronGitHub.Entities;

namespace IronGitHub.Apis
{
    public class UsersApi : GitHubApi
    {
        public UsersApi(GitHubApiContext context) 
            : base(context)
        {
        }

        async public Task<User> GetCurrent()
        {
            var request = CreateRequest("/user");
            var response = await request.Complete<User>();
            return response.Result;
        }
    }
}