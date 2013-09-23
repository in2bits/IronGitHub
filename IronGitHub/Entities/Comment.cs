using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Comment
    {
        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "position")]
        public int Position { get; set; }

        [DataMember(Name = "line")]
        public int Line { get; set; }

        [DataMember(Name = "commit_id")]
        public string CommitId { get; set; }

        [DataMember(Name = "user")]
        public Entity User { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public string UpdatedAt { get; set; }
    }
}