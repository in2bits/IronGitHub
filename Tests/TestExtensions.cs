using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IronGitHub;
using Newtonsoft.Json;
using Authorization = IronGitHub.Entities.Authorization;

namespace Tests
{
    public static class TestExtensions
    {
        async public static Task Account01(this GitHubApi api, IEnumerable<Scopes> scopes = null, string note = null)
        {
            var accountNumber = "01";
            var oauthFile = string.Format("test.oauth.{0}.txt", accountNumber);
            if (File.Exists(oauthFile))
            {
                var authorizationJson = File.ReadAllText(oauthFile);
                var auth = JsonConvert.DeserializeObject<Authorization>(authorizationJson);
                api.Authorize(auth);
                return;
            }
            
            var credFile = string.Format("test.creds.{0}.txt", accountNumber);
            if (!File.Exists(credFile))
                throw new Exception(string.Format("Missing Test Credentials file {0}", credFile));

            var lines = File.ReadAllLines(credFile);
            if (lines.Length < 2)
                throw new FileLoadException("Credentials not found");

            var cred = new NetworkCredential(lines[0], lines[1]);
            await api.Authorize(cred, scopes, note);

            var authJson = JsonConvert.SerializeObject(api.Context.Authorization);
            File.WriteAllText(oauthFile, authJson);
        }

        public static string LoadToken(string file)
        {
            return File.ReadAllText(file);
        }
    }
}
