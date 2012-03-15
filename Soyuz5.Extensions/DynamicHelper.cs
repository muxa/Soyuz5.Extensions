using System.Collections.Generic;
using System.ComponentModel;

namespace System
{
    public static class DynamicHelper
    {
        /// <summary>
        /// Converts dynamic (or normal) object properties to a dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(object obj)
        {
            //if (obj is ExpandoObject) return (ExpandoObject)obj;
            if (obj is IDictionary<string, object>) return (IDictionary<string, object>)obj;

            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                result.Add(descriptor.Name, descriptor.GetValue(obj));
            }

            return result;
        }
    }
}