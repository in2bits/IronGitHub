using IronGitHub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub.Apis
{
    /// <summary>
    /// Organizations API
    /// 
    /// http://developer.github.com/v3/orgs/
    /// </summary>
    public class OrganizationsApi : GitHubApiBase
    {
        public OrganizationsApi(GitHubApiContext context) 
            : base(context)
        {
        }

        /// <summary>
        /// Get a single Organization
        /// 
        /// /orgs/:org
        /// </summary>
        /// <returns>The authenticated User</returns>
        async public Task<Organization> GetOrganization(string organization)
        {
            var request = CreateRequest(string.Format("/orgs/{0}", organization));
            var response = await Complete<Organization>(request);
            return response.Result;
        }

        /// <summary>
        /// Gets the repositories belonging to an Organization
        /// 
        /// /orgs/:org/repos
        /// </summary>
        /// <returns>The repositories belonging to an Organization</returns>
        async public Task<IEnumerable<Repository>> GetRepositories(string organization, RepositoryTypes? type = null)
        {
            var request = CreateRequest(string.Format("/orgs/{0}/repos", organization),
                new Dictionary<string, string>
                    {
                        {"type", type.ToParameterValue()}
                    });
            var response = await Complete<IEnumerable<Repository>>(request);
            return response.Result;
        }
    }
}
