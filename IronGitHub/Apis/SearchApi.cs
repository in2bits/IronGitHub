using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using IronGitHub.Entities;

namespace IronGitHub.Apis
{
    /// <summary>
    /// Search API
    /// This is a listing of the Search API features from API v2 that have 
    /// been ported to API v3. There should be no changes, other than the new 
    /// URL and JSON output format.
    /// http://developer.github.com/v3/search/
    /// </summary>
    public class SearchApi : GitHubApiBase
    {
        public SearchApi(GitHubApiContext context) : base(context)
        {
        }

        /// <summary>
        /// Search users - Find users by keyword.
        /// 
        /// /legacy/user/search/:keyword
        /// </summary>
        /// <param name="keyword">Keyword search parameters</param>
        /// <param name="startPage">Optional Page number to fetch</param>
        /// <param name="sort">Optional Sort field. One of followers, joined, 
        /// or repositories. If not provided, results are sorted by best 
        /// match.</param>
        /// <param name="order">Optional Sort order if sort param is provided. One of asc or desc.</param>
        /// <returns></returns>
        async public Task<User.UserSearchResults> Users(string keyword,
            int? startPage = null,
            UserSorts? sort = null,
            SortOrders? order = null)
        {
            var request = CreateRequest("/legacy/user/search/" + Uri.EscapeDataString(keyword));

            var response = await Complete<User.UserSearchResults>(request);

            return response.Result;
        }

        /// <summary>
        /// Search repositories - Find repositories by keyword. Note, this 
        /// legacy method does not follow the v3 pagination pattern. This 
        /// method returns up to 100 results per page and pages can be fetched 
        /// using the startPage parameter.
        /// 
        /// /legacy/repos/search/:keyword
        /// </summary>
        /// <param name="keyword">Search term</param>
        /// <param name="language">Optional Filter results by language 
        /// (https://github.com/languages)</param>
        /// <param name="startPage">Optional Page number to fetch</param>
        /// <param name="sort">Optional Sort field. One of stars, forks, or 
        /// updated. If not provided, results are sorted by best match.</param>
        /// <param name="order">Optional Sort order if sort param is provided. 
        /// One of asc or desc.</param>
        /// <returns>A Repository.RepositorySearchResults with the matching Repositories.</returns>
        async public Task<Repository.RepositorySearchResults> Repositories(string keyword,
            string language = null,
            int startPage = 0,
            RepositorySorts? sort = null,
            SortOrders? order = null)
        {
            var request = CreateRequest("/legacy/repos/search/" + Uri.EscapeDataString(keyword),
                new Dictionary<string, string>
                    {
                        {"language", language},
                        {"start_page", startPage.ToString(CultureInfo.InvariantCulture)},
                        {"sort", sort.ToParameterValue()},
                        {"order", order.ToParameterValue()}
                    });

            var response = await Complete<Repository.RepositorySearchResults>(request);

            return response.Result;
        }

        /// <summary>
        /// Search issues - Find issues by state and keyword.
        /// 
        /// /legacy/issues/search/:owner/:repository/:state/:keyword
        /// </summary>
        /// <param name="owner">The owner of the repository in which to search for issues.</param>
        /// <param name="repository">The repository in which to search for issues.</param>
        /// <param name="state">open or closed </param>
        /// <param name="keyword">Search term</param>
        /// <returns>An Issue.IssueSearchResults with the matching Issues</returns>
        async public Task<Issue.IssueSearchResults> Issues(string owner, string repository, IssueStates state, string keyword)
        {
            if (string.IsNullOrEmpty(owner))
                throw new ArgumentException("must be populated", "owner");

            if (string.IsNullOrEmpty(repository))
                throw new ArgumentException("must be populated", "repository");

            var request = CreateRequest(string.Format("/legacy/issues/search/{0}/{1}/{2}/{3}", owner, repository, state.ToJsonValue(), Uri.EscapeDataString(keyword)));

            var response = await Complete<Issue.IssueSearchResults>(request);

            return response.Result;
        }

        /// <summary>
        /// Email search - This API call is added for compatibility reasons 
        /// only. There’s no guarantee that full email searches will always be 
        /// available. The @ character in the address must be left unencoded. 
        /// Searches only against public email addresses (as configured on the 
        /// user’s GitHub profile).
        /// 
        /// /legacy/user/email/:email
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>A User with the specified email address</returns>
        async public Task<User.EmailSearchResults> Email(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("must be populated", "email");

            var request = CreateRequest("/legacy/user/email/" + email);

            var response = await Complete<User.EmailSearchResults>(request);

            return response.Result;
        }
    }
}