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
    public class GistsApi : GitHubApi
    {
        public GistsApi(GitHubApiContext context) : base(context)
        {
        }

        async public Task<Gist> New(IDictionary<string, string> files, string description = null, bool @public = true)
        {
            var request = CreateRequest("/gists");

            var newFiles = new Dictionary<string, Gist.NewGistPost.NewGistFile>();
            foreach (var key in files.Keys)
                newFiles[key] = new Gist.NewGistPost.NewGistFile {Content = files[key]};
            var gistRequest = new Gist.NewGistPost
                {
                    Description = description,
                    Public = @public,
                    Files = newFiles
                };

            await request.PostAsJson(gistRequest);

            var response = await request.Complete<Gist>();

            return response.Result;
        }

        async public Task<Gist> Get(long id)
        {
            var request = CreateRequest("/gists/" + id);

            var response = await request.Complete<Gist>();

            return response.Result;
        }

        async public Task Delete(Gist gist)
        {
            var request = CreateRequest("/gists/" + gist.Id);

            var response = await request.Delete();
            if (response.HttpResponse.StatusCode != HttpStatusCode.NoContent)
                throw new GitHubException(string.Format("Unexpected response code : {0} {1}",
                                                        response.HttpResponse.StatusCode,
                                                        response.HttpResponse.StatusDescription));
        }
    }
}
