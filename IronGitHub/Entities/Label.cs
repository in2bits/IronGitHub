using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Label
    {
        [DataMember(Name = "color")]
        public string Color { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
