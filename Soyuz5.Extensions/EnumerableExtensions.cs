using System.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Yields a single value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> Yield<T>(this T value)
        {
            yield return value;
            yield break;
        }
/*
        /// <summary>
        /// Returns first item from items, if there are no items returns defaultValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> items, T defaultValue) where T : class
        {
            T item = items.FirstOrDefault();
            if (item != null)
                return item;

            return defaultValue;
        }

        /// <summary>
        /// Returns first value from items, if there are no items returns defaultValue
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="items"></param>
        /// <param name="valueFunc"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TResult ValueOrDefault<TItem, TResult>(this IEnumerable<TItem> items, Func<TItem,TResult> valueFunc, TResult defaultValue) where TItem : class
        {
            TItem item = items.FirstOrDefault();
            if (item != null)
                return valueFunc(item);

            return defaultValue;
        }*/
    }
}