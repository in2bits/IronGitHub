using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class User : Entity
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

        [DataMember(Name = "hireable")]
        public bool Hireable { get; set; }

        [DataMember(Name = "bio")]
        public string Bio { get; set; }

        [DataMember(Name = "public_repos")]
        public int PublicRepos { get; set; }

        [DataMember(Name = "followers")]
        public int Followers { get; set; }

        [DataMember(Name = "following")]
        public int Following { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "public_gists")]
        public int PublicGists { get; set; }

        [DataContract]
        public class UserList
        {
            [DataMember(Name = "users")]
            public IEnumerable<User> Users { get; set; }
        }
    }
}