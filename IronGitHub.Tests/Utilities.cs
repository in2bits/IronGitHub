using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using IronGitHub.Entities;

namespace IronGitHub.Tests
{
    [TestClass]
    public class Utilities
    {
        [TestMethod]
        public void Should_Parse_Legacy_Gist_Url()
        {
            var urls = new[]
            {
                "https://api.github.com/gists/5731704",
                "https://gist.github.com/anonymous/5731704",
                "https://gist.github.com/raw/5731704/f70d7bba4ae1f07682e0358bd7a2068094fc023b/theAnswer",
                "https://gist.githubusercontent.com/anonymous/5731704/raw/f70d7bba4ae1f07682e0358bd7a2068094fc023b/theAnswer"
            };

            foreach (var url in urls)
            {
                Gist.ParseIdFromUrl(url).Should().Be("5731704");
            }
        }

        [TestMethod]
        public void Should_Parse_Gist_Url()
        {
            var urls = new[]
            {
                "https://api.github.com/gists/791775dde23be2736b5c",
                "https://gist.github.com/erik5388/791775dde23be2736b5c",
                "https://gist.github.com/raw/791775dde23be2736b5c/10500012fca9b4425b50de67a7258a12cba0c076/asd",
                "https://gist.githubusercontent.com/erik5388/791775dde23be2736b5c/raw/10500012fca9b4425b50de67a7258a12cba0c076/asd"
            };

            foreach (var url in urls)
            {
                Gist.ParseIdFromUrl(url).Should().Be("791775dde23be2736b5c");
            }
        }
    }
}
