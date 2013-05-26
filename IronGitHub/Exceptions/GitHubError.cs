using System.Runtime.Serialization;

namespace IronGitHub.Exceptions
{
    [DataContract]
    public class GitHubError
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "field")]
        public string Field { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "resource")]
        public string Resource { get; set; }
    }
}