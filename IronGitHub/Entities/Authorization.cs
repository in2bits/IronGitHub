using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Authorization
    {
        [DataMember]
        [JsonProperty("app")]
        public App App { get; set; }

        [DataMember]
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("note")]
        public string Note { get; set; }

        [DataMember]
        [JsonProperty("note_url")]
        public string NoteUrl { get; set; }

        [DataMember]
        [JsonProperty("scopes")]
        public IEnumerable<Scopes> Scopes { get; set; }

        [DataMember]
        [JsonProperty("token")]
        public string Token { get; set; }

        [DataMember]
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember]
        [JsonProperty("url")]
        public string Url { get; set; }

        public bool IsValid { get; private set; }

        [DataContract]
        public class AuthorizeRequest
        {
            [DataMember]
            [JsonProperty("scopes")]
            public IEnumerable<Scopes> Scopes { get; set; }

            [DataMember]
            [JsonProperty("note")]
            public string Note { get; set; }
        }

        [DataContract]
        public class AuthorizeAppRequestPost : AuthorizeRequest
        {
            [DataMember]
            [JsonProperty("client_id")]
            public string ClientId { get; set; }

            [DataMember]
            [JsonProperty("client_secret")]
            public string ClientSecret { get; set; }
        }
    }

    [DataContract]
    public class App
    {
        [DataMember]
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
