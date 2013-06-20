using System.Runtime.Serialization;

namespace IronGitHub
{
    [DataContract]
    public enum SortOrders
    {
        /// <summary>
        /// Ascending
        /// </summary>
        [EnumMember(Value = "asc")]
        Asc,

        /// <summary>
        /// Descending
        /// </summary>
        [EnumMember(Value = "desc")]
        Desc
    }

    public static class SortOrderExtensions
    {
        public static string ToParameterValue(this SortOrders? order)
        {
            if (order == null)
                return null;
            return CustomEnumValueSerializer<SortOrders>.ToJsonValue(order.Value);
        }
    }
}