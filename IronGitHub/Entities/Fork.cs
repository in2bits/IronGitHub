using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Fork
    {
        [DataMember][JsonProperty("user")]
        public User User { get; set; }

        [DataMember][JsonProperty("url")]
        public string Url { get; set; }

        [DataMember][JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}