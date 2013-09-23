using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IronGitHub.Entities;
using IronGitHub.Exceptions;

namespace IronGitHub.Apis
{
    public class GistsApi : GitHubApiBase
    {
        public GistsApi(GitHubApiContext context) : base(context)
        {
        }

        /// <summary>
        /// Create a gist
        /// </summary>
        /// <param name="files">Files that make up this gist. The key of which 
        /// should be a required string filename and the value another 
        /// required hash with parameters</param>
        /// <param name="description">(Optional)</param>
        /// <param name="public">(Optional)</param>
        /// <returns>The new Gist</returns>
        async public Task<Gist> New(IDictionary<string, string> files, string description = null, bool @public = true)
        {
            var newFiles = new Dictionary<string, Gist.NewGistPost.NewGistFile>();
            foreach (var key in files.Keys)
                newFiles[key] = new Gist.NewGistPost.NewGistFile {Content = files[key]};
            return await New(newFiles, description, @public);
        }

        /// <summary>
        /// Create a gist
        /// </summary>
        /// <param name="files">Files that make up this gist. The key of which 
        /// should be a required string filename and the value another 
        /// required hash with parameters</param>
        /// <param name="description">(Optional)</param>
        /// <param name="public">(Optional)</param>
        /// <returns>The new Gist</returns>
        async public Task<Gist> New(IDictionary<string, Gist.NewGistPost.NewGistFile> files, string description = null, bool @public = true)
        {
            var gistRequest = new Gist.NewGistPost
            {
                Description = description,
                Public = @public,
                Files = files
            };

            return await New(gistRequest);
        }

        /// <summary>
        /// Create a gist
        /// </summary>
        /// <param name="newGist">The new Gist.NewGistPost to create</param>
        /// <returns>The new Gist</returns>
        async public Task<Gist> New(Gist.NewGistPost newGist)
        {
            var request = CreateRequest("/gists");

            var response = await PostAsJson<Gist.NewGistPost, Gist>(request, newGist);

            return response.Result;
        }

        /// <summary>
        /// Get a single gist
        /// </summary>
        /// <param name="id">The Id of the Gist to get</param>
        /// <returns>The Gist</returns>
        async public Task<Gist> Get(long id)
        {
            var request = CreateRequest("/gists/" + id);

            var response = await Complete<Gist>(request);

            return response.Result;
        }

        /// <summary>
        /// Delete a gist
        /// </summary>
        /// <param name="gist">The Gist to delete</param>
        /// <returns>Void</returns>
        async public Task Delete(Gist gist)
        {
            var request = CreateRequest("/gists/" + gist.Id);

            var response = await Delete(request);
            if (response.HttpResponse.StatusCode != HttpStatusCode.NoContent)
                throw new GitHubException(string.Format("Unexpected response code : {0} {1}",
                                                        response.HttpResponse.StatusCode,
                                                        response.HttpResponse.StatusDescription));
        }

        /// <summary>
        /// Edit a gist
        /// </summary>
        /// <param name="patch">The EditGistPost comprising the edits to be made</param>
        /// <returns>The edited Gist</returns>
        async public Task<Gist> Edit(Gist.EditGistPost patch)
        {
            var request = CreateRequest("/gists/" + patch.Id);
            var response = await Patch<Gist.EditGistPost, Gist>(request, patch);
            return response.Result;
        }
    }
}
