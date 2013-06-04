using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Permissions
    {
        [DataMember(Name = "admin")]
        public bool Admin { get; set; }

        [DataMember(Name = "pull")]
        public bool Pull { get; set; }

        [DataMember(Name = "push")]
        public bool Push { get; set; }
    }
}