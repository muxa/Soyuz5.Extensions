using System;
using NUnit.Framework;

namespace Soyuz5.Extensions.Tests
{
    [TestFixture]
    public class MiscExtensionsTests
    {
        #region ChangedValue

        [Test]
        public void ChangedValue_string_changed()
        {
            int tracking = 0;
            Assert.AreEqual("234", "234".ChangedValue("123", ref tracking));
            Assert.AreEqual(1, tracking);
        }

        [Test]
        public void ChangedValue_string_unchanged()
        {
            int tracking = 0;
            Assert.AreEqual("123", "123".ChangedValue("123", ref tracking));
            Assert.AreEqual(0, tracking);
        }

        [Test]
        public void ChangedValue_string_nulls()
        {
            int tracking = 0;
            Assert.AreEqual(null, ((string) null).ChangedValue(null, ref tracking));
            Assert.AreEqual(0, tracking);
        }

        [Test]
        public void ChangedValue_string_nulls_and_empty()
        {
            int tracking = 0;
            Assert.AreEqual("", ((string) null).ChangedValue("", ref tracking));
            Assert.AreEqual(0, tracking);
            Assert.AreEqual(null, "".ChangedValue(null, ref tracking));
            Assert.AreEqual(0, tracking);
        }

        [Test]
        public void ChangedValue_int_changed()
        {
            int tracking = 0;
            Assert.AreEqual(234, 234.ChangedValue(123, ref tracking));
            Assert.AreEqual(1, tracking);
        }

        [Test]
        public void ChangedValue_int_unchanged()
        {
            int tracking = 0;
            Assert.AreEqual(123, 123.ChangedValue(123, ref tracking));
            Assert.AreEqual(0, tracking);
        }

        [Test]
        public void ChangedValue_nullable_int_changed()
        {
            int? v1 = 1;
            int? v2 = 2;
            int tracking = 0;
            Assert.AreEqual(v2, v2.ChangedValue(v1, ref tracking));
            Assert.AreEqual(1, tracking);

            v1 = null;
            v2 = 2;
            Assert.AreEqual(v2, v2.ChangedValue(v1, ref tracking));
            Assert.AreEqual(2, tracking);

            v1 = 1;
            v2 = null;
            Assert.AreEqual(null, v2.ChangedValue(v1, ref tracking));
            Assert.AreEqual(3, tracking);
        }

        [Test]
        public void ChangedValue_nullable_int_unchanged()
        {
            int? v1 = 1;
            int? v2 = 1;
            int tracking = 0;
            Assert.AreEqual(v2, v2.ChangedValue(v1, ref tracking));
            Assert.AreEqual(0, tracking);

            v1 = null;
            v2 = null;
            Assert.AreEqual(v2, v2.ChangedValue(v1, ref tracking));
            Assert.AreEqual(0, tracking);
        }

        #endregion

        #region SafeCastedCheck

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SafeCastedCheck_object_null()
        {
            object obj = null;
            obj.SafeCastedCheck<Object>(x => false);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SafeCastedCheck_predicate_null()
        {
            object obj = new Object();
            obj.SafeCastedCheck<Object>(null);
        }

        [Test]
        public void SafeCastedCheck_object_not_of_type()
        {
            object obj = new Exception();
            Assert.IsFalse(obj.SafeCastedCheck<ArgumentException>(x => false));
        }

        [Test]
        public void SafeCastedCheck_object_of_type_not_predicate()
        {
            object obj = new ArgumentException();
            Assert.IsFalse(obj.SafeCastedCheck<ArgumentException>(x => false));
        }

        [Test]
        public void SafeCastedCheck_object_of_type_predicate()
        {
            object obj = new ArgumentException();
            Assert.IsTrue(obj.SafeCastedCheck<ArgumentException>(x => true));
        }

        #endregion
    }
}