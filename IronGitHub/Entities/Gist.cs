using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

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
        public string Id { get; set; }

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
        public class EditGistPost
        {
            public EditGistPost() { }

            public EditGistPost(Gist gist)
            {
                this.Id = gist.Id;

                this.Description = gist.Description;

                Files = new Dictionary<string, PatchedGistFile>();
                foreach (var file in gist.Files)
                    Files.Add(file.Key, new PatchedGistFile(file.Value));
            }

            public string Id { get; set; }

            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "files")]
            public IDictionary<string, PatchedGistFile> Files { get; set; }

            [DataContract]
            public class PatchedGistFile
            {
                public PatchedGistFile() { }

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

        private static readonly Regex GistUrlRegex = new Regex(@"http(?:s)?://(?:api|gist)\.github\.com(?:/gists|/raw|/[^/]+)?/([0-9]+)(?:/.*$)?", RegexOptions.IgnoreCase);

        public static string ParseIdFromUrl(string url)
        {
            var match = GistUrlRegex.Match(url);

            if (match.Groups.Count < 2)
                return null;

            var group = match.Groups[1];

            return group.Captures[0].Value;
        }
    }

    [DataContract]
    public enum GistAction
    {
        [EnumMember(Value = "create")]
        Create,

        [EnumMember(Value = "update")]
        Update
    }
}
