using System;

namespace DataStructures
{
    public static class Extensions
    {
        /// <summary>
        ///  Swaps two elements at two given indexes.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the array.</typeparam>
        /// <param name="array"> The array which will swap values from.</param>
        /// <param name="firstIndex"> Index that will be swaped with index number two.</param>
        /// <param name="secondIndex"> Index that will be swaped with index number one.</param>
        /// <throws><see cref="ArgumentException"/></throws>
        public static void Swap<T>(this T[] array, int firstIndex, int secondIndex)
        {
#if DEBUG
            if (array == null)
            {
               throw new ArgumentNullException(nameof(array));
            }
            if (firstIndex > array.Length || firstIndex < 0 ||
                secondIndex > array.Length || secondIndex < 0)
            {
                throw new ArgumentException("One of the indexes is out of range.");
            }
#endif
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        /// <summary>
        ///  Removes element from array at index.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the array.</typeparam>
        /// <param name="array"> Array to remove item from.</param>
        /// <param name="index"> Index which will be removed.</param>
        /// <returns>Returns new array without the element at passed index.</returns>
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
#if DEBUG
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (index < 0)
            {
                throw new ArgumentException(nameof(index));
            }
#endif
            var dest = new T[array.Length - 1];
            if (index > 0)
                Array.Copy(array, 0, dest, 0, index);

            if (index < array.Length - 1)
                Array.Copy(array, index + 1, dest, index, array.Length - index - 1);

            return dest;
        }
    }
}
