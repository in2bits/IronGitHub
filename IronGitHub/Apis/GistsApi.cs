using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

        async public Task<Gist> New(IDictionary<string, string> files, string description = null, bool @public = true)
        {
            var newFiles = new Dictionary<string, Gist.NewGistPost.NewGistFile>();
            foreach (var key in files.Keys)
                newFiles[key] = new Gist.NewGistPost.NewGistFile {Content = files[key]};
            return await New(newFiles, description, @public);
        }

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

        async public Task<Gist> New(Gist.NewGistPost newGist)
        {
            var request = CreateRequest("/gists");

            var response = await PostAsJson<Gist.NewGistPost, Gist>(request, newGist);

            return response.Result;
        }

        async public Task<Gist> Get(long id)
        {
            var request = CreateRequest("/gists/" + id);

            var response = await Complete<Gist>(request);

            return response.Result;
        }

        async public Task Delete(Gist gist)
        {
            var request = CreateRequest("/gists/" + gist.Id);

            var response = await Delete(request);
            if (response.HttpResponse.StatusCode != HttpStatusCode.NoContent)
                throw new GitHubException(string.Format("Unexpected response code : {0} {1}",
                                                        response.HttpResponse.StatusCode,
                                                        response.HttpResponse.StatusDescription));
        }

        async public Task<Gist> Patch(Gist.PatchedGistPost patch)
        {
            var request = CreateRequest("/gists/" + patch.Id);
            var response = await Patch<Gist.PatchedGistPost, Gist>(request, patch);
            return response.Result;
        }
    }
}
