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

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> seq, Func<T,T,bool> comparer)
        {
            return seq.Distinct(new LambdaComparer<T>(comparer));
        }
    }

    internal sealed class LambdaComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _del;
        private IEqualityComparer<T> _comparer;

        public LambdaComparer(Func<T, T, bool> del)
        {
            _del = del;
            _comparer = EqualityComparer<T>.Default;
        }

        public bool Equals(T left, T right)
        {
            return _del(left, right);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
        public int GetHashCode(T obj)
        {
            return _comparer.GetHashCode(obj);
        }
    }
}