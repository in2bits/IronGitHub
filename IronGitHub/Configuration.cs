using System;
using System.Linq;
using System.Reflection;

namespace IronGitHub
{
    public class Configuration
    {
        public const string DefaultDomain = "api.github.com";

        public readonly string DefaultUserAgent;

        public Configuration()
        {
            Domain = DefaultDomain;
            var version = "0.1";
            var versionAttribute = Assembly.GetExecutingAssembly().CustomAttributes
                .First(x => x.AttributeType == typeof (AssemblyFileVersionAttribute));
            if (versionAttribute != null)
                version = versionAttribute.ConstructorArguments[0].Value as string;
            UserAgent = "IronGitHub API v" + version;
        }

        public string Domain { get; set; }

        public string UserAgent { get; set; }
    }
}
