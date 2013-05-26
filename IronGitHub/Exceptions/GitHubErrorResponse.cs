using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IronGitHub.Exceptions
{
    public class GitHubErrorResponse
    {
        [DataMember][JsonProperty("errors")]
        public IEnumerable<GitHubError> Errors { get; set; }

        [DataMember][JsonProperty("message")]
        public string Message { get; set; }
    }
}