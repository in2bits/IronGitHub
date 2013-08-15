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

    [DataContract]
    public enum PullRequestAction
    {
        [EnumMember(Value = "opened")]
        Opened,
        [EnumMember(Value = "closed")]
        Closed,
        [EnumMember(Value = "synchronize")]
        Synchronize,
        [EnumMember(Value = "reopened")]
        Reopened
    }
}
