using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            [Display(Description = "1", Order = 2)]
            [SortOrder(2)]
            One = 1,
            [System.ComponentModel.Description("2")]
            [SortOrder(1)]
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

        public class SortOrderAttribute : Attribute, IComparable
        {
            public SortOrderAttribute(int sortOrder)
            {
                SortOrder = sortOrder;
            }

            public int SortOrder { get; private set; }

            #region Implementation of IComparable

            public int CompareTo(object obj)
            {
                SortOrderAttribute other = obj as SortOrderAttribute;
                if (other == null)
                    return 0;

                return SortOrder.CompareTo(other.SortOrder);
            }

            #endregion
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
        public void GetEnumAttribute_none()
        {
            System.ComponentModel.DescriptionAttribute attr =
                MyFlags.One.GetEnumAttribute<System.ComponentModel.DescriptionAttribute>();
            Assert.IsNull(attr);

            System.ComponentModel.DataAnnotations.DisplayAttribute attr2 =
                MyFlags.Two.GetEnumAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
            Assert.IsNull(attr2);
        }

        [Test]
        public void GetEnumAttribute_some()
        {
            System.ComponentModel.DescriptionAttribute attr = MyFlags.Two.GetEnumAttribute<System.ComponentModel.DescriptionAttribute>();
            Assert.IsNotNull(attr);
            Assert.AreEqual("2", attr.Description);

            DisplayAttribute attr2 = MyFlags.One.GetEnumAttribute<DisplayAttribute>();
            Assert.IsNotNull(attr2);
            Assert.AreEqual("1", attr2.Description);
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
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetEnumBindableList_Generic_NotEnum()
        {
            IList<KeyValuePair<MyEnum, string>> list = typeof(int).GetEnumBindableList<MyEnum>();
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

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SortEnums_AttributeSorted_NotEnum()
        {
            IList<MyFlags> list = typeof (int).SortEnums<MyFlags, SortOrderAttribute>().ToList();
        }


        [Test]
        public void SortEnums_AttributeSorted()
        {
            IList<MyFlags> list = typeof(MyFlags).SortEnums<MyFlags, SortOrderAttribute>().ToList();

            /* resulted order should be

            None
            Four
            All
            Two
            One*/

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(MyFlags.None, list[0]);
            Assert.AreEqual(MyFlags.Four, list[1]);
            Assert.AreEqual(MyFlags.All, list[2]);
            Assert.AreEqual(MyFlags.Two, list[3]);
            Assert.AreEqual(MyFlags.One, list[4]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetEnumBindableList_Generic_AttributeSorted_NotEnum()
        {
            IList<KeyValuePair<MyEnum, string>> list = typeof(int).GetEnumBindableList<MyEnum, SortOrderAttribute>();
        }

        [Test]
        public void GetEnumBindableList_Generic_AttributeSorted()
        {
            IList<KeyValuePair<MyFlags, string>> list = typeof(MyFlags).GetEnumBindableList<MyFlags, SortOrderAttribute>();
            /* resulted order should be

            None
            Four
            All
            Two
            One*/

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(MyFlags.None, list[0].Key);
            Assert.AreEqual(MyFlags.Four, list[1].Key);
            Assert.AreEqual(MyFlags.All, list[2].Key);
            Assert.AreEqual(MyFlags.Two, list[3].Key);
            Assert.AreEqual(MyFlags.One, list[4].Key);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetEnumBindableList_Generic_Sorted_NotEnum()
        {
            IList<KeyValuePair<MyEnum, string>> list = typeof(int).GetEnumBindableList<MyEnum>((a,b) => 0);
        }


        [Test]
        public void GetEnumBindableList_Generic_Sorted()
        {
            IList<KeyValuePair<MyFlags, string>> list = typeof(MyFlags).GetEnumBindableList<MyFlags>(
                (a, b) =>
                    {
                        var attr1 = a.GetEnumAttribute<SortOrderAttribute>();
                        var attr2 = b.GetEnumAttribute<SortOrderAttribute>();
                        if (attr1 == null && attr2 == null)
                            return a.CompareTo(b);

                        if (attr1 == null)
                            return -1;
                            //return a.Key.CompareTo(attr2.SortOrder);

                        if (attr2 == null)
                            return 1;
                            //return attr1.SortOrder.CompareTo(b.Key);

                        return attr1.SortOrder.CompareTo(attr2.SortOrder);
                    });
            /* resulted order should be

            None
            Four
            All
            Two
            One*/

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(MyFlags.None, list[0].Key);
            Assert.AreEqual(MyFlags.Four, list[1].Key);
            Assert.AreEqual(MyFlags.All, list[2].Key);
            Assert.AreEqual(MyFlags.Two, list[3].Key);
            Assert.AreEqual(MyFlags.One, list[4].Key);
        }
    }
}