using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using IronGitHub.Exceptions;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Authorization
    {
        public static readonly Authorization Anonymous = new Authorization();

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

        [DataContract]
        public class AuthorizeRequest
        {
            [DataMember(Name = "scopes")]
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

        public event EventHandler Invalidated;

        internal void Invalidate()
        {
            var handler = Invalidated;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public int? RateLimitLimit { get; private set; }
        public int? RateLimitRemaining { get; private set; }

        public void UpdateRateLimit(HttpWebResponse response)
        {
            const string rateLimitLimitKey = "X-RateLimit-Limit";
            const string rateLimitRemainingKey = "X-RateLimit-Remaining";
            if (!string.IsNullOrEmpty(response.Headers[rateLimitLimitKey]))
                RateLimitLimit = Convert.ToInt32(response.Headers[rateLimitLimitKey]);
            if (!string.IsNullOrEmpty(response.Headers[rateLimitRemainingKey]))
                RateLimitRemaining = Convert.ToInt32(response.Headers[rateLimitRemainingKey]);
        }

        public void CheckRateLimit()
        {
            if (RateLimitLimit == null || RateLimitRemaining == null)
                return;
            if (RateLimitRemaining.Value == 0)
            {
                var isAnonymous = this == Anonymous;
                throw new RateLimitExceededException(isAnonymous ? "Anonymous " : "" + "RateLimit of " + RateLimitLimit.Value + " exceeded!");
            }
        }
    }
}
