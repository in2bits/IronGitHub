using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub
{
    public sealed class MembersApi : GitHubApi
    {
        public MembersApi(GitHubApiContext context)
            : base(context)
        {
            
        }
    }
}
