using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using IronGitHub.Exceptions;
using Newtonsoft.Json;
//using ServiceStack.Text;

namespace IronGitHub
{
    [DataContract]
    public class Authorization
    {
        [DataMember(Name = "app")]
        public App App { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "note_url")]
        public string NoteUrl { get; set; }

        [DataMember(Name = "scopes")]
        public IEnumerable<Scopes> Scopes { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        public bool IsValid { get; private set; }

        [DataContract]
        public class AuthorizeRequest
        {
            [DataMember(Name="scopes")]
            public IEnumerable<Scopes> Scopes { get; set; }

            [DataMember(Name = "note")]
            public string Note { get; set; }
        }

        [DataContract]
        public class AuthorizeAppRequestPost : AuthorizeRequest
        {
            [DataMember(Name = "client_id")]
            public string ClientId { get; set; }

            [DataMember(Name = "client_secret")]
            public string ClientSecret { get; set; }
        }
    }

    [DataContract]
    public class App
    {
        [DataMember(Name = "client_id")]
        public string ClientId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
