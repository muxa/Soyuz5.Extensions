using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DataContract]
        public class DataItem2
        {
            [DataMember(EmitDefaultValue = true, IsRequired = true)]
            public int IdRequired { get; set; }

            [DataMember(EmitDefaultValue = true, IsRequired = false)]
            public int IdOptional { get; set; }

            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public int IdOptional2 { get; set; }

            [DataMember(EmitDefaultValue = false, IsRequired = false)]
            public string Name { get; set; }
        }

        [Test]
        public void SerializeToJson_Object_options()
        {
            Assert.AreEqual("{\"IdOptional\":2,\"IdRequired\":1,\"Name\":\"Test\"}", new DataItem2() { IdRequired = 1, IdOptional = 2, Name = "Test" }.SerializeToJson());
            Assert.AreEqual("{\"IdOptional\":0,\"IdRequired\":0}", new DataItem2() {}.SerializeToJson());
        }

        [Test]
        [ExpectedException(typeof(System.Runtime.Serialization.SerializationException))]
        public void DerializeFromJson_Object_options_missing_required()
        {
            DataItem2 dataItem = "{\"Name\":\"Testing\"}".DeserializeFromJson<DataItem2>();
            Assert.AreEqual("Testing", dataItem.Name);
        }

        [Test]
        public void DerializeFromJson_Object_options()
        {
            DataItem2 dataItem = "{\"IdRequired\":1,\"Name\":\"Testing\"}".DeserializeFromJson<DataItem2>();
            Assert.AreEqual("Testing", dataItem.Name);
            Assert.AreEqual(1, dataItem.IdRequired);
            Assert.AreEqual(0, dataItem.IdOptional);
            Assert.AreEqual(0, dataItem.IdOptional2);
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

        [Test]
        public void SerializeToJson_Round_trip()
        {
            DataItem result = new DataItem() { Id = 1, Name = "Test" }.SerializeToJson().DeserializeFromJson<DataItem>();
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Test", result.Name);
        }

        [Test]
        public void SerializeToBinary_Round_trip()
        {
            DataItem result = new DataItem() { Id = 1, Name = "Test" }.SerializeToBinary().DeserializeFromBinary<DataItem>();
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Test", result.Name);

            Assert.AreEqual(230, result.SerializeToBinary().Length);
        }

        [Test]
        public void Binary_vs_Json_serializers()
        {
            DataItem item = new DataItem() { Id = 1, Name = "Test" };

            Assert.AreEqual(230, item.SerializeToBinary().Length);
            Assert.AreEqual(22, item.SerializeToJson().Length);
        }
    }
}