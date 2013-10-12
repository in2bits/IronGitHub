using System.Collections.Generic;
using System.Threading.Tasks;
using IronGitHub.Entities;

namespace IronGitHub.Apis
{
    public class IssuesApi : GitHubApiBase
    {
        public IssuesApi(GitHubApiContext context) : base(context)
        {
        }

        /// <summary>
        /// List all issues across all the authenticated user’s visible 
        /// repositories including owned repositories, member repositories, 
        /// and organization repositories.
        /// </summary>
        /// <returns>List of IssueItems</returns>
        async public Task<IEnumerable<Issue.IssueItem>> Get()
        {
            var request = CreateRequest("/issues");

            var response = await Complete<IEnumerable<Issue.IssueItem>>(request);

            return response.Result;
        }

        /// <summary>
        /// Create a new issue
        /// </summary>
        /// <param name="repoFullName">Repository full name, i.e org/repoName</param>
        /// <param name="issueTitle">Title</param>
        /// <param name="issueBody">Body of the issue</param>
        /// <returns></returns>
        async public Task<Issue> New(string repoFullName, string issueTitle, string issueBody)
        {
            var request = CreateRequest(string.Format("/repos/{0}/issues", repoFullName));

            var response = await PostAsJson<NewIssue, Issue>(request, new NewIssue
            {
                Title = issueTitle,
                Body = issueBody
            });

            return response.Result;
        }

        /// <summary>
        /// List all issues across owned and member repositories for the 
        /// authenticated user.
        /// </summary>
        /// <returns>List of IssueItems</returns>
        async public Task<IEnumerable<Issue.IssueItem>> GetForUser()
        {
            var request = CreateRequest("/user/issues");

            var response = await Complete<IEnumerable<Issue.IssueItem>>(request);

            return response.Result;
        } 

        /// <summary>
        /// List all issues for a given organization for the authenticated user.
        /// </summary>
        /// <param name="organization">Name of the organization for which to
        /// retrieve issues for the authenticated user.</param>
        /// <returns>List of IssueItems</returns>
        async public Task<IEnumerable<Issue.IssueItem>> GetForUserOrganization(string organization)
        {
            var request = CreateRequest(string.Format("/orgs/{0}/issues", organization));

            var response = await Complete<IEnumerable<Issue.IssueItem>>(request);

            return response.Result;
        } 

        /// <summary>
        /// Get a single issue
        /// </summary>
        /// <param name="owner">Owner of the repository of the issue</param>
        /// <param name="repo">Repository of the issue</param>
        /// <param name="number">Issue number in the repository</param>
        /// <returns>The Issue</returns>
        async public Task<Issue> Get(string owner, string repo, int number)
        {
            var request = CreateRequest(string.Format("/repos/{0}/{1}/issues/{2}", owner, repo, number));

            var response = await Complete<Issue>(request);

            return response.Result;
        }

        /// <summary>
        /// List all issues for a given repository
        /// </summary>
        /// <param name="owner">Owner of the repository of the issues</param>
        /// <param name="repo">Repository of the issues</param>
        /// <returns>List of IssueItems</returns>
        async public Task<IEnumerable<Issue.IssueItem>> GetForRepository(string owner, string repo)
        {
            var request = CreateRequest(string.Format("/repos/{0}/{1}/issues", owner, repo));

            var response = await Complete<IEnumerable<Issue.IssueItem>>(request);

            return response.Result;
        }
    }
}