using System.Net;

namespace IronGitHub.Exceptions
{
    public class NotFoundException : GitHubErrorException
    {
        public NotFoundException(HttpWebResponse response, GitHubErrorResponse errorResponse)
            : base(response, errorResponse)
        {
        }
    }
}