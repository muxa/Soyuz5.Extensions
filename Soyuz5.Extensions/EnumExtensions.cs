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
    }
}