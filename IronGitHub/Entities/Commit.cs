using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    /// <summary>
    /// This is meant to reflect the commit data returned from GitHub.
    /// Unfortunately I have seen a few different ways that this can be
    /// returned, so this might not refelct the full information available.
    /// </summary>
    [DataContract]
    public class Commit
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "sha")]
        public string Sha { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "author")]
        public CommitAuthor Author { get; set; }

        [DataMember(Name = "commiter")]
        public CommitAuthor Committer { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "distinct")]
        public bool Distinct { get; set; }

        /// <summary>
        /// List of filenames that were added
        /// </summary>
        [DataMember(Name = "added")]
        public IEnumerable<string> Added { get; set; }

        /// <summary>
        /// List of filenames that were removed
        /// </summary>
        [DataMember(Name = "removed")]
        public IEnumerable<string> Removed { get; set; }

        /// <summary>
        /// List of filenames that were modified
        /// </summary>
        [DataMember(Name = "modified")]
        public IEnumerable<string> Modified { get; set; }

        [DataContract]
        public class CommitAuthor
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "username")]
            public string Username { get; set; }

            [DataMember(Name = "email")]
            public string Email { get; set; }
        }
    }


}
