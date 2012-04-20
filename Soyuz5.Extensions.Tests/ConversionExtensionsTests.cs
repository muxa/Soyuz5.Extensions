using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace Soyuz5.Extensions.Tests
{
    /// <summary>
    /// Test for ConversionExtensions
    /// </summary>
    [TestFixture]
    public class ConversionExtensionsTests
    {
        #region Parse int

        [Test]
        public void Parse_int_value()
        {
            Assert.AreEqual(1, "1".Parse(0));
        }

        [Test]
        public void Parse_int_null()
        {
            Assert.AreEqual(1, ((string) null).Parse(1));
        }

        [Test]
        public void Parse_int_empty()
        {
            Assert.AreEqual(1, "".Parse(1));
        }

        [Test]
        public void Parse_int_invliad()
        {
            Assert.AreEqual(1, "qwe".Parse(1));
        }

        #endregion
    }
}