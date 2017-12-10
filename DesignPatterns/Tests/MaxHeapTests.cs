using System.Collections.Generic;
using DataStructures;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MaxHeapTests
    {

        [TestCase(new[] { 1 })]
        [TestCase(new[] { 10, 5, 8, 2, 14 })]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 })]
        public void Build_ShouldBuildCorrectMaxHeap(int[] array)
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(IsMaxHeap(new MaxHeap<int>(array)));
        }

        [TestCase(new[] { 1 }, 1)]
        [TestCase(new[] { 10, 5, 8, 2, 14 }, 14)]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, 90)]
        public void Extract_ShouldExtractAlwaysMaxElement(int[] array, int maxElement)
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(maxElement, new MaxHeap<int>(array).Extract());
        }

        /// <summary>
        ///  Determines if given array is max heap.
        /// </summary>
        /// <typeparam name="T">Type of the elements contained in the heap.</typeparam>
        /// <param name="heap"> The heap.</param>
        /// <param name="index"> Index from which to start.</param>
        /// <returns></returns>
        private static bool IsMaxHeap<T>(MaxHeap<T> heap, int index = 0)
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
