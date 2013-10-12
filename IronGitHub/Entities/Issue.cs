using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public abstract class IssueBase
    {
        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "comments")]
        public int Comments { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }

        [DataMember(Name = "number")]
        public int Number { get; set; }

        [DataMember(Name = "state")]
        public IssueStates State { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "user")]
        public string User { get; set; }
    }

    [DataContract]
    public class Issue : IssueBase
    {
        [DataMember(Name = "assignee")]
        public Entity Assignee { get; set; }

        [DataMember(Name = "closed_at")]
        public DateTime? ClosedAt { get; set; }

        [DataMember(Name = "comments_url")]
        public string CommentsUrl { get; set; }

        [DataMember(Name = "events_url")]
        public string EventsUrl { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "labels")]
        public IEnumerable<Label> Labels { get; set; }

        [DataMember(Name = "labels_url")]
        public string LabelsUrl { get; set; }

        [DataMember(Name = "milestone")]
        public Milestone Milestone { get; set; }

        [DataMember(Name = "pull_request")]
        public PullRequest PullRequest { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataContract]
        public class IssueSearchResults
        {
            [DataMember(Name = "issues")]
            public IEnumerable<IssueResult> Issues { get; set; }

            [DataContract]
            public class IssueResult : IssueBase
            {
                [DataMember(Name = "gravatar_id")]
                public string GravatarId { get; set; }

                [DataMember(Name = "labels")]
                public IEnumerable<string> Labels { get; set; }

                [DataMember(Name = "position")]
                public double Position { get; set; }

                [DataMember(Name = "votes")]
                public int Votes { get; set; }
            }
        }

        [DataContract]
        public class IssueItem : Issue
        {
            [DataMember(Name = "repository")]
            public Repository Repository { get; set; }
        }
    }

    [DataContract]
    public class NewIssue
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }
    }

    [DataContract]
    public enum IssueAction
    {
        [EnumMember(Value = "opened")]
        Opened,
        [EnumMember(Value = "closed")]
        Closed,
        [EnumMember(Value = "reopened")]
        Reopened
    }
}