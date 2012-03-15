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
    public class SerializationExtensionsTests
    {
        [DataContract]
        public class DataItem
        {
            [DataMember]
            public int Id { get; set; }

            [DataMember]
            public string Name { get; set; }
        }

        [Test]
        public void SerializeToJson_Object()
        {
            Assert.AreEqual("{\"Id\":1,\"Name\":\"Test\"}", new DataItem() { Id = 1, Name = "Test" }.SerializeToJson());
        }

        [Test]
        public void SerializeToJson_List()
        {
            IList<DataItem> list = new List<DataItem>();
            list.Add(new DataItem() { Id = 1, Name = "Test" });
            list.Add(new DataItem() { Id = 2, Name = "Test" });
            Assert.AreEqual("[{\"Id\":1,\"Name\":\"Test\"},{\"Id\":2,\"Name\":\"Test\"}]", list.SerializeToJson());
        }

        [Test]
        public void DerializeFromJson_Object()
        {
            DataItem dataItem = "{\"Id\":2,\"Name\":\"Testing\"}".DeserializeFromJson<DataItem>();
            Assert.AreEqual(2, dataItem.Id);
            Assert.AreEqual("Testing", dataItem.Name);
        }

        [Test]
        public void DerializeFromJson_List()
        {
            List<DataItem> data = "[{\"Id\":1,\"Name\":\"Test\"},{\"Id\":2,\"Name\":\"Test\"}]".DeserializeFromJson<List<DataItem>>();
            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(1, data[0].Id);
            Assert.AreEqual(2, data[1].Id);
        }

        [Test]
        public void DerializeFromJson_Array()
        {
            DataItem[] data = "[{\"Id\":1,\"Name\":\"Test\"},{\"Id\":2,\"Name\":\"Test\"}]".DeserializeFromJson<DataItem[]>();
            Assert.AreEqual(2, data.Length);
            Assert.AreEqual(1, data[0].Id);
            Assert.AreEqual(2, data[1].Id);
        }
    }
}