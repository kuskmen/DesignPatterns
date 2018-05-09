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

        [Ignore("Ignored until properly developed.")]
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
            // TODO
            // Assert.IsTrue(IsMaxHeap(heap));
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
    }
}
