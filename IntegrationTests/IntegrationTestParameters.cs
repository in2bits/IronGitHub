using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace IntegrationTests
{
    class IntegrationTestParameters
    {
        private static string _username;
        private static string _password;

        public static string GitHubUsername
        {
            get
            {
                EnsurePopulated();
                return _username;
            }
        }

        public static string GitHubPassword
        {
            get
            {
                EnsurePopulated();
                return _password;
            }
        }

        private static void EnsurePopulated()
        {
            if (!string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password))
            {
                return;
            }

            // Attempt to load in account information from a file
            if (!File.Exists("testaccount.json"))
            {
                throw new Exception("Create the testaccount.json file before running tessts");
            }

            Dictionary<string, string> account;
            using (var fs = File.Open("testaccount.json", FileMode.Open))
            {
                account = JsonSerializer.DeserializeFromStream<Dictionary<string,string>>(fs);
            }

            _username = account["username"];
            _password = account["password"];
        }

        [DataContract]
        private class TestAccount
        {
            [DataMember(Name = "username")]
            public string Username { get; set; }

            [DataMember(Name = "password")]
            public string Password { get; set; }
        }
    }
}
