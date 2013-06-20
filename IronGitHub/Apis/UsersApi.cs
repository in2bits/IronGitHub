using System.Threading.Tasks;
using IronGitHub.Entities;

namespace IronGitHub.Apis
{
    /// <summary>
    /// Users API
    /// 
    /// Many of the resources on the users API provide a shortcut for getting 
    /// information about the currently authenticated user. If a request URL 
    /// does not include a :user parameter then the response will be for the 
    /// logged in user (and you must pass authentication information with your 
    /// request).
    /// 
    /// http://developer.github.com/v3/users/
    /// </summary>
    public class UsersApi : GitHubApiBase
    {
        public UsersApi(GitHubApiContext context) 
            : base(context)
        {
        }

        /// <summary>
        /// Get the authenticated user
        /// 
        /// /user
        /// </summary>
        /// <returns>The authenticated User</returns>
        async public Task<User> GetAuthenticatedUser()
        {
            var request = CreateRequest("/user");
            var response = await Complete<User>(request);
            return response.Result;
        }

        /// <summary>
        /// Get a single user
        /// 
        /// /users/:user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async public Task<User> Get(int id)
        {
            var request = CreateRequest("/user/" + id);
            var response = await Complete<User>(request);
            return response.Result;
        }
    }
}