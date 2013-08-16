using System.Net;

namespace IronGitHub.Exceptions
{
    public class UnauthorizedException : GitHubErrorException
    {
        public UnauthorizedException(HttpWebResponse response, GitHubErrorResponse errorResponse) 
            : base(response, errorResponse)
        {
        }
    }
}
