using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub.Tests.Helpers
{
    public static class TestConfiguration
    {
        public static string GitHubUsername = Convert.ToString(ConfigurationManager.AppSettings["Username"]);

        public static string GitHubPassword = Convert.ToString(ConfigurationManager.AppSettings["Password"]);

        public static string GitHubRepository = Convert.ToString(ConfigurationManager.AppSettings["Repository"]);

        public static string TestRequestIdentifierDescription { get { return string.Format("IronGithub Integration Test - {0}", Guid.NewGuid().ToString("D")); } }
    }
}
