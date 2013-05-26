using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IronGitHub.Entities
{
    public class Gist
    {
        [DataMember][JsonProperty("comments")]
        public int Comments { get; set; }

        [DataMember][JsonProperty("comments_url")]
        public string CommentsUrl { get; set; }

        [DataMember][JsonProperty("commits_url")]
        public string CommitsUrl { get; set; }

        [DataMember][JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember][JsonProperty("description")]
        public string Descripton { get; set; }

        [DataMember][JsonProperty("files")]
        public IDictionary<string, GistFile> Files { get; set; }

        [DataMember][JsonProperty("forks")]
        public IEnumerable<Fork> Forks { get; set; }

        [DataMember][JsonProperty("history")]
        public IEnumerable<VersionEntry> History { get; set; }

        [DataContract]
        public class NewGistPost
        {
            [DataMember][JsonProperty("description")]
            public string Description { get; set; }

            [DataMember][JsonProperty("public")]
            public bool Public { get; set; }

            [DataMember][JsonProperty("files")]
            public IDictionary<string, NewGistFile> Files { get; set; } 

            [DataContract]
            public class NewGistFile
            {
                [DataMember][JsonProperty("content")]
                public string Content { get; set; }
            }
        }
    }
}
