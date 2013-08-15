using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class HookBase
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "events")]
        public IEnumerable<string> Events { get; set; }

        [DataMember(Name = "active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// This is a hash of configuration settings to pass to GitHub.
        /// The sample for a "WebHook" is:
        ///   "config": {
        ///         "url": "http://example.com/webhook",
        ///         "content_type": "json"
        ///   }
        /// http://developer.github.com/v3/repos/hooks/
        /// </summary>
        [DataMember(Name = "config")]
        public Dictionary<string, string> Config { get; set; }
    }

    [DataContract]
    public class Hook : HookBase
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataContract]
        public class PatchHook : HookBase
        {
            /// <summary>
            /// This is only used for Editing (PATCH) a hook
            /// </summary>
            [DataMember(Name = "add_events")]
            public IEnumerable<SupportedEvents> AddEvents { get; set; }

            /// <summary>
            /// This is only used for Editing (PATCH) a hook
            /// </summary>
            [DataMember(Name = "remove_events")]
            public IEnumerable<SupportedEvents> RemoveEvents { get; set; }
        }
    }
}