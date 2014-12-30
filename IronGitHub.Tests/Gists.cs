using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using IronGitHub.Tests.Helpers;
using System.Collections.Generic;
using IronGitHub.Entities;

namespace IronGitHub.Tests
{
    [TestClass]
    public class Gists : AuthenticationBoilerPlate
    {
        [TestMethod]
        public void Should_Setup_GitHub_Api()
        {
            this.CreateGitHubApi();
            this.Api.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_Create_Gist_Without_Authentication()
        {
            Should_Setup_GitHub_Api();

            var files = new Dictionary<string, string>
            {
                {"theAnswer", "42"},
                {"the Question", "I dunno"}
            };

            var gist = Api.Gists.New(files).GetAwaiter().GetResult();
            gist.Id.Should().NotBe(null);
            gist.Files.Count.Should().Be(2);
        }

        [TestMethod]
        public void Should_Create_Then_Delete_User_Gist()
        {
            Should_Setup_GitHub_Api();

            var files = new Dictionary<string, string>
            {
                {"theAnswer", "42"},
                {"the Question", "I dunno"}
            };

            this.Authorize(new[] { Scopes.Gist });

            var createdGists = Api.Gists.New(files).GetAwaiter().GetResult();
            
            Api.Gists.Delete(createdGists).GetAwaiter().GetResult();
        
        }

        [TestMethod]
        public void Should_Get_Gist_With_Legacy_ID()
        {
            Should_Setup_GitHub_Api();

            var gistId = "6287413";
            var gist = Api.Gists.Get(gistId).GetAwaiter().GetResult();

            gist.Id.Should().Be(gistId);
            gist.Description.Should().Be("This gist has a description!");
            gist.Public.Should().BeTrue();

            // TODO: For some reason UTC DateTimes are not being compared correctly. Fix this
            gist.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 08, 20, 21, 13, 36, DateTimeKind.Utc));
            gist.UpdatedAt.ToUniversalTime().Should().BeAfter(gist.CreatedAt);

            gist.Comments.Should().BeGreaterOrEqualTo(1);

            // URLs
            gist.CommentsUrl.Should().Be("https://api.github.com/gists/6287413/comments");
            gist.CommitsUrl.Should().Be("https://api.github.com/gists/6287413/commits");
            gist.ForksUrl.Should().Be("https://api.github.com/gists/6287413/forks");
            gist.GitPushUrl.Should().Be("https://gist.github.com/6287413.git");
            gist.GitPullUrl.Should().Be("https://gist.github.com/6287413.git");
            gist.HtmlUrl.Should().Be("https://gist.github.com/6287413");

            // Files
            gist.Files.Should().HaveCount(2);
            gist.Files.Should().ContainKeys("code.rb", "file.txt");
            gist.Files["file.txt"].Content.Should().Be("It has text too!");
            gist.Files["file.txt"].Size.Should().Be(16);
            gist.Files["file.txt"].RawUrl.Should().Be("https://gist.githubusercontent.com/johnduhart/6287413/raw/d598c68a9b50654d1242f0e0881728f64d97d85c/file.txt");
            gist.Files["code.rb"].Filename.Should().Be("code.rb");
            gist.Files["code.rb"].Type.Should().Be("application/x-ruby");
            gist.Files["code.rb"].Language.Should().Be("Ruby");

            // User (this was changed in API, check the owner object instead.)
            gist.User.Should().BeNull();
            gist.Owner.Should().NotBeNull();
            gist.Owner.Type.Should().Be("User");
            gist.Owner.Login.Should().Be("johnduhart");
            gist.Owner.Id.Should().Be(113642);

            // Revisions
            gist.History.Should().HaveCount(2);

            var firstRevision = gist.History.Last();
            firstRevision.User.Should().BeNull(); // As of 2013-08-20 the api is returning null. It might be a bug
            firstRevision.Version.Should().Be("c5bdde037729f9b704b4e986f13cbe3a23f96bf7");
            firstRevision.Url.Should().Be("https://api.github.com/gists/6287413/c5bdde037729f9b704b4e986f13cbe3a23f96bf7");
            firstRevision.ChangeStatus.Total.Should().Be(2);
            firstRevision.ChangeStatus.Additions.Should().Be(2);
            firstRevision.ChangeStatus.Deletions.Should().Be(0);

        }

        [TestMethod]
        public void Should_Get_Gist_By_ID()
        {
            Should_Setup_GitHub_Api();

            var gistId = "72ad0b833911ee7fdafe";
            var gist = Api.Gists.Get(gistId).GetAwaiter().GetResult();

            gist.Id.Should().Be(gistId);
            gist.Description.Should().Be("Testing Alphanumeric Gist ID");
            gist.Public.Should().BeTrue();
            gist.Comments.Should().BeGreaterOrEqualTo(1);

            // URLs
            gist.CommentsUrl.Should().Be("https://api.github.com/gists/72ad0b833911ee7fdafe/comments");
            gist.CommitsUrl.Should().Be("https://api.github.com/gists/72ad0b833911ee7fdafe/commits");
            gist.ForksUrl.Should().Be("https://api.github.com/gists/72ad0b833911ee7fdafe/forks");
            gist.GitPushUrl.Should().Be("https://gist.github.com/72ad0b833911ee7fdafe.git");
            gist.GitPullUrl.Should().Be("https://gist.github.com/72ad0b833911ee7fdafe.git");
            gist.HtmlUrl.Should().Be("https://gist.github.com/72ad0b833911ee7fdafe");

            // Files
            gist.Files.Should().HaveCount(1);
            gist.Files.Should().ContainKeys("Charge.js");
            gist.Files["Charge.js"].RawUrl.Should().Be("https://gist.githubusercontent.com/erik5388/72ad0b833911ee7fdafe/raw/49e7cf6ab9a7b375eb9843ff6607cc4df3ab7135/Charge.js");

            // User
            gist.User.Should().BeNull();
            gist.Owner.Should().NotBeNull();
            gist.Owner.Type.Should().Be("User");
            gist.Owner.Login.Should().Be("erik5388");
            gist.Owner.Id.Should().Be(183147);

            // Revisions
            gist.History.Should().HaveCount(2);
            var firstRevision = gist.History.Last();
            firstRevision.Version.Should().Be("57a4103e0d38945654fa252352e934f7e0eb7690");
            firstRevision.Url.Should().Be("https://api.github.com/gists/72ad0b833911ee7fdafe/57a4103e0d38945654fa252352e934f7e0eb7690");
        }

        [TestMethod]
        public void Should_Create_And_Update_Gist()
        {
            Should_Setup_GitHub_Api();

            var files = new Dictionary<string, string>
            {
                {"theAnswer", "42"},
                {"the Question", "I dunno"}
            };

            this.Authorize(new[] { Scopes.Gist });

            var gist = Api.Gists.New(files).GetAwaiter().GetResult();
            var patch = new Gist.EditGistPost(gist);
            var patchFile = patch.Files["theAnswer"];

            patchFile.Filename = "theWrongAnswer";
            patchFile.Content = "43";
            patch.Files["the Question"] = null;
            var patchedGist = Api.Gists.Edit(patch).GetAwaiter().GetResult();

            patchedGist.Files.Should().HaveCount(1);
            patchedGist.Files["theWrongAnswer"].Should().NotBeNull();
            patchedGist.Files["theWrongAnswer"].Content.Should().Be("43");
            patchedGist.Id.Should().Be(gist.Id);

            // Remove when done.
            Api.Gists.Delete(patchedGist).GetAwaiter().GetResult();

        }
    }
}
