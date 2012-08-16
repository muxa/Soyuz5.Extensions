namespace System.Collections.Generic
{

    public static class GenericCollectionExtensions
    {
        /// <summary>
        /// Adds all items to the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemsToAdd"></param>
        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> itemsToAdd)
        {
            foreach (T item in itemsToAdd)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Removes items from the list identified by range
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="fromIndex"></param>
        /// <param name="count">If -1 will remove all remaining elements since fromIndex</param>
        public static void RemoveRange<T>(this IList<T> list, int fromIndex, int count = -1)
        {
            if (fromIndex < 0)
                throw new ArgumentOutOfRangeException("fromIndex", "Must be non-negative");

            if (count == 0)
                return;

            if (fromIndex == 0 && (count == -1 || count == list.Count))
            {
                list.Clear();
            }
            else
            {
                if ((fromIndex + count) > list.Count)
                    throw new ArgumentOutOfRangeException("count", "Trying to remove too many items");

                for (int i = count - 1; i >= 0; i--)
                {
                    list.RemoveAt(fromIndex + i);
                }
            }
        }

        /// <summary>
        /// Removes items from the list based on  a predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate">Return true to remove</param>
        public static void Remove<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list.Count== 0)
                return;
            int index = list.Count - 1;
            do
            {
                if (predicate(list[index]))
                {
                    list.RemoveAt(index);
                }
                index--;
            } while (index >= 0);
        }

        /*/// <summary>
        /// Removes items from the list based on  a predicate
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate">Return true to remove</param>
        public static void Remove(this IList list, Func<object, bool> predicate)
        {
            int index = list.Count - 1;
            do
            {
                if (predicate(list[index]))
                {
                    list.RemoveAt(index);
                }
                index--;
            }
            while (index >= 0);
        }*/
    }
}