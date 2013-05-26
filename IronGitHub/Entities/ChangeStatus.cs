using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IronGitHub.Entities
{
    [DataContract]
    public class ChangeStatus
    {
        [DataMember][JsonProperty("deletions")]
        public int Deletions { get; set; }

        [DataMember][JsonProperty("additions")]
        public int Additions { get; set; }

        [DataMember][JsonProperty("total")]
        public int Total { get; set; }
    }
}