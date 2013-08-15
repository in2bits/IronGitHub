using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    using System.Collections.Generic;

    [DataContract]
    public class EventBase
    {
        /// <summary>
        /// The object that was created: “repository”, “branch”, or “tag”
        /// </summary>
        [DataMember(Name = "ref_type")]
        public string RefType { get; set; }

        /// <summary>
        /// The git ref (or null if only a repository was created).
        /// </summary>
        [DataMember(Name = "ref")]
        public string Ref { get; set; }
    }

    [DataContract]
    public class CommitCommentEvent
    {
        public string HookName
        {
            get
            {
                return "commit_comment";
            }
        }

        [DataMember(Name = "comment")]
        public Comment Comment { get; set; }
    }

    /// <summary>
    /// Represents a created repository, branch, or tag.
    /// </summary>
    [DataContract]
    public class CreateEvent : EventBase
    {
        public string HookName
        {
            get
            {
                return "create";
            }
        }

        [DataMember(Name = "master_branch")]
        public string MasterBranch { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Represents a deleted branch or tag.
    /// </summary>
    [DataContract]
    public class DeleteEvent : EventBase
    {
        public string HookName
        {
            get
            {
                return "delete";
            }
        }
    }

    [DataContract]
    public class DownloadEvent
    {
        public string HookName
        {
            get
            {
                return "download";
            }
        }

        [DataMember(Name = "download")]
        public Download Download { get; set; }
    }

    [DataContract]
    public class FollowEvent
    {
        public string HookName
        {
            get
            {
                return "follow";
            }
        }

        [DataMember(Name = "user")]
        public Entity User { get; set; }
    }

    [DataContract]
    public class ForkEvent
    {
        public string HookName
        {
            get
            {
                return "fork";
            }
        }

        [DataMember(Name = "forkee")]
        public Repository Forkee { get; set; }
    }

    /// <summary>
    /// Triggered when a patch is applied in the Fork Queue.
    /// </summary>
    [DataContract]
    public class ForkApplyEvent
    {
        public string HookName
        {
            get
            {
                return "fork_apply";
            }
        }

        [DataMember(Name = "head")]
        public string Head { get; set; }

        [DataMember(Name = "before")]
        public string Before { get; set; }

        [DataMember(Name = "after")]
        public string After { get; set; }
    }

    [DataContract]
    public class GistEvent
    {
        public string HookName
        {
            get
            {
                return "gist";
            }
        }

        /// <summary>
        /// The action that was performed: “create” or “update”
        /// </summary>
        [DataMember(Name = "action")]
        public GistAction Action { get; set; }

        [DataMember(Name="gist")]
        public Gist Gist { get; set; }
    }

    [DataContract]
    public class GollumEvent
    {
        public string HookName
        {
            get
            {
                return "gollum";
            }
        }

        //TODO: figure out how this payload is structured
        [DataMember(Name = "pages")]
        public IEnumerable<object> Pages { get; set; }
    }

    [DataContract]
    public class IssueCommentEvent
    {
        public string HookName
        {
            get
            {
                return "issue_comment";
            }
        }

        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "action")]
        public Issue Issue { get; set; }

        [DataMember(Name = "action")]
        public Comment Comment { get; set; }
    }

    [DataContract]
    public class IssuesEvent
    {
        public string HookName
        {
            get
            {
                return "issues";
            }
        }

        /// <summary>
        /// The action that was performed: “opened”, “closed”, or “reopened”.
        /// </summary>
        [DataMember(Name = "action")]
        public IssuesEvent Action { get; set; }

        [DataMember(Name = "action")]
        public Issue Issue { get; set; }
    }

    /// <summary>
    /// Triggered when a user is added as a collaborator to a repository.
    /// </summary>
    [DataContract]
    public class MemberEvent
    {
        public string HookName
        {
            get
            {
                return "member";
            }
        }

        /// <summary>
        /// The action that was performed: “added”.
        /// </summary>
        [DataMember(Name = "action")]
        public RepositoryMemberAction Action { get; set; }

        [DataMember(Name = "Member")]
        public Entity Member { get; set; }
    }

    /// <summary>
    /// This is triggered when a private repo is open sourced. Without a doubt: the best GitHub event.
    /// It has an empty payload
    /// </summary>
    [DataContract]
    public class PublicEvent
    {
        public string HookName
        {
            get
            {
                return "public";
            }
        }
    }

    [DataContract]
    public class PullRequestEvent
    {
        public string HookName
        {
            get
            {
                return "pull_request";
            }
        }

        /// <summary>
        /// The action that was performed: “opened”, “closed”, “synchronize”, or “reopened”.
        /// </summary>
        [DataMember(Name = "action")]
        public PullRequestAction Action { get; set; }

        [DataMember(Name = "number")]
        public int Number { get; set; }

        [DataMember(Name = "pull_request")]
        public PullRequest PullRequest { get; set; }
    }
}
