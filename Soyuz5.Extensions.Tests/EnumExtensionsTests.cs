using System;
using System.Linq;
using NUnit.Framework;

namespace Soyuz5.Extensions.Tests
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Flags]
        public enum MyFlags
        {
            None = 0,
            One = 1,
            Two = 2,
            Four = 4,
            All = One | Two | Four
        }

        [Test]
        public void GetFlags_None()
        {
            Assert.AreEqual(0, MyFlags.None.GetFlags().Count());
        }

        [Test]
        public void GetFlags_One()
        {
            Assert.AreEqual(1, MyFlags.One.GetFlags().Count());
            Assert.AreEqual(MyFlags.One, MyFlags.One.GetFlags().First());
        }

        [Test]
        public void GetFlags_One_Two()
        {
            MyFlags flags = (MyFlags.One | MyFlags.Two);
            Assert.AreEqual(2, flags.GetFlags().Count());
            Assert.AreEqual(MyFlags.One, flags.GetFlags().First());
            Assert.AreEqual(MyFlags.Two, flags.GetFlags().ElementAt(1));
        }

        [Test]
        public void GetFlags_All()
        {
            Assert.AreEqual(4, MyFlags.All.GetFlags().Count());
        }
    }
}