using System;
using System.Linq;
using DataStructures.Implementations;
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
            var heap = new MaxHeap<int>(array, _intMaxComparer);
            foreach (var element in array)
            {
                heap.Add(element);
            }

            // Act
            // Assert
            Assert.IsTrue(IsMaxHeap(heap.ToArray()));
        }

        //[TestCase(new[] { 1 })]
        [TestCase(new[] { 10, 5, 8, 2, 14 })]
        [TestCase(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 })]
        public void Extract_ShouldExtractAlwaysMaxElementFromMaxHeap(int[] array)
        {
            // Arrange
            var heap = new MaxHeap<int>(array, _intMaxComparer);

            // Act
            // Assert
            Assert.AreEqual(array.Max(), heap.Extract());
        }

        [Test]
        public void Extract_ShouldThrowExceptionWhenExtractingFromEmptyHeap()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new MaxHeap<int>(new int[0], _intMaxComparer).Extract());
        }

        [TestCase(new[] { 1 })]
        [TestCase(new[] { 3, 2, 1 })]
        public void Add_ShouldAddSuccessfullyElementsInTheHeap(int[] array)
        {
            // Arrange
            var heap = new MaxHeap<int>(array, _intMaxComparer);

            // Act
            // Assert
            Assert.AreEqual(array.Length, heap.Count);
        }

        [TestCase(new[] {3, 2, 1})]
        public void GetMax_ShouldReturnHighestElementInTheHeapWithoutExtractingIt(int[] array)
        {
            // Arrange
            var heap = new MaxHeap<int>(array, _intMaxComparer);

            // Act
            var max = heap.GetMax();

            // Assert
            Assert.AreEqual(max, array.Max());
            Assert.AreEqual(array.Length, heap.Count);
        }

        [TestCase(new[] {12, 15, 5, -17, 20, 20, 0, -1, -5})]
        public void Remove_ShouldRemoveElementAtIndexAndThenFixTheHeapProperty(int[] array)
        {
            // Arrange
            var heap = new MaxHeap<int>(array, _intMaxComparer);

            // Act
            heap.Remove(5);

            // Assert
            Assert.IsTrue(IsMaxHeap(heap.ToArray()));
        }

        private static bool IsMaxHeap(int[] array)
        {
            var n = array.Length;

            // Start from root and go till the last internal node
            for (var i = 0; i <= n / 2 - 1; i++)
            {
                // If any internal node is smaller than either of its children
                // then array does not represent a max-heap, so return false.
                if (array[i] < array[2 * i + 1])
                    return false;

                // There is possibility that the last internal node has 
                // only left child, and no right child. So fist check if 
                // right child index falls in the array index range
                if (2 * i + 2 < n)
                    if (array[i] < array[2 * i + 2])
                        return false;
            }
            return true;
        }
    }
}
