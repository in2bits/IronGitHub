using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub.Entities
{
    [DataContract]
    public class PullRequest
    {
        [DataMember(Name = "diff_url")]
        public string DiffUrl { get; set; }

        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }

        [DataMember(Name = "patch_url")]
        public string PatchUrl { get; set; }
    }
}
