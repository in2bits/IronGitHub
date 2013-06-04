using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using IronGitHub;
using ServiceStack.Text;
using Authorization = IronGitHub.Entities.Authorization;

namespace Tests
{
    public static class TestExtensions
    {
        async public static Task in2bitstest(this GitHubApi api, IEnumerable<Scopes> scopes = null, string note = null)
        {
            await TestAccount.Load("in2bitstest", api, scopes);
        }

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

    [DataContract]
    public class TestAccount
    {
        public TestAccount()
        {
            Authorizations = new List<Authorization>();
        }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "authorizations")]
        public IList<Authorization> Authorizations { get; set; }

        public NetworkCredential Credential { get { return new NetworkCredential(Name, Password); } }

        private static string GetFileName(string name)
        {
            return string.Format("testaccount.{0}.json", name);
        }

        async public static Task Load(string name, GitHubApi api, IEnumerable<Scopes> scopes = null, string note = null)
        {
            var account = Load("in2bitstest");
            var auth = account.Authorizations.FirstOrDefault(x => x.Scopes.Matches(scopes));
            if (auth == null)
            {
                auth = await api.Authorize(account.Credential, scopes, note);
                account.Authorizations.Add(auth);
                account.Save();
            }
            else
            {
                api.Authorize(auth);
            }
        }

        public static TestAccount Load(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("must be populated", "name");

            var fileName = GetFileName(name);
            TestAccount account;
            if (File.Exists(fileName))
            {
                using (var fs = File.Open(fileName, FileMode.Open))
                    account = JsonSerializer.DeserializeFromStream<TestAccount>(fs);
            }
            else
            {
                account = new TestAccount
                    {
                        Name = name
                    };
            }
            return account;
        }

        public void Save()
        {
            var fileName = GetFileName(Name);
            using (var fs = File.Open(fileName, FileMode.Create))
                JsonSerializer.SerializeToStream(this, fs);
        }
    }
}
