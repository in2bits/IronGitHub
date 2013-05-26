using System;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Fork
    {
        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }
    }
}