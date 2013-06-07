namespace IronGitHub.Exceptions
{
    public class RateLimitExceededException : GitHubException
    {
        public RateLimitExceededException(string message) : base(message)
        {
        }
    }
}