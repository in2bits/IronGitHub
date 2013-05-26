using System;

namespace IronGitHub.Exceptions
{
    public class GitHubException : Exception
    {
        public GitHubException(string message) : base(message)
        {
            
        }

        public GitHubException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
