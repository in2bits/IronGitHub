using System.Runtime.Serialization;

namespace IronGitHub
{
    [DataContract]
    public enum IssueStates
    {
        [EnumMember(Value = "open")]
        Open,

        [EnumMember(Value = "closed")]
        Closed
    }

    public static class IssueStatesExtensions
    {
        public static string ToJsonValue(this IssueStates state)
        {
            return CustomEnumValueSerializer<IssueStates>.ToJsonValue(state);
        }
    }
}