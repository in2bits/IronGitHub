using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
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