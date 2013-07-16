using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using IronGitHub.Entities;
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
            JsConfig.IncludeNullValues = true;
            
            CustomEnumValueSerializer<Scopes>.Init();
            CustomEnumValueSerializer<IssueStates>.Init();
            CustomEnumValueSerializer<MilestoneStates>.Init();
            CustomEnumValueSerializer<SortOrders>.Init();
			CustomEnumValueSerializer<RepositorySorts>.Init();
			CustomEnumValueSerializer<RepositoryTypes>.Init();
			CustomEnumValueSerializer<UserSorts>.Init();
            
            _initted = true;
        }

        public static void WriteAsJson<T>(this Stream stream, T obj)
        {
            JsonSerializer.SerializeToStream(obj, stream);
        }
    }

    public class CustomEnumValueSerializer<TEnum>
    {
        private static Dictionary<TEnum, string> _strings;
        private static Dictionary<string, TEnum> _values;

        static CustomEnumValueSerializer()
        {
            InitDictionaries();
            Register();
        }

        private CustomEnumValueSerializer()
        {
        }

        public static void Init()
        {
            //no-op just to allow calling to execute the static constructor
        }

        public static string ToJsonValue(TEnum s)
        {
            return _strings[s];
        }

        public static TEnum ToValue(string s)
        {
            return _values[s];
        }

        private static void InitDictionaries()
        {
            _strings = new Dictionary<TEnum, string>();
            _values = new Dictionary<string, TEnum>();
            var type = typeof(TEnum);
            foreach (TEnum s in Enum.GetValues(typeof(TEnum)))
            {
                var memInfo = type.GetMember(s.ToString());
                var enumMemberAttribute = memInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault() as EnumMemberAttribute;
                if (enumMemberAttribute == null || enumMemberAttribute.Value == null)
                    _strings[s] = s.ToString();
                else
                    _strings[s] = enumMemberAttribute.Value;
                _values[_strings[s]] = s;
            }
        }

        private static void Register()
        {
            JsConfig<TEnum>.SerializeFn = ToJsonValue;
            JsConfig<TEnum>.DeSerializeFn = ToValue;
        }
    }
}
