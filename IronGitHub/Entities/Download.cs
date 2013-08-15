using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Download
    {
        public string url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int size { get; set; }
        public int download_count { get; set; }
        public string content_type { get; set; }
    }
}
