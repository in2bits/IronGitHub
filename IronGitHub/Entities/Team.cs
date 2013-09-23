using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Team
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "permission")]
        public PermissionTypes Permission { get; set; }

        [DataMember(Name = "members_count")]
        public int MembersCount { get; set; }

        [DataMember(Name = "repos_count")]
        public int RepositoriesCount { get; set; }

        [DataContract]
        public enum PermissionTypes
        {
            /// <summary>
            /// Team members can pull, but not push to or administer these repositories. Default
            /// </summary>
            [EnumMember(Value = "pull")]
            Pull,
            /// <summary>
            /// Team members can pull and push, but not administer these repositories.
            /// </summary>
            [EnumMember(Value = "push")]
            Push,
            /// <summary>
            /// Team members can pull, push and administer these repositories.
            /// </summary>
            [EnumMember(Value = "admin")]
            Admin
        }
    }
}
