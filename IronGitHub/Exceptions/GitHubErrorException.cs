using System.IO;
using System.Net;

namespace IronGitHub.Exceptions
{
    public abstract class GitHubErrorException : GitHubException
    {
        public HttpWebResponse Response { get; private set; }

        public GitHubErrorResponse ErrorResponse { get; private set; }

        protected GitHubErrorException(HttpWebResponse response, GitHubErrorResponse errorResponse)
            : base(errorResponse.Message)
        {
            Response = response;
            ErrorResponse = errorResponse;
        }
    }
}