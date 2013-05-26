using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Exceptions
{
    [DataContract]
    public class GitHubErrorResponse
    {
        [DataMember(Name = "errors")]
        public IEnumerable<GitHubError> Errors { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}