using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IronGitHub.Entities
{
    public class User
    {
        [DataMember][JsonProperty("login")]
        public string Login { get; set; }

        [DataMember][JsonProperty("id")]
        public long Id { get; set; }
        
        [DataMember][JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        
        [DataMember][JsonProperty("gravatar_id")]
        public string GravatarId { get; set; }
        
        [DataMember][JsonProperty("url")]
        public string Url { get; set; }
        
        [DataMember][JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
        
        [DataMember][JsonProperty("followers_url")]
        public string FollowersUrl { get; set; }
        
        [DataMember][JsonProperty("following_url")]
        public string FollowingUrl { get; set; }
        
        [DataMember][JsonProperty("gists_url")]
        public string GistsUrl { get; set; }
        
        [DataMember][JsonProperty("starred_url")]
        public string StarredUrl { get; set; }
        
        [DataMember][JsonProperty("subscriptions_url")]
        public string SubscriptionsUrl { get; set; }
        
        [DataMember][JsonProperty("organizations_url")]
        public string OrganizationsUrl { get; set; }
        
        [DataMember][JsonProperty("repos_url")]
        public string ReposUrl { get; set; }
        
        [DataMember][JsonProperty("events_url")]
        public string EventsUrl { get; set; }
        
        [DataMember][JsonProperty("received_events_url")]
        public string ReceivedEventsUrl { get; set; }
        
        [DataMember][JsonProperty("type")]
        public string GitHubType { get; set; }
        
        [DataMember][JsonProperty("name")]
        public string Name { get; set; }
        
        [DataMember][JsonProperty("company")]
        public string Company { get; set; }
        
        [DataMember][JsonProperty("blog")]
        public string Blog { get; set; }
        
        [DataMember][JsonProperty("location")]
        public string Location { get; set; }
        
        [DataMember][JsonProperty("email")]
        public string Email { get; set; }
        
        [DataMember][JsonProperty("hireable")]
        public bool Hireable { get; set; }
        
        [DataMember][JsonProperty("bio")]
        public string Bio { get; set; }
        
        [DataMember][JsonProperty("public_repos")]
        public int PublicRepos { get; set; }
        
        [DataMember][JsonProperty("followers")]
        public int Followers { get; set; }
        
        [DataMember][JsonProperty("following")]
        public int Following { get; set; }
        
        [DataMember][JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [DataMember][JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [DataMember][JsonProperty("public_gists")]
        public int PublicGists { get; set; }
    }
}