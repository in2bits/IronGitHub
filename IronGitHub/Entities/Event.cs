using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class EventBase
    {
        /// <summary>
        /// The object that was created: “repository”, “branch”, or “tag”
        /// </summary>
        [DataMember(Name = "ref_type")]
        public string RefType { get; set; }

        /// <summary>
        /// The git ref (or null if only a repository was created).
        /// </summary>
        [DataMember(Name = "ref")]
        public string Ref { get; set; }
    }

    [DataContract]
    public class CommitCommentEvent
    {
        [DataMember(Name = "comment")]
        public Comment Comment { get; set; }
    }

    [DataContract]
    public class CreateEvent : EventBase
    {
        [DataMember(Name = "master_branch")]
        public string MasterBranch { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }

    [DataContract]
    public class DeleteEvent : EventBase { }

    [DataContract]
    public class DownloadEvent
    {
        [DataMember(Name = "download")]
        public Download Download { get; set; }
    }
}
