using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class ChangeStatus
    {
        [DataMember(Name = "deletions")]
        public int Deletions { get; set; }

        [DataMember(Name = "additions")]
        public int Additions { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}