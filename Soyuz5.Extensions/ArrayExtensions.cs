namespace System
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Gets first element of the array if and only if there only one element in the array. 
        /// Otherwise returns default value. 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int SingleElementOnly(this int[] array, int defaultValue)
        {
            if (array == null) return defaultValue;

            if (array.Length == 1) return array[0];

            return defaultValue;
        }

        /// <summary>
        /// Gets first element of the array if and only if there only one element in the array. 
        /// Otherwise returns default value. 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object SingleElementOnly(this int[] array, object defaultValue)
        {
            if (array == null) return defaultValue;

            if (array.Length == 1) return array[0];

            return defaultValue;
        }
    }
}