using System;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class VersionEntry
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "change_status")]
        public ChangeStatus ChangeStatus { get; set; }

        [DataMember(Name = "committed_at")]
        public DateTime CommittedAt { get; set; }
    }
}