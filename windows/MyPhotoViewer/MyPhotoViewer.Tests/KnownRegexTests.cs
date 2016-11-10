using MyPhotoViewer.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyPhotoViewer.Tests
{
    [TestFixture]
    public class KnownRegexTests
    {
        private readonly Regex RegexPath = new Regex(KnownRegex.WindowsAbsoluteLocalFilePath);

        [Test]
        public void TestPathRegex()
        {
            var validPaths = new[] 
            {
                @"C:\file",
                @"C:\Dir\file",
                @"C:\Dir\Dir2\file",
                @"C:\Dir\Dir2\Dir3\file",

                @"C:\file.txt",
                @"C:\Dir\file.txt",
                @"C:\Dir\file-1.txt",
            };
            
            var invalidPaths = new[]
            {
                "",
                @"file",
                @"\",
                @"\file",
                @"C:",
                @"C:\",
                @"C:\Dir\",
                @"C:\Dir\Dir2\",
                @"C:\Dir\Dir2\Dir3\",
            };

            TestPathRegex(validPaths, true);
            TestPathRegex(invalidPaths, false);
        }

        private void TestPathRegex(IEnumerable<string> paths, bool isMatch)
        {
            foreach (string path in paths)
            {
                Assert.IsTrue(RegexPath.IsMatch(path) == isMatch);
            }
        }

        [Test]
        private void HelpTest()
        {
            Assert.IsTrue(new Regex(@"^(\\[a-zA-Z0-9.-]+)+$").IsMatch(@"\ff\a1.a\") == true);
        }
    }
}
