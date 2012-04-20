namespace System
{
    public static class ConversionExtensions
    {
        public static int? NullIfZero(this int value)
        {
            if (value == 0) return null;

            return value;
        }
        
        /// <summary>
        /// Tries to parse input string as int. If unsuccessful returns default value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int Parse(this string value, int defaultValue)
        {
            int result;

            if (int.TryParse(value, out result))
                return result;

            return defaultValue;
        } 
    }
}