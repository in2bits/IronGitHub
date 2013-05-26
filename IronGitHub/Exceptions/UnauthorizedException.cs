using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
