using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public abstract class RepositoryBase
    {
        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "fork")]
        public bool Fork { get; set; }

        [DataMember(Name = "forks")]
        public int Forks { get; set; }

        [DataMember(Name = "has_downloads")]
        public bool HasDownloads { get; set; }

        [DataMember(Name = "has_issues")]
        public bool HasIssues { get; set; }

        [DataMember(Name = "has_wiki")]
        public bool HasWiki { get; set; }

        [DataMember(Name = "homepage")]
        public string Homepage { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "open_issues")]
        public int OpenIssues { get; set; }

        [DataMember(Name = "private")]
        public bool Private { get; set; }

        [DataMember(Name = "pushed_at")]
        public DateTime PushedAt { get; set; }

        [DataMember(Name = "size")]
        public long Size { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "watchers")]
        public int Watchers { get; set; }
    }

    [DataContract]
    public class Repository : RepositoryBase
    {
        [DataMember(Name = "archive_url")]
        public string ArchiveUrl { get; set; }

        [DataMember(Name = "assignees_url")]
        public string AssigneesUrl { get; set; }

        [DataMember(Name = "blobs_url")]
        public string BlobsUrl { get; set; }

        [DataMember(Name = "branches_url")]
        public string BranchesUrl { get; set; }

        [DataMember(Name = "clone_url")]
        public string CloneUrl { get; set; }

        [DataMember(Name = "collaborators_url")]
        public string CollaboratorsUrl { get; set; }

        [DataMember(Name = "comments_url")]
        public string CommentsUrl { get; set; }

        [DataMember(Name = "commits_url")]
        public string CommitsUrl { get; set; }

        [DataMember(Name = "compare_url")]
        public string CompareUrl { get; set; }

        [DataMember(Name = "contents_url")]
        public string ContentsUrl { get; set; }

        [DataMember(Name = "contributors_url")]
        public string ContributorsUrl { get; set; }

        [DataMember(Name = "default_branch")]
        public string DefaultBranch { get; set; }

        [DataMember(Name = "downloads_url")]
        public string DownloadsUrl { get; set; }

        [DataMember(Name = "events_url")]
        public string EventsUrl { get; set; }

        [DataMember(Name = "forks_count")]
        public int ForksCount { get; set; }

        [DataMember(Name = "forks_url")]
        public string ForksUrl { get; set; }

        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

        [DataMember(Name = "git_commits_url")]
        public string GitCommitsUrl { get; set; }

        [DataMember(Name = "git_refs_url")]
        public string GitRefsUrl { get; set; }

        [DataMember(Name = "git_tags_url")]
        public string GitTagsUrl { get; set; }

        [DataMember(Name = "git_url")]
        public string GitUrl { get; set; }

        [DataMember(Name = "hooks_url")]
        public string HooksUrl { get; set; }

        [DataMember(Name = "html_url")]
        public string HtmlUrl { get; set; }
        
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "issue_comment_url")]
        public string IssueCommentUrl { get; set; }

        [DataMember(Name = "issue_events_url")]
        public string IssueEventsUrl { get; set; }

        [DataMember(Name = "issues_url")]
        public string IssuesUrl { get; set; }

        [DataMember(Name = "keys_url")]
        public string KeysUrl { get; set; }

        [DataMember(Name = "labels_url")]
        public string LabelsUrl { get; set; }

        [DataMember(Name = "languages_url")]
        public string LanguagesUrl { get; set; }

        [DataMember(Name = "master_branch")]
        public string MasterBranch { get; set; }

        [DataMember(Name = "merges_url")]
        public string MergesUrl { get; set; }

        [DataMember(Name = "milestones_url")]
        public string MilestonesUrl { get; set; }

        [DataMember(Name = "mirror_url")]
        public string MirrorUrl { get; set; }

        [DataMember(Name = "network_count")]
        public int NetworkCount { get; set; }

        [DataMember(Name = "notifications_url")]
        public string NotificationsUrl { get; set; }

        [DataMember(Name = "open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [DataMember(Name = "organization")]
        public Entity Organization { get; set; }

        [DataMember(Name = "owner")]
        public Entity Owner { get; set; }

        [DataMember(Name = "permissions")]
        public Permissions Permissions { get; set; }

        [DataMember(Name = "pulls_url")]
        public string PullsUrl { get; set; }

        [DataMember(Name = "ssh_url")]
        public string SshUrl { get; set; }

        [DataMember(Name = "stargazers_url")]
        public string StargazersUrl { get; set; }

        [DataMember(Name = "statuses_url")]
        public string StatusesUrl { get; set; }

        [DataMember(Name = "subscribers_url")]
        public string SubscribersUrl { get; set; }

        [DataMember(Name = "subscription_url")]
        public string SubscriptionUrl { get; set; }

        [DataMember(Name = "svn_url")]
        public string SvnUrl { get; set; }

        [DataMember(Name = "tags_url")]
        public string TagsUrl { get; set; }

        [DataMember(Name="teams_url")]
        public string TeamsUrl { get; set; }

        [DataMember(Name="trees_url")]
        public string TreesUrl { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "watchers_count")]
        public int WatchersCount { get; set; }

        [DataContract]
        public class RepositorySearchResults
        {
            [DataMember(Name = "repositories")]
            public IEnumerable<RepositoryResult> Repositories { get; set; }

            [DataContract]
            public class RepositoryResult : RepositoryBase
            {
                [DataMember(Name = "created")]
                public DateTime Created { get; set; }

                [DataMember(Name = "followers")]
                public int Followers { get; set; }

                [DataMember(Name = "owner")]
                public string Owner { get; set; }

                [DataMember(Name = "pushed")]
                public DateTime Pushed { get; set; }

                [DataMember(Name = "score")]
                public double Score { get; set; }

                [DataMember(Name = "type")]
                public string Type { get; set; }

                [DataMember(Name = "username")]
                public string Username { get; set; }
            }
        }
    }
}
