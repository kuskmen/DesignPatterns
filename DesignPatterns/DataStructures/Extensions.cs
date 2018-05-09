namespace DataStructures
{
    using System;
    using System.Collections.Generic;
    using DataStructures.Implementations;

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
                throw new IndexOutOfRangeException("One of the indexes is out of range.");
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
            if (index < 0 || index > array.Length)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
#endif
            if (index < array.Length - 1)
                Array.Copy(array, index + 1, array, index, array.Length - index);

            array[array.Length - 1] = default;
            return array;
        }

        /// <summary>
        ///  Builds max heap out of array of <typeparamref name="T"/> using default comparer when not provided one.
        ///  This method takes O(logn) time complexity.
        /// </summary>
        /// <typeparam name="T"> Type of the elements in the array. </typeparam>
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
        ///  Resizes an array and initializes extra elements with <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T"> Type of the elements in the array. </typeparam>
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
