using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using ServiceStack.Text;

namespace IronGitHub
{
    public static class JsonExtensions
    {
        static JsonExtensions()
        {
            Init();
        }

        private static bool _initted;
        public static void Init()
        {
            if (_initted)
                return;
            JsConfig<Scopes>.SerializeFn = s => s.ToJsonValue();
            JsConfig<Scopes>.DeSerializeFn = s => s.ToScopesValue();
            _initted = true;
        }

        public static void WriteAsJson<T>(this Stream stream, T obj)
        {
            JsonSerializer.SerializeToStream(obj, stream);
        }

        private static Dictionary<Scopes, string> _scopesStrings;
        private static Dictionary<string, Scopes> _scopesValues;
        public static string ToJsonValue(this Scopes s)
        {
            if (_scopesStrings == null)
                InitScopesDictionaries();
            return _scopesStrings[s];
        }

        public static Scopes ToScopesValue(this string s)
        {
            if (_scopesValues == null)
                InitScopesDictionaries();
            return _scopesValues[s];
        }

        private static void InitScopesDictionaries()
        {
            _scopesStrings = new Dictionary<Scopes, string>();
            _scopesValues = new Dictionary<string, Scopes>();
            var type = typeof(Scopes);
            foreach (Scopes s in Enum.GetValues(typeof (Scopes)))
            {
                var memInfo = type.GetMember(s.ToString());
                var dmAttribute = memInfo[0].GetCustomAttributes(typeof (EnumMemberAttribute), false).FirstOrDefault() as EnumMemberAttribute;
                if (dmAttribute == null || dmAttribute.Value == null)
                    _scopesStrings[s] = s.ToString();
                else
                    _scopesStrings[s] = dmAttribute.Value;
                _scopesValues[_scopesStrings[s]] = s;
            }
        }
    }
}
