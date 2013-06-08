using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Gist
    {
        [DataMember(Name = "comments")]
        public int Comments { get; set; }

        [DataMember(Name = "comments_url")]
        public string CommentsUrl { get; set; }

        [DataMember(Name = "commits_url")]
        public string CommitsUrl { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "files")]
        public IDictionary<string, GistFile> Files { get; set; }

        [DataMember(Name = "forks")]
        public IEnumerable<Fork> Forks { get; set; }

        [DataMember(Name = "forks_url")]
        public string ForksUrl { get; set; }

        [DataMember(Name = "git_pull_url")]
        public string GitPullUrl { get; set; }

        [DataMember(Name = "git_push_url")]
        public string GitPushUrl { get; set; }

        [DataMember(Name = "history")]
        public IEnumerable<VersionEntry> History { get; set; }

        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "public")]
        public bool Public { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataContract]
        public class NewGistPost
        {
            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "public")]
            public bool Public { get; set; }

            [DataMember(Name = "files")]
            public IDictionary<string, NewGistFile> Files { get; set; } 

            [DataContract]
            public class NewGistFile
            {
                [DataMember(Name = "content")]
                public string Content { get; set; }
            }
        }

        [DataContract]
        public class PatchedGistPost
        {
            public PatchedGistPost(){}

            public PatchedGistPost(Gist gist)
            {
                this.Id = gist.Id;

                this.Description = gist.Description;

                Files = new Dictionary<string, PatchedGistFile>();
                foreach (var file in gist.Files)
                    Files.Add(file.Key, new PatchedGistFile(file.Value));
            }

            public long Id { get; set; }

            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "files")]
            public IDictionary<string, PatchedGistFile> Files { get; set; }

            [DataContract]
            public class PatchedGistFile
            {
                public PatchedGistFile(){}

                public PatchedGistFile(GistFile gistFile)
                {
                    this.Filename = gistFile.Filename;
                    this.Content = gistFile.Content;
                }

                [DataMember(Name = "filename")]
                public string Filename { get; set; }

                [DataMember(Name = "content")]
                public string Content { get; set; }
            }
        }
    }
}
