using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Page
    {
        /// <summary>
        /// The name of the page.
        /// </summary>
        [DataMember(Name = "page_name")]
        public string PageName { get; set; }

        /// <summary>
        /// The name of the page.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// The action that was performed on the page.
        /// </summary>
        [DataMember(Name = "action")]
        public string Action { get; set; }

        /// <summary>
        /// The latest commit SHA of the page.
        /// </summary>
        [DataMember(Name = "sha")]
        public string Sha { get; set; }

        /// <summary>
        /// Points to the HTML wiki page.
        /// </summary>
        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }
    }
}
