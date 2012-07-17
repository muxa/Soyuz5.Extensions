using System;
using System.Collections.Generic;
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
            [System.ComponentModel.Description("2")]
            Two = 2,
            Four = 4,
            All = One | Two | Four
        }

        public enum MyEnum
        {
            None = 0,
            One = 1,
            [System.ComponentModel.Description("2")]
            Two = 2,
            Three = 3
        }

        [Test]
        public void GetFlags_NotFlags()
        {
            Assert.AreEqual(0, MyFlags.None.GetFlags().Count());

            foreach (var flag in MyFlags.None.GetFlags())
            {
                
            }
        }

        [Test]
        public void GetFlags_None()
        {
            Assert.AreEqual(0, MyEnum.None.GetFlags().Count());
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

        [Test]
        public void GetEnumDescription_Default()
        {
            Assert.AreEqual("One", MyFlags.One.GetEnumDescription());
        }

        [Test]
        public void GetEnumDescription_DescriptionAttribute()
        {
            Assert.AreEqual("2", MyFlags.Two.GetEnumDescription());
        }

        [Test]
        public void GetEnumDescription_Flags()
        {
            MyFlags flags = (MyFlags.One | MyFlags.Two);
            Assert.AreEqual("One, 2", flags.GetEnumDescription());
        }

        [Test]
        public void GetEnumDescription_NonStandard()
        {
            MyFlags flags = (MyFlags) 14;
            Assert.AreEqual("2, Four", flags.GetEnumDescription());
        }

        [Test]
        public void GetEnumDescription_NoFLags()
        {
            MyEnum value = (MyEnum)14;
            Assert.AreEqual("2", value.GetEnumDescription());

            value = (MyEnum)4;
            Assert.AreEqual("", value.GetEnumDescription());

            value = (MyEnum)3;
            Assert.AreEqual("Three", value.GetEnumDescription());
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetEnumBindableList_NotEnum()
        {
            IList<KeyValuePair<int, string>> list = typeof(int).GetEnumBindableList();
        }

        [Test]
        public void GetEnumBindableList()
        {
            IList<KeyValuePair<int, string>> list = typeof (MyEnum).GetEnumBindableList();
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("None", list[0].Value);
            Assert.AreEqual("One", list[1].Value);
            Assert.AreEqual("2", list[2].Value);
            Assert.AreEqual("Three", list[3].Value);
            Assert.AreEqual(0, list[0].Key);
            Assert.AreEqual(1, list[1].Key);
            Assert.AreEqual(2, list[2].Key);
            Assert.AreEqual(3, list[3].Key);
        }

        [Test]
        public void GetEnumBindableList_Generic()
        {
            IList<KeyValuePair<MyEnum, string>> list = typeof (MyEnum).GetEnumBindableList<MyEnum>();
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("None", list[0].Value);
            Assert.AreEqual("One", list[1].Value);
            Assert.AreEqual("2", list[2].Value);
            Assert.AreEqual("Three", list[3].Value);
            Assert.AreEqual(MyEnum.None, list[0].Key);
            Assert.AreEqual(MyEnum.One, list[1].Key);
            Assert.AreEqual(MyEnum.Two, list[2].Key);
            Assert.AreEqual(MyEnum.Three, list[3].Key);
        }
    }
}