using System;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Organization : Entity
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "company")]
        public string Company { get; set; }

        [DataMember(Name = "blog")]
        public string Blog { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "public_repos")]
        public int PublicRepositoryCount { get; set; }

        [DataMember(Name = "public_gists")]
        public int PublicGistCount { get; set; }

        [DataMember(Name = "followers")]
        public int FollowersCount { get; set; }

        [DataMember(Name = "following")]
        public int FollowingCount { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

    }
}
