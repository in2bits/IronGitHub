using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IronGitHub.Entities
{
    [DataContract]
    public class VersionEntry
    {
        [DataMember][JsonProperty("url")]
        public string Url { get; set; }

        [DataMember][JsonProperty("version")]
        public string Version { get; set; }

        [DataMember][JsonProperty("user")]
        public User User { get; set; }

        [DataMember][JsonProperty("change_status")]
        public ChangeStatus ChangeStatus { get; set; }

        [DataMember][JsonProperty("committed_at")]
        public DateTime CommittedAt { get; set; }
    }
}