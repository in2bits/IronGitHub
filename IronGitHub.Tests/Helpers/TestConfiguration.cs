using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub.Tests.Helpers
{
    public static class TestConfiguration
    {
        public static string GitHubUsername = "...";

        public static string GitHubPassword = "...";

        public static string TestRequestIdentifierDescription { get { return string.Format("IronGithub Integration Test - {0}", Guid.NewGuid().ToString("D")); } }
    }

    public static class UserConfiguration
    {
        // TODO: get data from APP setting or JSON file.
    }
}
