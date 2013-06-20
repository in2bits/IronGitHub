using System;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class Milestone
    {
        [DataMember(Name = "closed_issues")]
        public int ClosedIssues { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "creator")]
        public User Creator { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "due_on")]
        public DateTime? DueOn { get; set; }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "labels_url")]
        public string LabelsUrl { get; set; }

        [DataMember(Name = "number")]
        public int Number { get; set; }

        [DataMember(Name = "open_issues")]
        public int OpenIssues { get; set; }

        [DataMember(Name = "state")]
        public MilestoneStates State { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}