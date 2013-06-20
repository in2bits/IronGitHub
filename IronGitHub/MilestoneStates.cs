using System.Runtime.Serialization;

namespace IronGitHub
{
    [DataContract]
    public enum MilestoneStates
    {
        [EnumMember(Value = "open")]
        Open,

        [EnumMember(Value = "closed")]
        Closed
    }
}