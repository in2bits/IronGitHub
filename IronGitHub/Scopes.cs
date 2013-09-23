using System.Runtime.Serialization;

namespace IronGitHub
{
    [DataContract]
    public enum Scopes
    {
        [EnumMember(Value = "user")]
        User,

        [EnumMember(Value = "user:email")]
        UserEmail,

        [EnumMember(Value = "user:follow")]
        UserFollow,

        [EnumMember(Value = "public_repo")]
        PublicRepo,

        [EnumMember(Value = "repo")]
        Repo,

        [EnumMember(Value = "repo:status")]
        RepoStatus,

        [EnumMember(Value = "delete_repo")]
        DeleteRepo,

        [EnumMember(Value = "notifications")]
        Notifications,

        [EnumMember(Value = "gist")]
        Gist
    }
}
