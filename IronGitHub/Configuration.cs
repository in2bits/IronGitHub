using System.Linq;
using System.Reflection;

namespace IronGitHub
{
    public class Configuration
    {
        public const string DefaultDomain = "api.github.com";

        public readonly string DefaultUserAgent;

        /// <summary>
        /// This is here to prevent a breaking change in the Library.
        /// The DefaultDomain actually changes for GitHub Enterprise, so this change
        /// allows for GHE support.
        /// </summary>
        public Configuration() : this(DefaultDomain)
        {
        }

        public Configuration(string defaultDomain)
        {
            Domain = defaultDomain;
            var version = "0.1";
            var versionAttribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true)
                .OfType<AssemblyFileVersionAttribute>()
                .First();
            if (versionAttribute != null)
                version = versionAttribute.Version.ToString();
            UserAgent = "IronGitHub API v" + version;
        }
        public string Domain { get; set; }

        public string UserAgent { get; set; }
    }
}
