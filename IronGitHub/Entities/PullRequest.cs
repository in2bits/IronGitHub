using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class PullRequest
    {
        [DataMember(Name = "diff_url")]
        public string DiffUrl { get; set; }

        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }

        [DataMember(Name = "patch_url")]
        public string PatchUrl { get; set; }
    }

    public enum PullRequestAction
    {
        opened, 
        closed, 
        synchronize, 
        reopened
    }
}
