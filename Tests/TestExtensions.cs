using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public static class TestExtensions
    {
        public static NetworkCredential LoadCredentials(string file)
        {
            var lines = File.ReadAllLines(file);
            if (lines.Length < 2)
                throw new FileLoadException("Credentials not found");
            return new NetworkCredential(lines[0], lines[1]);
        }
    }
}
