using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    /// <summary>
    /// http://developer.github.com/v3/activity/events/types
    /// </summary>
    [DataContract]
    public abstract class EventBase
    {
        public SupportedEvents HookName { get; private set; }

        protected EventBase(SupportedEvents supportedEvent)
        {
            HookName = supportedEvent;
        }
    }

    [DataContract]
    public class CommitCommentEvent : EventBase
    {
        public CommitCommentEvent() : base(SupportedEvents.CommitComment) { }

        [DataMember(Name = "comment")]
        public Comment Comment { get; set; }
    }

    /// <summary>
    /// Represents a created repository, branch, or tag.
    /// </summary>
    [DataContract]
    public class CreateEvent : EventBase
    {
        public CreateEvent() : base(SupportedEvents.Create) { }

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

        /// <summary>
        /// The name of the repository’s master branch.
        /// </summary>
        [DataMember(Name = "master_branch")]
        public string MasterBranch { get; set; }

        /// <summary>
        /// The repository’s current description.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Represents a deleted branch or tag.
    /// </summary>
    [DataContract]
    public class DeleteEvent : EventBase
    {
        public DeleteEvent() : base(SupportedEvents.Delete) { }

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
    public class DownloadEvent : EventBase
    {
        public DownloadEvent() : base(SupportedEvents.Download) { }

        [DataMember(Name = "download")]
        public Download Download { get; set; }
    }

    [DataContract]
    public class FollowEvent : EventBase
    {
        public FollowEvent() : base(SupportedEvents.Follow) { }

        [DataMember(Name = "user")]
        public Entity User { get; set; }
    }

    [DataContract]
    public class ForkEvent : EventBase
    {
        public ForkEvent() : base(SupportedEvents.Fork) { }

        [DataMember(Name = "forkee")]
        public Repository Forkee { get; set; }
    }

    /// <summary>
    /// Triggered when a patch is applied in the Fork Queue.
    /// </summary>
    [DataContract]
    public class ForkApplyEvent : EventBase
    {
        public ForkApplyEvent() : base(SupportedEvents.ForkApply) { }

        /// <summary>
        /// The branch name the patch is applied to.
        /// </summary>
        [DataMember(Name = "head")]
        public string Head { get; set; }

        /// <summary>
        /// SHA of the repo state before the patch.
        /// </summary>
        [DataMember(Name = "before")]
        public string Before { get; set; }

        /// <summary>
        /// SHA of the repo state before the patch.
        /// </summary>
        [DataMember(Name = "after")]
        public string After { get; set; }
    }

    [DataContract]
    public class GistEvent : EventBase
    {
        public GistEvent() : base(SupportedEvents.Gist) { }

        /// <summary>
        /// The action that was performed: “create” or “update”
        /// </summary>
        [DataMember(Name = "action")]
        public GistAction Action { get; set; }

        [DataMember(Name = "gist")]
        public Gist Gist { get; set; }
    }

    [DataContract]
    public class GollumEvent : EventBase
    {
        public GollumEvent() : base(SupportedEvents.Gollum) { }

        /// <summary>
        /// The pages that were updated.
        /// </summary>
        [DataMember(Name = "pages")]
        public IEnumerable<Page> Pages { get; set; }
    }

    [DataContract]
    public class IssueCommentEvent : EventBase
    {
        public IssueCommentEvent() : base(SupportedEvents.IssueComment) { }

        /// <summary>
        /// The action that was performed on the comment.
        /// </summary>
        [DataMember(Name = "action")]
        public string Action { get; set; }

        /// <summary>
        /// The issue the comment belongs to.
        /// </summary>
        [DataMember(Name = "action")]
        public Issue Issue { get; set; }

        [DataMember(Name = "action")]
        public Comment Comment { get; set; }
    }

    [DataContract]
    public class IssuesEvent : EventBase
    {
        public IssuesEvent() : base(SupportedEvents.Issues) { }

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
    public class MemberEvent : EventBase
    {
        public MemberEvent() : base(SupportedEvents.Member) { }

        [DataMember(Name = "Member")]
        public Entity Member { get; set; }

        /// <summary>
        /// The action that was performed: “added”.
        /// </summary>
        [DataMember(Name = "action")]
        public RepositoryMemberAction Action { get; set; }
    }

    /// <summary>
    /// This is triggered when a private repo is open sourced. Without a doubt: the best GitHub event.
    /// It has an empty payload
    /// </summary>
    [DataContract]
    public class PublicEvent : EventBase
    {
        public PublicEvent() : base(SupportedEvents.Public) { }
    }

    [DataContract]
    public class PullRequestEvent : EventBase
    {
        public PullRequestEvent() : base(SupportedEvents.PullRequest) { }

        /// <summary>
        /// The action that was performed: “opened”, “closed”, “synchronize”, or “reopened”.
        /// </summary>
        [DataMember(Name = "action")]
        public PullRequestAction Action { get; set; }

        /// <summary>
        /// The pull request number.
        /// </summary>
        [DataMember(Name = "number")]
        public int Number { get; set; }

        [DataMember(Name = "pull_request")]
        public PullRequest PullRequest { get; set; }
    }

    [DataContract]
    public class PullRequestReviewCommentEvent : EventBase
    {
        public PullRequestReviewCommentEvent() : base(SupportedEvents.PullRequestReviewComment) { }

        [DataMember(Name = "comment")]
        public Comment Comment { get; set; }
    }

    public class PushEvent : EventBase
    {
        public PushEvent() : base(SupportedEvents.Push) { }

        /// <summary>
        /// The SHA of the HEAD commit on the repository.
        /// </summary>
        [DataMember(Name = "head")]
        public string Head { get; set; }

        /// <summary>
        /// The full Git ref that was pushed. Example: “refs/heads/master”
        /// </summary>
        [DataMember(Name = "ref")]
        public string Ref { get; set; }

        /// <summary>
        /// The number of commits in the push.
        /// </summary>
        [DataMember(Name = "size")]
        public int Size { get; set; }

        [DataMember(Name = "commits")]
        public IEnumerable<Commit> Commits { get; set; }
    }

    public class TeamAddEvent : EventBase
    {
        public TeamAddEvent() : base(SupportedEvents.TeamAdd) { }

        /// <summary>
        /// The team that was modified. Note: older events may not include this in the payload.
        /// </summary>
        [DataMember(Name = "team")]
        public Team Team { get; set; }

        /// <summary>
        /// The user that was added to this team.
        /// </summary>
        [DataMember(Name = "user")]
        public Entity User { get; set; }

        /// <summary>
        /// The repository that was added to this team.
        /// </summary>
        [DataMember(Name = "repo")]
        public Repository Repository { get; set; }
    }

    /// <summary>
    /// The event’s actor is the watcher, and the event’s repo is the watched repository.
    /// </summary>
    public class WatchEvent : EventBase
    {
        public WatchEvent() : base(SupportedEvents.Watch) { }

        [DataMember(Name = "action")]
        public string Action { get; set; }
    }

    [DataContract]
    public enum SupportedEvents
    {
        [EnumMember(Value = "commit_comment")]
        CommitComment,
        [EnumMember(Value = "create")]
        Create,
        [EnumMember(Value = "delete")]
        Delete,
        [EnumMember(Value = "download")]
        Download,
        [EnumMember(Value = "follow")]
        Follow,
        [EnumMember(Value = "fork")]
        Fork,
        [EnumMember(Value = "fork_apply")]
        ForkApply,
        [EnumMember(Value = "gist")]
        Gist,
        [EnumMember(Value = "gollum")]
        Gollum,
        [EnumMember(Value = "issue_comment")]
        IssueComment,
        [EnumMember(Value = "issues")]
        Issues,
        [EnumMember(Value = "member")]
        Member,
        [EnumMember(Value = "public")]
        Public,
        [EnumMember(Value = "pull_request")]
        PullRequest,
        [EnumMember(Value = "pull_request_review_comment")]
        PullRequestReviewComment,
        [EnumMember(Value = "push")]
        Push,
        [EnumMember(Value = "status")]
        Status,
        [EnumMember(Value = "team_add")]
        TeamAdd,
        [EnumMember(Value = "watch")]
        Watch
    }
}
