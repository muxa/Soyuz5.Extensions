using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace System
{
    public static class SerializationExtensions
    {
        /// <summary>
        /// Serializes object to XML.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Returns null if object is null.</returns>
        public static string SerializeToXml(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        /// <summary>
        /// Serializes object to Json using <see cref="DataContractAttribute"/> and <see cref="DataMemberAttribute"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Returns null if object is null.</returns>
        public static string SerializeToJson(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
                ser.WriteObject(stream, obj);
                stream.Position = 0;
                using (StreamReader sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserializes object from Json using <see cref="DataContractAttribute"/> and <see cref="DataMemberAttribute"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T DeserializeFromJson<T>(this string json)
        {
            if (json == null)
            {
                return default(T);
            }

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                stream.Position = 0;

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                T obj = (T)ser.ReadObject(stream);
                return obj;
            }
        }
    }
}