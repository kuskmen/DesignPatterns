namespace DataStructures.Extensions
{
    using System;

    public static class ArrayExtensions
    {
        /// <summary>
        ///  Swaps two elements at two given indexes.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the source.</typeparam>
        /// <param name="array"> The source which will swap values from.</param>
        /// <param name="firstIndex"> Index that will be swaped with index number two.</param>
        /// <param name="secondIndex"> Index that will be swaped with index number one.</param>
        /// <throws><see cref="ArgumentException"/></throws>
        /// <throws><see cref="ArgumentNullException"/></throws>
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
                throw new IndexOutOfRangeException("One of the indexes is out of range.");
            }
#endif
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        /// <summary>
        ///  Removes element from source at index.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the source.</typeparam>
        /// <param name="source"> Array to remove item from.</param>
        /// <param name="index"> Index which will be removed.</param>
        /// <returns>Returns new source without the element at passed index.</returns>
        /// <throws><see cref="ArgumentNullException"/></throws>
        /// <throws><see cref="IndexOutOfRangeException"/></throws>
        public static T[] RemoveAt<T>(this T[] source, int index)
        {
#if DEBUG
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (index < 0 || index > source.Length)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
#endif
            var dest = new T[source.Length - 1];

            if(index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        /// <summary>
        ///  Resizes an source and initializes extra elements with <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T"> Type of the elements in the source. </typeparam>
        /// <param name="array"> Array that will be resized. </param>
        /// <param name="newSize"> New size. </param>
        /// <param name="defaultValue"> Value of the new extra elements. </param>
        /// <returns> Resized <paramref name="array"/> with <paramref name="newSize"/> and default value for extra elements <paramref name="defaultValue"/></returns>
        /// <throws><see cref="ArgumentNullException"/></throws>
        /// <throws><see cref="ArgumentException"/></throws>
        public static T[] Resize<T>(ref T[] array, int newSize, T defaultValue)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (newSize < 0 || newSize < array.Length)
            {
                throw new ArgumentException(nameof(newSize));
            }

            var oldSize = array.Length;
            Array.Resize(ref array, newSize);

            for (var i = oldSize; i < newSize; i++)
            {
                array[i] = defaultValue;
            }

            return array;
        }
    }
}
