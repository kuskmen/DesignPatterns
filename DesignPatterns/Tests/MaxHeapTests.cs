using System;
using System.Collections.Generic;
using DataStructures.Heap;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MaxHeapTests
    {
        private readonly Comparison<int> _intMaxComparer = (first, second) => first > second ? 1 : first < second ? -1 : 0;

        [TestCase(new[] { 1 })]
        [TestCase(new[] { 10, 5, 8, 2, 14 })]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 })]
        public void Build_ShouldBuildCorrectMaxHeap(int[] array)
        {
            // Arrange
            var heap = new MaxHeap<int>(array.Length, _intMaxComparer);
            foreach (var element in array)
            {
                heap.Add(element);
            }

            // Act
            // Assert
            // TODO
            // Assert.IsTrue(IsMaxHeap(heap));
        }

        [TestCase(new[] { 1 }, 1)]
        [TestCase(new[] { 10, 5, 8, 2, 14 }, 14)]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, 90)]
        public void Extract_ShouldExtractAlwaysMaxElementFromMaxHeap(int[] array, int maxElement)
        {
            // Arrange
            var heap = new MaxHeap<int>(array.Length, _intMaxComparer);
            foreach (var element in array)
            {
                heap.Add(element);
            }

            // Act
            // Assert
            Assert.AreEqual(maxElement, heap.Extract());
        }

        public void Extract_ShouldThrowExceptionWhenExtractingFromEmptyHeap()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new MaxHeap<int>(0, _intMaxComparer).Extract());
        }

        [TestCase(new[] { 1 })]
        [TestCase(new[] { 3, 2, 1 })]
        public void Add_ShouldAddSuccessfullyElementsInTheHeap(int[] array)
        {
            // Arrange
            var heap = new MaxHeap<int>(array.Length, _intMaxComparer);
            foreach (var element in array)
            {
                heap.Add(element);
            }

            // Act
            Assert.AreEqual(array.Length, heap.Count);
        }

        /// <summary>
        ///  Determines if given array is max maxHeap.
        /// </summary>
        /// <typeparam name="T">Type of the elements contained in the maxHeap.</typeparam>
        /// <param name="maxHeap"> The maxHeap.</param>
        /// <param name="index"> Index from which to start.</param>
        /// <returns>Whether the heap is max heap or not.</returns>
        //private static bool IsMaxHeap<T>(MaxHeap<T> maxHeap, int index = 0)
        //{
        //    // if it is a leaf it is max maxHeap.
        //    if (index >= (maxHeap.Count - 1) / 2)
        //    {
        //        return true;
        //    }

        //    return Comparer<T>.Default.Compare(maxHeap[index], maxHeap[2 * index + 1]) > 0
        //           && Comparer<T>.Default.Compare(maxHeap[index], maxHeap[2 * index + 2]) > 0
        //           && IsMaxHeap(maxHeap, 2 * index + 1)
        //           && IsMaxHeap(maxHeap, 2 * index + 2);
        //}
    }
}
