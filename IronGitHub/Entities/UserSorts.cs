using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public enum UserSorts
    {
        [EnumMember(Value = "followers")]
        Followers,

        [EnumMember(Value = "joined")]
        Joined,

        [EnumMember(Value = "repositories")]
        Repositories
    }

    public static class UserSortExtensions
    {
        public static string ToParameterValue(this UserSorts? sort)
        {
            if (sort == null)
                return null;
            return CustomEnumValueSerializer<UserSorts>.ToJsonValue(sort.Value);
        }
    }
}