using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public enum RepositorySorts
    {
        [EnumMember(Value = "stars")]
        Stars,

        [EnumMember(Value = "forks")]
        Forks,

        [EnumMember(Value = "updated")]
        Updated
    }

    public static class RepositorySortExtensions
    {
        public static string ToParameterValue(this RepositorySorts? sort)
        {
            if (sort == null)
                return null;
            return CustomEnumValueSerializer<RepositorySorts>.ToJsonValue(sort.Value);
        }
    }
}