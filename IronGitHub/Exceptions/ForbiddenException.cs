using System.Net;

namespace IronGitHub.Exceptions
{
    public class ForbiddenException : GitHubErrorException
    {
        public ForbiddenException(HttpWebResponse response, GitHubErrorResponse errorResponse) 
            : base(response, errorResponse)
        {
        }
    }
}
