using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IronGitHub.Entities
{
    [DataContract]
    public class HookBase
    {
        [DataMember(Name = "name")]
        public HookName Name { get; set; }

        [DataMember(Name = "events")]
        public IEnumerable<SupportedEvents> Events { get; set; }

        [DataMember(Name = "active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// This is a hash of configuration settings to pass to GitHub.
        /// The sample for a "WebHook" is:
        ///   "config": {
        ///         "url": "http://example.com/webhook",
        ///         "content_type": "json"
        ///   }
        /// http://developer.github.com/v3/repos/hooks/
        /// 
        /// The actual scheme for each of the types of Hooks can be found here:
        /// https://api.github.com/hooks
        /// </summary>
        [DataMember(Name = "config")]
        public Dictionary<string, string> Config { get; set; }
    }

    [DataContract]
    public class Hook : HookBase
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataContract]
        public class PatchHook : HookBase
        {
            /// <summary>
            /// This is only used for Editing (PATCH) a hook
            /// </summary>
            [DataMember(Name = "add_events")]
            public IEnumerable<SupportedEvents> AddEvents { get; set; }

            /// <summary>
            /// This is only used for Editing (PATCH) a hook
            /// </summary>
            [DataMember(Name = "remove_events")]
            public IEnumerable<SupportedEvents> RemoveEvents { get; set; }
        }
    }

    /// <summary>
    /// This list was pulled from https://api.github.com/hooks and parsed with an F# TypeProvider
    /// Note: only the first letter has been capitalized for the sake of time/convenience. If
    /// you feel like cleaning them all up feel free.
    /// </summary>
    [DataContract]
    public enum HookName
    {
        [EnumMember(Value = "activecollab")]
        Activecollab,
        [EnumMember(Value = "acunote")]
        Acunote,
        [EnumMember(Value = "agilebench")]
        Agilebench,
        [EnumMember(Value = "agilezen")]
        Agilezen,
        [EnumMember(Value = "amazonsns")]
        Amazonsns,
        [EnumMember(Value = "apiary")]
        Apiary,
        [EnumMember(Value = "apoio")]
        Apoio,
        [EnumMember(Value = "appharbor")]
        Appharbor,
        [EnumMember(Value = "apropos")]
        Apropos,
        [EnumMember(Value = "asana")]
        Asana,
        [EnumMember(Value = "backlog")]
        Backlog,
        [EnumMember(Value = "bamboo")]
        Bamboo,
        [EnumMember(Value = "basecamp")]
        Basecamp,
        [EnumMember(Value = "bcx")]
        Bcx,
        [EnumMember(Value = "blimp")]
        Blimp,
        [EnumMember(Value = "boxcar")]
        Boxcar,
        [EnumMember(Value = "buddycloud")]
        Buddycloud,
        [EnumMember(Value = "bugherd")]
        Bugherd,
        [EnumMember(Value = "bugly")]
        Bugly,
        [EnumMember(Value = "bugzilla")]
        Bugzilla,
        [EnumMember(Value = "campfire")]
        Campfire,
        [EnumMember(Value = "cia")]
        Cia,
        [EnumMember(Value = "circleci")]
        Circleci,
        [EnumMember(Value = "codeclimate")]
        Codeclimate,
        [EnumMember(Value = "codeportingcsharp2java")]
        Codeportingcsharp2Java,
        [EnumMember(Value = "codeship")]
        Codeship,
        [EnumMember(Value = "coffeedocinfo")]
        Coffeedocinfo,
        [EnumMember(Value = "conductor")]
        Conductor,
        [EnumMember(Value = "coop")]
        Coop,
        [EnumMember(Value = "copperegg")]
        Copperegg,
        [EnumMember(Value = "cube")]
        Cube,
        [EnumMember(Value = "depending")]
        Depending,
        [EnumMember(Value = "deployhq")]
        Deployhq,
        [EnumMember(Value = "devaria")]
        Devaria,
        [EnumMember(Value = "docker")]
        Docker,
        [EnumMember(Value = "ducksboard")]
        Ducksboard,
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "firebase")]
        Firebase,
        [EnumMember(Value = "fisheye")]
        Fisheye,
        [EnumMember(Value = "flowdock")]
        Flowdock,
        [EnumMember(Value = "fogbugz")]
        Fogbugz,
        [EnumMember(Value = "freckle")]
        Freckle,
        [EnumMember(Value = "friendfeed")]
        Friendfeed,
        [EnumMember(Value = "gemini")]
        Gemini,
        [EnumMember(Value = "gemnasium")]
        Gemnasium,
        [EnumMember(Value = "geocommit")]
        Geocommit,
        [EnumMember(Value = "getlocalization")]
        Getlocalization,
        [EnumMember(Value = "gitlive")]
        Gitlive,
        [EnumMember(Value = "grmble")]
        Grmble,
        [EnumMember(Value = "grouptalent")]
        Grouptalent,
        [EnumMember(Value = "grove")]
        Grove,
        [EnumMember(Value = "habitualist")]
        Habitualist,
        [EnumMember(Value = "hakiri")]
        Hakiri,
        [EnumMember(Value = "hall")]
        Hall,
        [EnumMember(Value = "harvest")]
        Harvest,
        [EnumMember(Value = "hipchat")]
        Hipchat,
        [EnumMember(Value = "hostedgraphite")]
        Hostedgraphite,
        [EnumMember(Value = "hubcap")]
        Hubcap,
        [EnumMember(Value = "hubci")]
        Hubci,
        [EnumMember(Value = "humbug")]
        Humbug,
        [EnumMember(Value = "icescrum")]
        Icescrum,
        [EnumMember(Value = "irc")]
        Irc,
        [EnumMember(Value = "irker")]
        Irker,
        [EnumMember(Value = "ironmq")]
        Ironmq,
        [EnumMember(Value = "jabber")]
        Jabber,
        [EnumMember(Value = "jaconda")]
        Jaconda,
        [EnumMember(Value = "jeapie")]
        Jeapie,
        [EnumMember(Value = "jenkins")]
        Jenkins,
        [EnumMember(Value = "jenkinsgit")]
        Jenkinsgit,
        [EnumMember(Value = "jira")]
        Jira,
        [EnumMember(Value = "jqueryplugins")]
        Jqueryplugins,
        [EnumMember(Value = "kanbanery")]
        Kanbanery,
        [EnumMember(Value = "kickoff")]
        Kickoff,
        [EnumMember(Value = "leanto")]
        Leanto,
        [EnumMember(Value = "lechat")]
        Lechat,
        [EnumMember(Value = "lighthouse")]
        Lighthouse,
        [EnumMember(Value = "lingohub")]
        Lingohub,
        [EnumMember(Value = "loggly")]
        Loggly,
        [EnumMember(Value = "mantisbt")]
        Mantisbt,
        [EnumMember(Value = "masterbranch")]
        Masterbranch,
        [EnumMember(Value = "mqttpub")]
        Mqttpub,
        [EnumMember(Value = "nma")]
        Nma,
        [EnumMember(Value = "nodejitsu")]
        Nodejitsu,
        [EnumMember(Value = "notifo")]
        Notifo,
        [EnumMember(Value = "ontime")]
        Ontime,
        [EnumMember(Value = "pachube")]
        Pachube,
        [EnumMember(Value = "packagist")]
        Packagist,
        [EnumMember(Value = "phraseapp")]
        Phraseapp,
        [EnumMember(Value = "pivotaltracker")]
        Pivotaltracker,
        [EnumMember(Value = "planbox")]
        Planbox,
        [EnumMember(Value = "planio")]
        Planio,
        [EnumMember(Value = "prowl")]
        Prowl,
        [EnumMember(Value = "puppetlinter")]
        Puppetlinter,
        [EnumMember(Value = "pushalot")]
        Pushalot,
        [EnumMember(Value = "pushover")]
        Pushover,
        [EnumMember(Value = "pythonpackages")]
        Pythonpackages,
        [EnumMember(Value = "railsbp")]
        Railsbp,
        [EnumMember(Value = "railsbrakeman")]
        Railsbrakeman,
        [EnumMember(Value = "rally")]
        Rally,
        [EnumMember(Value = "rapidpush")]
        Rapidpush,
        [EnumMember(Value = "rationaljazzhub")]
        Rationaljazzhub,
        [EnumMember(Value = "rationalteamconcert")]
        Rationalteamconcert,
        [EnumMember(Value = "rdocinfo")]
        Rdocinfo,
        [EnumMember(Value = "readthedocs")]
        Readthedocs,
        [EnumMember(Value = "redmine")]
        Redmine,
        [EnumMember(Value = "rubyforge")]
        Rubyforge,
        [EnumMember(Value = "scrumdo")]
        Scrumdo,
        [EnumMember(Value = "shiningpanda")]
        Shiningpanda,
        [EnumMember(Value = "sifter")]
        Sifter,
        [EnumMember(Value = "simperium")]
        Simperium,
        [EnumMember(Value = "slatebox")]
        Slatebox,
        [EnumMember(Value = "snowyevening")]
        Snowyevening,
        [EnumMember(Value = "socialcast")]
        Socialcast,
        [EnumMember(Value = "softlayermessaging")]
        Softlayermessaging,
        [EnumMember(Value = "sourcemint")]
        Sourcemint,
        [EnumMember(Value = "splendidbacon")]
        Splendidbacon,
        [EnumMember(Value = "sprintly")]
        Sprintly,
        [EnumMember(Value = "sqsqueue")]
        Sqsqueue,
        [EnumMember(Value = "stackmob")]
        Stackmob,
        [EnumMember(Value = "statusnet")]
        Statusnet,
        [EnumMember(Value = "talker")]
        Talker,
        [EnumMember(Value = "targetprocess")]
        Targetprocess,
        [EnumMember(Value = "tddium")]
        Tddium,
        [EnumMember(Value = "teamcity")]
        Teamcity,
        [EnumMember(Value = "tender")]
        Tender,
        [EnumMember(Value = "tenxer")]
        Tenxer,
        [EnumMember(Value = "testpilot")]
        Testpilot,
        [EnumMember(Value = "toggl")]
        Toggl,
        [EnumMember(Value = "trac")]
        Trac,
        [EnumMember(Value = "trajectory")]
        Trajectory,
        [EnumMember(Value = "travis")]
        Travis,
        [EnumMember(Value = "trello")]
        Trello,
        [EnumMember(Value = "twilio")]
        Twilio,
        [EnumMember(Value = "twitter")]
        Twitter,
        [EnumMember(Value = "unfuddle")]
        Unfuddle,
        /// <summary>
        /// This is the default hook
        /// </summary>
        [EnumMember(Value = "web")]
        Web,
        [EnumMember(Value = "weblate")]
        Weblate,
        [EnumMember(Value = "webtranslateit")]
        Webtranslateit,
        [EnumMember(Value = "yammer")]
        Yammer,
        [EnumMember(Value = "youtrack")]
        Youtrack,
        [EnumMember(Value = "zendesk")]
        Zendesk,
        [EnumMember(Value = "zohoprojects")]
        Zohoprojects,
    }
}