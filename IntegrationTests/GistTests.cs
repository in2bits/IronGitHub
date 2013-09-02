﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IronGitHub;
using IronGitHub.Entities;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    public class GistTests : WithGitHubApi
    {
        [Test]
        async public Task CreateNewAnonymousGist()
        {
            var files = new Dictionary<string, string>
                {
                    {"theAnswer", "42"},
                    {"the Question", "I dunno"}
                };

            var gist = await Api.Gists.New(files);

            gist.Id.Should().NotBe(null);
            gist.Files.Count.Should().Be(2);
        }

        [Test]
        [Category("Authenticated")]
        async public Task CreateAndDeleteUserGist()
        {
            var files = new Dictionary<string, string>
                {
                    {"theAnswer", "42"},
                    {"the Question", "I dunno"}
                };
            await Authorize(new[] { Scopes.Gist });
            
            var gist = await Api.Gists.New(files);
            await Api.Gists.Delete(gist);
        }

        [Test]
        async public Task GetGist()
        {
            const long gistId = 6287413;

            var gist = await Api.Gists.Get(gistId);

            gist.Id.Should().Be(gistId);
            gist.Description.Should().Be("This gist has a description!");
            gist.Public.Should().BeTrue();
            // TODO: For some reason UTC DateTimes are not being compared correctly. Fix this
            gist.CreatedAt.ToUniversalTime().Should().Be(new DateTime(2013, 08, 20, 21, 13, 36, DateTimeKind.Utc));
            gist.UpdatedAt.ToUniversalTime().Should().BeAfter(gist.CreatedAt);
            gist.Comments.Should().BeGreaterOrEqualTo(1); // Somebody will eventually be a smartass and comment on the gist.

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
            gist.Files["file.txt"].RawUrl.Should()
                .Be("https://gist.github.com/raw/6287413/d598c68a9b50654d1242f0e0881728f64d97d85c/file.txt");
            gist.Files["code.rb"].Filename.Should().Be("code.rb");
            gist.Files["code.rb"].Type.Should().Be("application/ruby");
            gist.Files["code.rb"].Language.Should().Be("Ruby");

            // User
            gist.User.Should().NotBeNull();
            gist.User.Login.Should().Be("johnduhart");

            // Revisions
            gist.History.Should().HaveCount(2);
            var firstRevision = gist.History.Last();
            firstRevision.User.Should().BeNull(); // As of 2013-08-20 the api is returning null. It might be a bug
            firstRevision.Version.Should().Be("c5bdde037729f9b704b4e986f13cbe3a23f96bf7");
            firstRevision.Url.Should()
                .Be("https://api.github.com/gists/6287413/c5bdde037729f9b704b4e986f13cbe3a23f96bf7");
            firstRevision.ChangeStatus.Total.Should().Be(2);
            firstRevision.ChangeStatus.Additions.Should().Be(2);
            firstRevision.ChangeStatus.Deletions.Should().Be(0);
        }

        [Test]
        [Category("Authenticated")]
        async public Task PatchGist()
        {
            var files = new Dictionary<string, string>
                {
                    {"theAnswer", "42"},
                    {"the Question", "I dunno"}
                };
            await Authorize(new[] { Scopes.Gist });
            var gist = await Api.Gists.New(files);

            var patch = new Gist.EditGistPost(gist);
            var patchFile = patch.Files["theAnswer"];
            patchFile.Filename = "theWrongAnswer";
            patchFile.Content = "43";
            patch.Files["the Question"] = null;
            var patchedGist = await Api.Gists.Edit(patch);

            patchedGist.Files.Should().HaveCount(1);
            patchedGist.Files["theWrongAnswer"].Should().NotBeNull();
            patchedGist.Files["theWrongAnswer"].Content.Should().Be("43");
            patchedGist.Id.Should().Be(gist.Id);
        }
    }
}
