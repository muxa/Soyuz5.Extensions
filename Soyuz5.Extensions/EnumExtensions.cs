using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Tries to parse input string as Enum of specified type. If unsuccessful returns default value.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <param name="ignoreCase">Default false</param>
        /// <returns></returns>
        public static TEnum ParseEnum<TEnum>(this string input, TEnum defaultValue, bool ignoreCase = false)
             where TEnum : struct
        {
            TEnum value;
            if (Enum.TryParse<TEnum>(input, true, out value))
                return value;

            return defaultValue;
        }

        /// <summary>
        /// Gets an enumerable sequence of all values that are present in flags.
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetFlags(this Enum flags)
        {
            foreach (Enum value in Enum.GetValues(flags.GetType()))
            {
                if (Convert.ToUInt64(value) == 0)
                    continue;

                if (flags.HasFlag(value))
                    yield return value;
            }
        }
/*
        /// <summary>
        /// Gets an enumerable sequence of all values that are present in flags.
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum flags)
        {
            ulong intFlags = Convert.ToUInt64(flags);
            foreach (TEnum value in Enum.GetValues(flags.GetType()))
            {
                ulong intValue = Convert.ToUInt64(value);
                if (intValue == 0)
                    continue;

                if ((intFlags & intValue) == intValue)
                    yield return value;
            }
        }*/

        /// <summary>
        /// Gets Description attribute value for an enum value if available. Otherwise returns enum name. 
        /// If value is flags will returns a list of flags (will use description if set via DescriptionAttribute) that are set in value. 
        /// If is not a standard value and not flags will find ine matchin value and returns its desription (in available). 
        /// If no match - will return empty string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            Type enumType = value.GetType();
            string valueName = Enum.GetName(enumType, value);
            if (valueName == null)
            {
                // non-standard or multiple values
                return string.Join(", ", value.GetFlags().Select(f => f.GetEnumDescription()).ToArray());
            }
            MemberInfo[] members = enumType.GetMember(valueName);
            if (members.Length > 0)
            {
                DescriptionAttribute attr = (DescriptionAttribute)Attribute.GetCustomAttribute(members[0], typeof(DescriptionAttribute));
                if (attr != null && !string.IsNullOrEmpty(attr.Description))
                    return attr.Description;
            }
            return Enum.GetName(enumType, value);
        }

        /// <summary>
        /// Gets custom attribute for an enum value if available. Otherwise returns null. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TAttribute GetEnumAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            Type enumType = value.GetType();
            string valueName = Enum.GetName(enumType, value);
            if (valueName == null)
            {
                // non-standard or multiple values
                return null;
            }
            MemberInfo[] members = enumType.GetMember(valueName);
            if (members.Length > 0)
            {
                return (TAttribute)Attribute.GetCustomAttribute(members[0], typeof(TAttribute));
            }
            return null;
        }

        /// <summary>
        /// Returns list of value/name pairs for all available enum values. 
        /// Key contains an integer representation of enum value and value is the name or description (set via DescriptionAttribute)
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="useDescription">If true will use value desription if available</param>
        /// <returns></returns>
        public static IList<KeyValuePair<int, string>> GetEnumBindableList(this Type enumType, bool useDescription = true)
        {
            if (!enumType.IsEnum)
                throw new ArgumentOutOfRangeException("enumType", "Must be Enum");

            var list = new List<KeyValuePair<int, string>>();
            foreach (Enum value in Enum.GetValues(enumType))
            {
                if (useDescription)
                {
                    list.Add(new KeyValuePair<int, string>(Convert.ToInt32(value), value.GetEnumDescription()));
                }
                else
                {
                    list.Add(new KeyValuePair<int, string>(Convert.ToInt32(value), Enum.GetName(enumType, value)));
                }
            }
            return list;
        }

        /// <summary>
        /// Returns list of value/name pairs for all available enum values. 
        /// Key contains enum value casted to T and value is the name or description (set via DescriptionAttribute). 
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="useDescription">If true will use value desription if available</param>
        /// <returns></returns>
        public static IList<KeyValuePair<T, string>> GetEnumBindableList<T>(this Type enumType, bool useDescription = true)
            where T : struct
        {
            if (!enumType.IsEnum)
                throw new ArgumentOutOfRangeException("enumType", "Must be Enum");

            var list = new List<KeyValuePair<T, string>>();
            foreach (Enum value in Enum.GetValues(enumType))
            {

                if (useDescription)
                {
                    list.Add(new KeyValuePair<T, string>((T)(object)value, value.GetEnumDescription()));
                }
                else
                {
                    list.Add(new KeyValuePair<T, string>((T)(object)value, Enum.GetName(enumType, value)));
                }
            }
            return list;
        }

        /// <summary>
        /// Returns a sequence of enum values sorted by the custom attributes that define sorting order attached to enum values. 
        /// Custom attribute must implement <see cref="IComparable"/>. 
        /// If no sort attribute is attached to an enum values, those values will appear on top of the list and will be sorted by ther values.
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IEnumerable<T> SortEnums<T, TSortAttribute>(this Type enumType)
            where T : struct
            where TSortAttribute : Attribute, IComparable
        {
            if (!enumType.IsEnum)
                throw new ArgumentOutOfRangeException("enumType", "Must be Enum");

            var tempList = new List<Tuple<T, TSortAttribute>>();
            foreach (Enum value in Enum.GetValues(enumType))
            {
                var item = new Tuple<T, TSortAttribute>(
                    (T)(object)value,
                    value.GetEnumAttribute<TSortAttribute>());
                tempList.Add(item);
            }
            tempList.Sort((a, b) =>
            {
                if (a.Item2 == null && b.Item2 == null)
                    return ((Enum)(object)a.Item1).CompareTo(b.Item1);
                if (a.Item2 == null)
                    return -1;
                if (b.Item2 == null)
                    return 1;
                return a.Item2.CompareTo(b.Item2);
            });
            return tempList.Select(x => x.Item1).ToArray();
        }

        /// <summary>
        /// Returns a sequence of enum values sorted by the comparer. 
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="comparison"> </param>
        /// <returns></returns>
        public static IEnumerable<T> SortEnums<T>(this Type enumType, Comparison<T> comparison)
            where T : struct
        {
            if (!enumType.IsEnum)
                throw new ArgumentOutOfRangeException("enumType", "Must be Enum");

            var tempList = new List<T>();
            foreach (T value in Enum.GetValues(enumType))
            {
                tempList.Add(value);
            }
            tempList.Sort(comparison);
            return tempList.ToArray();
        }

        /// <summary>
        /// Returns list of value/name pairs for all available enum values. 
        /// Key contains enum value casted to T and value is the name or description (set via DescriptionAttribute). 
        /// Items are sorted by the custom attributes that define sorting order attached to enum values. 
        /// Custom attribute must implement <see cref="IComparable"/>. 
        /// If no sort attribute is attached to an enum values, those values will appear on top of the list and will be sorted by ther values.
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="useDescription">If true will use value desription if available</param>
        /// <returns></returns>
        public static IList<KeyValuePair<T, string>> GetEnumBindableList<T,TSortAttribute>(this Type enumType, bool useDescription = true)
            where T : struct
            where TSortAttribute : Attribute, IComparable
        {
            return
                enumType.SortEnums<T, TSortAttribute>().Select(
                    x => new KeyValuePair<T, string>(x,
                                                     useDescription
                                                         ? ((Enum) (object) x).GetEnumDescription()
                                                         : Enum.GetName(enumType, x))).ToList();
        }

        /// <summary>
        /// Returns list of value/name pairs for all available enum values. 
        /// Key contains enum value casted to T and value is the name or description (set via DescriptionAttribute). 
        /// Items are sorted by the comparer 
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="comparison"> </param>
        /// <param name="useDescription">If true will use value desription if available</param>
        /// <returns></returns>
        public static IList<KeyValuePair<T, string>> GetEnumBindableList<T>(this Type enumType, Comparison<T> comparison,
            bool useDescription = true)
            where T : struct
        {
            return
                enumType.SortEnums<T>(comparison).Select(
                    x => new KeyValuePair<T, string>(x,
                                                     useDescription
                                                         ? ((Enum) (object) x).GetEnumDescription()
                                                         : Enum.GetName(enumType, x))).ToList();
        }

        /// <summary>
        /// Count number of flags in enum. 
        /// A more readable version could be written based on http://www.dotnetperls.com/bitcount http://gurmeet.net/puzzles/fast-bit-counting-routines/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static UInt32 CountFlags<T>(this T enumValue)
            where T : struct
        {
            UInt32 v = Convert.ToUInt32(enumValue);
            v = v - ((v >> 1) & 0x55555555); // reuse input as temporary
            v = (v & 0x33333333) + ((v >> 2) & 0x33333333); // temp
            UInt32 c = ((v + (v >> 4) & 0xF0F0F0F) * 0x1010101) >> 24; // count
            return c;
        }
    }
}