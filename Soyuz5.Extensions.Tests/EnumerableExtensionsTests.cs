using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Soyuz5.Extensions.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void Yield()
        {
            object x = 10;
            Assert.AreEqual(1, x.Yield().Count());
            Assert.AreEqual(10, x.Yield().First());
        }

        [Test]
         public void Distinct()
        {
            int[] numbers = new[] {1, 2, 1, 3, 4, 5, 5, 3};

            Assert.AreEqual(5, numbers.Distinct((a, b) => a == b).Count());

            Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, numbers.Distinct((a, b) => a == b).ToArray());
        }
    }
}