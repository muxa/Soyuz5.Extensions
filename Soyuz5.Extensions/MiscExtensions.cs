﻿namespace System
{
    public static class MiscExtensions
    {
        /// <summary>
        /// Returns new value and increments changeCounter if values are different. If not changed returns old value. 
        /// Will treat null and empty string as being equal. 
        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="newValue"></param>
        /// <param name="changeCounter"></param>
        /// <returns></returns>
        public static string ChangedValue(this string newValue, string originalValue, ref int changeCounter)
        {
            if (originalValue != newValue)
            {
                if (string.IsNullOrEmpty(originalValue) && string.IsNullOrEmpty(newValue))
                    return originalValue;

                changeCounter++;
            }
            return newValue;
        }

        /// <summary>
        /// Returns new value and increments changeCounter if values are different. If not changed returns old value. 
        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="newValue"></param>
        /// <param name="changeCounter"></param>
        /// <param name="nullAsEmpty">If true will treat null and empty string as being equal (default true)</param>
        /// <returns></returns>
        public static string ChangedValue(this string newValue, string originalValue, ref int changeCounter, bool nullAsEmpty)
        {
            if (originalValue != newValue)
            {
                if (nullAsEmpty && string.IsNullOrEmpty(originalValue) && string.IsNullOrEmpty(newValue))
                    return originalValue;

                changeCounter++;
            }
            return newValue;
        }

        /// <summary>
        /// Returns new value and increments changeCounter if values are different. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalValue"></param>
        /// <param name="newValue"></param>
        /// <param name="changeCounter"></param>
        /// <returns></returns>
        public static T ChangedValue<T>(this T newValue, T originalValue, ref int changeCounter)
            where T : struct
        {
            if (!originalValue.Equals(newValue))
            {
                changeCounter++;
            }
            return newValue;
        }

        /// <summary>
        /// Returns new value and increments changeCounter if values are different. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalValue"></param>
        /// <param name="newValue"></param>
        /// <param name="changeCounter"></param>
        /// <returns></returns>
        public static Nullable<T> ChangedValue<T>(this Nullable<T> newValue, Nullable<T> originalValue, ref int changeCounter)
            where T : struct
        {
            if (!originalValue.HasValue && !newValue.HasValue)
                return newValue;

            if (!originalValue.HasValue || !newValue.HasValue)
            {
                changeCounter++;
                return newValue;
            }

            if (!originalValue.Equals(newValue))
            {
                // both have values
                changeCounter++;
            }
            return newValue;
        }
    }
}