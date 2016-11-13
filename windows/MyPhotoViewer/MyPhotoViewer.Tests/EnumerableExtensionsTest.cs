using MyPhotoViewer.Core;
using NUnit.Framework;
using System.Linq;

namespace MyPhotoViewer.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTest
    {
        [Test]
        public void Given_Enumerable_Then_SplitByConst_ExpectEnumerableOfEnumerable()
        {
            var elements = Enumerable.Range(1, 8);

            var groups = elements.Split(3).ToList();

            Assert.AreEqual(3, groups.Count);
            Assert.AreEqual(2, groups[2].Count());
        }
    }
}
