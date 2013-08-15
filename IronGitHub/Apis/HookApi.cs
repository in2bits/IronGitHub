using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub.Apis
{
    using System.Net;

    using IronGitHub.Entities;
    using IronGitHub.Exceptions;

    public class HookApi : GitHubApiBase
    {
        public HookApi(GitHubApiContext context) : base(context)
        {
        }

        /// <summary>
        /// List all existing Hooks for a given owner and repository
        /// </summary>
        /// <param name="owner">The username of the owner of the hooks</param>
        /// <param name="repo">The repository where the hooks should be retrieved from</param>
        /// <returns>List of Hooks</returns>
        async public Task<IEnumerable<Hook>> Get(string owner, string repo)
        {
            var request = CreateRequest(string.Format("/repos/{0}/{1}/hooks", owner, repo));

            var response = await Complete<IEnumerable<Hook>>(request);

            return response.Result;
        }

        /// <summary>
        /// A single hook for a given owner, repository, and ID
        /// </summary>
        /// <param name="owner">The username of the owner of the hook</param>
        /// <param name="repo">The repository where the hooks should be retrieved from</param>
        /// <param name="id">The id of the individual hook to retrieve</param>
        /// <returns>The Hook with the specified Id</returns>
        async public Task<Hook> GetById(string owner, string repo, int id)
        {
            var request = CreateRequest(string.Format("/repos/{0}/{1}/hooks/{2}", owner, repo, id));

            var response = await Complete<Hook>(request);

            return response.Result;
        }

        /// <summary>
        /// Creates a new hook for the owner on the given repository
        /// </summary>
        /// <param name="owner">The username of the owner of the hook</param>
        /// <param name="repo">The repository where the hooks should be created</param>
        /// <param name="hook">The basic information required by GitHub to create the hook itself</param>
        /// <returns>The complete hook returned by GitHub</returns>
        async public Task<Hook> Create(string owner, string repo, HookBase hook)
        {
            var request = CreateRequest(string.Format("/repos/{0}/{1}/hooks", owner, repo));

            var post = await PostAsJson<HookBase, Hook>(request, hook);
            
            return post.Result;
        }

        /// <summary>
        /// Edits a hook for the owner on the given repository
        /// </summary>
        /// <param name="owner">The username of the owner of the hook</param>
        /// <param name="repo">The repository where the hooks should be created</param>
        /// <param name="id">The id of the individual hook to edit</param>
        /// <param name="hook">
        /// The information to update the existing hook with. *Every property is optional*
        /// If you set the 'Events' property of hook then the values will replace all
        /// existing events for this hook.
        /// </param>
        /// <returns>The complete hook returned by GitHub</returns>
        async public Task<Hook> Edit(string owner, string repo, int id, Hook.PatchHook hook)
        {
            var request = CreateRequest(string.Format("/repos/{0}/{1}/hooks", owner, repo));

            var response = await Patch<Hook.PatchHook, Hook>(request, hook);

            return response.Result;
        }

        /// <summary>
        /// Deletes a hook by id for the owner on the given repository
        /// </summary>
        /// <param name="owner">The username of the owner of the hook</param>
        /// <param name="repo">The repository where the hook should be removed from</param>
        /// <param name="id">The id of the individual hook to delete</param>
        /// <returns></returns>
        async public Task Delete(string owner, string repo, int id)
        {
            var request = CreateRequest(string.Format("/repos/{0}/{1}/hooks/{2}", owner, repo, id));

            var response = await Delete(request);

            if (response.HttpResponse.StatusCode != HttpStatusCode.NoContent)
                throw new GitHubException(string.Format("Unexpected response code : {0} {1}",
                                                        response.HttpResponse.StatusCode,
                                                        response.HttpResponse.StatusDescription));
        }

        /// <summary>
        /// This will trigger the hook with the latest push to the current repository if the hook is 
        /// subscribed to push events. If the hook is not subscribed to push events, the server will 
        /// respond with 204 but no test POST will be generated.
        /// </summary>
        /// <param name="owner">The username of the owner of the hook</param>
        /// <param name="repo">The repository where the hook is located</param>
        /// <param name="id">The id of the individual hook to delete</param>
        /// <returns></returns>
        async public Task Test(string owner, string repo, int id)
        {
            var request = CreateRequest(string.Format("/repos/{0}/{1}/hooks/{2}/tests", owner, repo, id));

            var response = await Complete(request);

            if (response.HttpResponse.StatusCode != HttpStatusCode.NoContent)
                throw new GitHubException(string.Format("Unexpected response code : {0} {1}",
                                                        response.HttpResponse.StatusCode,
                                                        response.HttpResponse.StatusDescription));
        }
    }
}
