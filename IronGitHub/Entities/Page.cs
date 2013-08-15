using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Page
    {
        [DataMember(Name = "page_name")]
        public string PageName { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "sha")]
        public string Sha { get; set; }

        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }
    }
}
