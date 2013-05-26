using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class GistFile
    {
        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "filename")]
        public string Filename { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "raw_url")]
        public string RawUrl { get; set; }

        [DataMember(Name = "size")]
        public long Size { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}