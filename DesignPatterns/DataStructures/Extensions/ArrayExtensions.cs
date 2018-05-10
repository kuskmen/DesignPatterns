namespace DataStructures.Extensions
{
    using System;
    using System.Collections.Generic;
    using DataStructures.Implementations;

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
        ///  Builds max heap out of source of <typeparamref name="T"/> using default comparer when not provided one.
        ///  This method takes O(logn) time complexity.
        /// </summary>
        /// <typeparam name="T"> Type of the elements in the source. </typeparam>
        /// <param name="array"> Array to build max heap from. </param>
        /// <param name="comparer"> Comparer used to determine which element has higher priority.</param>
        /// <returns> <see cref="MaxHeap{T}"/> instance. </returns>
        public static MaxHeap<T> BuildMaxHeap<T>(this T[] array, Comparer<T> comparer = null)
        {
            var criteriaValidator = comparer == null
                ? (Comparison<T>) ((first, second) => Comparer<T>.Default.Compare(first, second))
                : comparer.Compare;
            var heap = new MaxHeap<T>(array, criteriaValidator);

            return heap;
        }

        /// <summary>
        ///  Resizes an source and initializes extra elements with <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T"> Type of the elements in the source. </typeparam>
        /// <param name="array"> Array that will be resized. </param>
        /// <param name="newSize"> New size. </param>
        /// <param name="defaultValue"> Value of the new extra elements. </param>
        /// <returns> Resized <paramref name="array"/> with <paramref name="newSize"/> and default value for extra elements <paramref name="defaultValue"/></returns>
        public static T[] Resize<T>(this T[] array, int newSize, T defaultValue)
        {
            var oldSize = array.Length;
            Array.Resize(ref array, newSize);

            for (int i = oldSize; i <= newSize; i++)
            {
                array[i] = defaultValue;
            }

            return array;
        }
    }
}
