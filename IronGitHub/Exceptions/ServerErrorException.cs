using System.Net;

namespace IronGitHub.Exceptions
{
    public class ServerErrorException : GitHubErrorException
    {
        public ServerErrorException(HttpWebResponse response, GitHubErrorResponse errorResponse) 
            : base(response, errorResponse)
        {
        }
    }
}
