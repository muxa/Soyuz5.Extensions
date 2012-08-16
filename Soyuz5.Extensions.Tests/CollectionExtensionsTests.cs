using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Soyuz5.Extensions.Tests
{
    /// <summary>
    /// Test for CollectionExtensions
    /// </summary>
    [TestFixture]
    public class CollectionExtensionsTests
    {
        #region Remove

        [Test]
        public void Remove_with_predicate_empty_list()
        {
            List<int> list = new List<int>();

            list.Remove(i => false);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Remove_with_predicate_none()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Remove(i => false);

            Assert.AreEqual(4, list.Count);
        }

        [Test]
        public void Remove_with_predicate_all()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Remove(i => true);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Remove_with_predicate_first()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Remove(i => i == 1);

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(2, list[0]);
            Assert.AreEqual(3, list[1]);
            Assert.AreEqual(4, list[2]);
        }

        [Test]
        public void Remove_with_predicate_last()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Remove(i => i == 4);

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }

        [Test]
        public void Remove_with_predicate_middle()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Remove(i => i == 2 || i == 3);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(4, list[1]);
        }

        #endregion

        #region RemoveRange

        [Test]
        public void RemoveRange_empty_list()
        {
            IList<int> list = new List<int>();

            list.RemoveRange(0);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveRange_negative_index()
        {
            IList<int> list = new List<int>();

            list.RemoveRange(-1);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveRange_empty_list_1()
        {
            IList<int> list = new List<int>();

            list.RemoveRange(0, 1);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void RemoveRange_all_1()
        {
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveRange(0);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void RemoveRange_all_2()
        {
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveRange(0, 4);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void RemoveRange_first()
        {
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveRange(0, 1);

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(2, list[0]);
        }

        [Test]
        public void RemoveRange_last()
        {
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveRange(3, 1);

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(3, list.Last());
        }

        [Test]
        public void RemoveRange_middle()
        {
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveRange(1, 2);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(4, list[1]);
        }

        [Test]
        public void RemoveRange_0()
        {
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveRange(1, 0);

            Assert.AreEqual(4, list.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveRange_outside()
        {
            IList<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveRange(4, 1);

            Assert.AreEqual(4, list.Count);
        }

        #endregion
    }
}