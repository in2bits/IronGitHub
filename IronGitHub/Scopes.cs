using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IronGitHub
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Scopes
    {
        [EnumMember(Value="user")]
        User,

        [EnumMember(Value="user:email")]
        UserEmail,

        [EnumMember(Value="user:follow")]
        UserFollow
    }
}
