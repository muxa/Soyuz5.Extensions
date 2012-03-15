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
    }
}