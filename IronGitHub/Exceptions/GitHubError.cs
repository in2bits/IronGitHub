using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IronGitHub.Exceptions
{
    [DataContract]
    public class GitHubError
    {
        [DataMember][JsonProperty("code")]
        public string Code { get; set; }

        [DataMember][JsonProperty("field")]
        public string Field { get; set; }

        [DataMember][JsonProperty("message")]
        public string Message { get; set; }

        [DataMember][JsonProperty("resource")]
        public string Resource { get; set; }
    }
}