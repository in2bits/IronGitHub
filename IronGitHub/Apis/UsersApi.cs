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
            var user = await request.Complete<User>();
            return user;
        }
    }
}