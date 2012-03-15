using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using NUnit.Framework;

namespace Soyuz5.Extensions.Tests
{
    /// <summary>
    /// Test for DynamicHelpersTests
    /// </summary>
    [TestFixture]
    public class DynamicHelpersTests
    {
        [Test]
        public void ToDictionary_dynamic()
        {
            dynamic o = new { a = 1, b = "2" };
            IDictionary<string, object> dictionary = DynamicHelper.ToDictionary(o);
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual(1, dictionary["a"]);
            Assert.AreEqual("2", dictionary["b"]);
        }

        [Test]
        public void ToDictionary_TestClass()
        {
            TestClass o = new TestClass() { a = 1, b = "2" };

            IDictionary<string, object> dictionary = DynamicHelper.ToDictionary(o);
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual(1, dictionary["a"]);
            Assert.AreEqual("2", dictionary["b"]);
        }

        [Test]
        public void ToDictionary_ExpandoObject()
        {
            dynamic o = new ExpandoObject();
            o.a = 1;
            o.b = "2";

            IDictionary<string, object> dictionary = DynamicHelper.ToDictionary(o);
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual(1, dictionary["a"]);
            Assert.AreEqual("2", dictionary["b"]);
        }

        [Test]
        public void ToDictionary_int()
        {
            IDictionary<string, object> dictionary = DynamicHelper.ToDictionary(1);
            Assert.AreEqual(0, dictionary.Count);
        }

        [Test]
        public void Enumerate_properties_of_dynamic_anonymous_type()
        {
            dynamic o = new { a = 1, b = "2" };
            int propertyCount = 0;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(o))
            {
                string s = string.Format("{0} = {1}", descriptor.Name, descriptor.GetValue(o));
                //Console.WriteLine("{0} = {1}", descriptor.Name, descriptor.GetValue(o));
                propertyCount++;
            }
            Assert.AreEqual(2, propertyCount);
        }

        [Test]
        public void Enumerate_properties_of_ExpandoObject()
        {
            dynamic o = new ExpandoObject();
            o.a = 1;
            o.b = "2";
            int propertyCount = 0;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(o))
            {
                string s = string.Format("{0} = {1}", descriptor.Name, descriptor.GetValue(o));
                //Console.WriteLine("{0} = {1}", descriptor.Name, descriptor.GetValue(o));
                propertyCount++;
            }
            Assert.AreEqual(0, propertyCount); // fail!
        }

        [Test]
        public void Enumerate_properties_of_TestClass()
        {
            TestClass o = new TestClass() { a = 1, b = "2" };
            int propertyCount = 0;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(o))
            {
                string s = string.Format("{0} = {1}", descriptor.Name, descriptor.GetValue(o));
                //Console.WriteLine("{0} = {1}", descriptor.Name, descriptor.GetValue(o));
                propertyCount++;
            }
            Assert.AreEqual(2, propertyCount);
        }

        [Test]
        public void Anonymous_type_without_explicit_property_name()
        {
            TestClass test = new TestClass() { a = 1, b = "2" };
            dynamic o = new { test.a, test.b };
            int propertyCount = 0;
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(o))
            {
                string s = string.Format("{0} = {1}", descriptor.Name, descriptor.GetValue(o));
                //Console.WriteLine("{0} = {1}", descriptor.Name, descriptor.GetValue(o));
                propertyCount++;
            }
            Assert.AreEqual(2, propertyCount);
        }

        class TestClass
        {
            public int a { get; set; }
            public string b { get; set; }
        }
    }
}