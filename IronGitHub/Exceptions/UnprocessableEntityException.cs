using System;
using System.Net;

namespace IronGitHub.Exceptions
{
    public class UnprocessableEntityException : GitHubErrorException
    {
        public UnprocessableEntityException(HttpWebResponse response, GitHubErrorResponse errorResponse)
            : base(response, errorResponse)
        {           
            if ((int) response.StatusCode != 422)
                throw new ArgumentException("Response must be a 422");
        }
    }
}