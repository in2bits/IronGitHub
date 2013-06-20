using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class User : Entity
    {
        [DataMember(Name = "bio")]
        public string Bio { get; set; }

        [DataMember(Name = "blog")]
        public string Blog { get; set; }

        [DataMember(Name = "company")]
        public string Company { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "followers")]
        public int Followers { get; set; }

        [DataMember(Name = "following")]
        public int Following { get; set; }

        [DataMember(Name = "hireable")]
        public bool Hireable { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "public_gists")]
        public int PublicGists { get; set; }

        [DataMember(Name = "public_repos")]
        public int PublicRepos { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataContract]
        public class UserSearchResults
        {
            [DataMember(Name = "users")]
            public IEnumerable<UserResult> Users { get; set; }

            [DataContract]
            public class UserResult
            {
                [DataMember(Name = "created")]
                public DateTime Created { get; set; }

                [DataMember(Name = "created_at")]
                public DateTime CreatedAt { get; set; }

                [DataMember(Name = "followers")]
                public int Followers { get; set; }

                [DataMember(Name = "followers_count")]
                public int FollowersCount { get; set; }

                [DataMember(Name = "fullname")]
                public string FullName { get; set; }

                [DataMember(Name = "gravatar_id")]
                public string GravatarId { get; set; }

                [DataMember(Name = "id")]
                public string Id { get; set; }

                [DataMember(Name = "language")]
                public string Language { get; set; }

                [DataMember(Name = "location")]
                public string Location { get; set; }

                [DataMember(Name = "login")]
                public string Login { get; set; }

                [DataMember(Name = "name")]
                public string Name { get; set; }

                [DataMember(Name = "public_gists")]
                public int PublicGists { get; set; }

                [DataMember(Name = "public_repo_count")]
                public int PublicRepoCount { get; set; }

                [DataMember(Name = "repos")]
                public int Repos { get; set; }

                [DataMember(Name = "score")]
                public double Score { get; set; }

                [DataMember(Name = "type")]
                public string Type { get; set; }

                [DataMember(Name = "updated_at")]
                public DateTime UpdatedAt { get; set; }

                [DataMember(Name = "username")]
                public string Username { get; set; }
            }
        }

        [DataContract]
        public class EmailSearchResults
        {
            [DataMember(Name = "user")]
            public IEnumerable<EmailSearchResult> Users { get; set; }

            [DataContract]
            public class EmailSearchResult
            {
                [DataMember(Name = "blog")]
                public string Blog { get; set; }

                [DataMember(Name = "company")]
                public string Company { get; set; }

                [DataMember(Name = "created")]
                public DateTime Created { get; set; }

                [DataMember(Name = "created_at")]
                public DateTime CreatedAt { get; set; }

                [DataMember(Name = "email")]
                public string Email { get; set; }

                [DataMember(Name = "followers_count")]
                public int FollowersCount { get; set; }

                [DataMember(Name = "following_count")]
                public int FollowingCount { get; set; }

                [DataMember(Name = "gravatar_id")]
                public string GravatarId { get; set; }

                [DataMember(Name = "id")]
                public int Id { get; set; }

                [DataMember(Name = "location")]
                public string Location { get; set; }

                [DataMember(Name = "login")]
                public string Login { get; set; }

                [DataMember(Name = "name")]
                public string Name { get; set; }

                [DataMember(Name = "public_gist_count")]
                public int PublicGistCount { get; set; }

                [DataMember(Name = "public_repo_count")]
                public int PublicRepoCount { get; set; }

                [DataMember(Name = "type")]
                public string Type { get; set; }
            }
        }
    }
}