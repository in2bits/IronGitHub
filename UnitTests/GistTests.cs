using FluentAssertions;
using IronGitHub.Entities;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class GistUrlParseTests
    {
        [Datapoints]
        public string[] GistUrls = new[]
        {
            "https://api.github.com/gists/5731704",
            "https://gist.github.com/anonymous/5731704",
            "https://gist.github.com/raw/5731704/f70d7bba4ae1f07682e0358bd7a2068094fc023b/theAnswer"
        };

        [Theory]
        public void ParseUrl(string url)
        {
            Gist.ParseIdFromUrl(url).Should().Be("5731704");
        }
    }

    [TestFixture]
    public class GistAlphaNumericUrlParseTests
    {
        [Datapoints]
        public string[] GistUrls = new[]
        {
            "https://api.github.com/gists/72ad0b833911ee7fdafe",
            "https://gist.github.com/erik5388/72ad0b833911ee7fdafe",
            "https://gist.githubusercontent.com/erik5388/72ad0b833911ee7fdafe/raw/49e7cf6ab9a7b375eb9843ff6607cc4df3ab7135/Charge.js"
        };
        
        [Theory]
        public void ParseUrl(string url)
        {
            Gist.ParseIdFromUrl(url).Should().Be("72ad0b833911ee7fdafe");
        }
    }

}