namespace IronGitHub
{
    public class GitHubApiContext
    {
        public GitHubApiContext(Configuration configuration = null, Application application = null)
        {
            if (configuration == null)
                configuration = new Configuration();
            Configuration = configuration;
            Application = application;
        }

        public Configuration Configuration { get; private set; }
        public Application Application { get; private set; }

        public Authorization Authorization { get; set; }
    }
}