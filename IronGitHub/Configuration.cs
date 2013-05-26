using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub
{
    public class Configuration
    {
        public const string DefaultDomain = "api.github.com";

        public const string DefaultUserAgent = "IronGitHub API v0.01";

        public Configuration()
        {
            Domain = DefaultDomain;
            UserAgent = DefaultUserAgent;
        }

        public string Domain { get; set; }

        public string UserAgent { get; set; }
    }
}
