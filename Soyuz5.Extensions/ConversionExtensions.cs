﻿namespace System
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

        /// <summary>
        /// Tries to parse input string as int. If unsuccessful returns null.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ParseInt(this string value)
        {
            int result;

            if (int.TryParse(value, out result))
                return result;

            return null;
        }

        /// <summary>
        /// Tries to parse input string as decimal. If unsuccessful returns default value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal Parse(this string value, decimal defaultValue)
        {
            decimal result;

            if (decimal.TryParse(value, out result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Tries to parse input string as decimal. If unsuccessful returns null.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? ParseDecimal(this string value)
        {
            decimal result;

            if (decimal.TryParse(value, out result))
                return result;

            return null;
        }

        /// <summary>
        /// Tries to parse input string as double. If unsuccessful returns default value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double Parse(this string value, double defaultValue)
        {
            double result;

            if (double.TryParse(value, out result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// Tries to parse input string as double. If unsuccessful returns null.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double? ParseDouble(this string value)
        {
            double result;

            if (double.TryParse(value, out result))
                return result;

            return null;
        } 
    }
}