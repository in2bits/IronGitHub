using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub
{
    public class Application
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AppName { get; set; }
        public string Url { get; set; }
        public string CallbackUrl { get; set; }
    }
}
