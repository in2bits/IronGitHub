using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using IronGitHub;
using ServiceStack.Text;
using Authorization = IronGitHub.Entities.Authorization;

namespace IntegrationTests
{
    public static class TestExtensions
    {
        public static bool Matches(this IEnumerable<Scopes> these, IEnumerable<Scopes> those)
        {
            these = these ?? Enumerable.Empty<Scopes>();
            those = those ?? Enumerable.Empty<Scopes>();

            var left = these as Scopes[] ?? these.ToArray();
            var right = those as Scopes[] ?? those.ToArray();
            
            if (left.Length != right.Length)
                return false;
            
            if (!left.All(right.Contains))
                return false;
            
            if (!right.All(left.Contains))
                return false;
            
            return true;
        }
    }
}
