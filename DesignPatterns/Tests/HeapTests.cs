using System.Collections.Generic;
using DataStructures;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class HeapTests
    {

        [TestCase(new[] { 1 })]
        [TestCase(new[] { 10, 5, 8, 2, 14 })]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 })]
        public void Build_ShouldBuildCorrectMaxHeap(int[] array)
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(IsMaxHeap(new Heap<int>(array, HeapType.Max)));
        }

        [TestCase(new[] { 1 }, 1)]
        [TestCase(new[] { 10, 5, 8, 2, 14 }, 14)]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, 90)]
        public void Extract_ShouldExtractAlwaysMaxElementFromMaxHeap(int[] array, int maxElement)
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(maxElement, new Heap<int>(array, HeapType.Max).Extract());
        }

        [TestCase(new[] { 1 }, 1)]
        [TestCase(new[] { 10, 5, 8, 2, 14 }, 2)]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, 1)]
        public void Extract_ShouldExtractAlwaysMinElementFromMinHeap(int[] array, int maxElement)
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(maxElement, new Heap<int>(array, HeapType.Min).Extract());
        }

        [TestCase(new[] { 10, 5, 8, 2, 14 }, new[] { 14, 10, 8, 5, 2})]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, new[] { 90, 36, 26, 25, 19, 17, 7, 3, 2, 1})]
        public void Sort_WhenHeapIsMax_ShouldReturnSortedInDescOrderArray(int[] heap, int[] sorted)
        {
            // Arrange
            // Act
            // Assert
            CollectionAssert.AreEqual(new Heap<int>(heap, HeapType.Max).Sort(), sorted);
        }

        [TestCase(new[] { 10, 5, 8, 2, 14 }, new[] { 2, 5, 8, 10, 14 })]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, new[] { 1, 2, 3, 7, 17, 19, 25, 26, 36, 90 })]
        public void Sort_WhenHeapIsMin_ShouldReturnSortedInAscOrderArray(int[] heap, int[] sorted)
        {
            // Arrange
            // Act
            // Assert
            CollectionAssert.AreEqual(new Heap<int>(heap, HeapType.Min).Sort(), sorted);
        }

        /// <summary>
        ///  Determines if given array is max heap.
        /// </summary>
        /// <typeparam name="T">Type of the elements contained in the heap.</typeparam>
        /// <param name="heap"> The heap.</param>
        /// <param name="index"> Index from which to start.</param>
        /// <returns></returns>
        private static bool IsMaxHeap<T>(Heap<T> heap, int index = 0)
        {
            // if it is a leaf it is max heap.
            if (index >= (heap.Count - 1)/2)
            {
                return true;
            }

            return Comparer<T>.Default.Compare(heap[index], heap[2 * index + 1]) > 0
                   && Comparer<T>.Default.Compare(heap[index], heap[2 * index + 2]) > 0
                   && IsMaxHeap(heap, 2 * index + 1)
                   && IsMaxHeap(heap, 2 * index + 2);
        }
    }
}
