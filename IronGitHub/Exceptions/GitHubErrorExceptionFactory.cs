using System;
using System.Net;

namespace IronGitHub.Exceptions
{
    public static class GitHubErrorExceptionFactory
    {
        public static GitHubErrorException From(HttpWebResponse response, GitHubErrorResponse errorResponse)
        {
            var code = (int) response.StatusCode;
            switch (code)
            {
                case 401: return new UnauthorizedException(response, errorResponse);
                case 403: return new ForbiddenException(response, errorResponse);
                case 422: return new UnprocessableEntityException(response, errorResponse);
                default :
                    throw new NotSupportedException("GitHub Error Response Status " + code);
            }
        }
    }
}