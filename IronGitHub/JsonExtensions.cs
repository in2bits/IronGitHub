using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace IronGitHub
{
    public static class JsonExtensions
    {
        public static void WriteAsJson<T>(this Stream stream, T obj)
        {
            JsonSerializer.SerializeToStream(obj, stream);
        }

        private static Dictionary<Scopes, string> _scopesJsonValues; 
        public static string ToJsonValue(this Scopes s)
        {
            if (_scopesJsonValues == null)
                InitScopesJsonValues();
            return _scopesJsonValues[s];
        }

        private static void InitScopesJsonValues()
        {
            _scopesJsonValues = new Dictionary<Scopes, string>();
            var type = typeof(Scopes);
            foreach (Scopes s in Enum.GetValues(typeof (Scopes)))
            {
                var memInfo = type.GetMember(s.ToString());
                var dmAttribute = memInfo[0].GetCustomAttributes(typeof (EnumMemberAttribute), false).FirstOrDefault() as EnumMemberAttribute;
                if (dmAttribute == null || dmAttribute.Value == null)
                    _scopesJsonValues[s] = s.ToString();
                else
                    _scopesJsonValues[s] = dmAttribute.Value;
            }
        }
    }
}
