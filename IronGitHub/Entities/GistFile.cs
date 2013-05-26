using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IronGitHub.Entities
{
    [DataContract]
    public class GistFile
    {
        [DataMember][JsonProperty("content")]
        public string Content { get; set; }

        [DataMember][JsonProperty("filename")]
        public string Filename { get; set; }

        [DataMember][JsonProperty("language")]
        public string Language { get; set; }

        [DataMember][JsonProperty("raw_url")]
        public string RawUrl { get; set; }

        [DataMember][JsonProperty("size")]
        public long Size { get; set; }

        [DataMember][JsonProperty("type")]
        public string Type { get; set; }
    }
}