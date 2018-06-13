namespace DataStructures.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataStructures.Abstractions;
    using DataStructures.Implementations;
    using NUnit.Framework;

    using static HeapTestsHelper;

    public delegate bool HeapPropertyVerifier(int[] data);
    public delegate int ExtractMethodVerifier(int[] data);
    public delegate AbstractHeap<int> HeapFactory(int[] data, Comparison<int> criteriaValidator);

    [TestFixture]
    public class HeapTests
    {
        private readonly Comparison<int> _intMaxComparer = (first, second) => first > second ? 1 : first < second ? -1 : 0;

        [TestCaseSource(typeof(HeapTestsHelper), nameof(Build_ShouldBuildCorrectHeapSource))]
        public void Build_ShouldBuildCorrectHeap(int[] array, HeapPropertyVerifier verifier, HeapFactory heapFactory)
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(verifier(heapFactory(array, _intMaxComparer).ToArray()));
        }

        [TestCaseSource(typeof(HeapTestsHelper), nameof(Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeapSource))]
        public void Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeap(int[] array, HeapFactory heapFactory, ExtractMethodVerifier extractMethod)
        {
            // Arrange
            var heap = heapFactory(array, _intMaxComparer);

            // Act
            // Assert
            Assert.AreEqual(extractMethod(array), heap.Extract());
        }

        [TestCaseSource(typeof(HeapTestsHelper), nameof(Extract_ShouldThrowExceptionWhenExtractingFromEmptyHeapSource))]
        public void Extract_ShouldThrowExceptionWhenExtractingFromEmptyHeap(HeapFactory heapFactory)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => heapFactory(new int[0], _intMaxComparer).Extract());
        }

        [TestCaseSource(typeof(HeapTestsHelper), nameof(Add_ShouldAddSuccessfullyElementsInTheHeapSource))]
        public void Add_ShouldAddSuccessfullyElementsInTheHeap(int[] array, HeapFactory heapFactory)
        {
            // Arrange
            var heap = heapFactory(array, _intMaxComparer);

            // Act
            // Assert
            Assert.AreEqual(array.Length, heap.Count);
        }

        [TestCase(new[] { 3, 2, 1 })]
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

        [TestCase(new[] { 3, 2, 1 })]
        public void GetMin_ShouldReturnHighestElementInTheHeapWithoutExtractingIt(int[] array)
        {
            // Arrange
            var heap = new MinHeap<int>(array, _intMaxComparer);

            // Act
            var max = heap.GetMin();

            // Assert
            Assert.AreEqual(max, array.Min());
            Assert.AreEqual(array.Length, heap.Count);
        }

        [TestCaseSource(typeof(HeapTestsHelper), nameof(Remove_ShouldRemoveElementAtIndexAndThenFixTheHeapPropertySource))]
        public void Remove_ShouldRemoveElementAtIndexAndThenFixTheHeapProperty(int[] array, HeapFactory heapFactory, HeapPropertyVerifier verifier)
        {
            // Arrange
            var heap = heapFactory(array, _intMaxComparer);

            // Act
            heap.Remove(5);

            // Assert
            Assert.IsTrue(verifier(heap.ToArray()));
        }

        [TestCaseSource(typeof(HeapTestsHelper), nameof(IsEmpty_WhenHeapIsNotEmpty_ShouldReturnFalseSource))]
        public void IsEmpty_WhenHeapIsNotEmpty_ShouldReturnFalse(HeapFactory heapFactory)
        {
            // Arrange
            // Act
            // Assert
            Assert.IsFalse(new MaxHeap<int>(new []{ 435, 123, 1, 2, 5, 3}, _intMaxComparer).IsEmpty);
        }

        [TestCaseSource(typeof(HeapTestsHelper), nameof(IsEmpty_WhenHeapIsEmpty_ShouldReturnTrueSource))]
        public void IsEmpty_WhenHeapIsEmpty_ShouldReturnTrue(HeapFactory heapFactory)
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(heapFactory(new int[0], _intMaxComparer).IsEmpty);
        }

        [TestCaseSource(typeof(HeapTestsHelper), nameof(TryExtract_WhenThereAreItemsInTheHeap_ShouldExtractTheItemAndReturnTrueSource))]
        public void TryExtract_WhenThereAreItemsInTheHeap_ShouldExtractTheItemAndReturnTrue(HeapFactory heapFactory, ExtractMethodVerifier verifier)
        {
            // Arrange
            var array = new [] { 1, 2, 3 };
            var heap = heapFactory(array, _intMaxComparer);

            // Act
            var success = heap.TryExtract(out var maxElement);
            
            // Assert
            Assert.IsTrue(success);
            Assert.AreEqual(verifier(array), maxElement);
        }

        [TestCaseSource(typeof(HeapTestsHelper), nameof(TryExtract_WhenThereAreNoItemsInTheHeap_ShouldReturnFalseAndDefaultElementSource))]
        public void TryExtract_WhenThereAreNoItemsInTheHeap_ShouldReturnFalseAndDefaultElement(HeapFactory heapFactory)
        {
            // Arrange
            var array = new int[0];
            var heap = heapFactory(array, _intMaxComparer);

            // Act
            var success = heap.TryExtract(out var maxElement);

            // Assert
            Assert.IsFalse(success);
            Assert.AreEqual(default(int), maxElement);
        }

        [Test]
        public void GetMax_WhenHeapIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new MaxHeap<int>(new int[0], _intMaxComparer).GetMax());
        }

        [Test]
        public void GetMin_WhenHeapIsEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new MinHeap<int>(new int[0], _intMaxComparer).GetMin());
        }

        [Test]
        public void GetMax_WhenHeapIsNotEmpty_ShouldReturnMaxButNotRemoveItFromHeap()
        {
            // Arrange
            var array = new [] { 1, 2, 3 };
            var heap = new MaxHeap<int>(array, _intMaxComparer);

            // Act
            var actualCount = heap.Count;
            var max = heap.GetMax();

            // Assert
            Assert.AreEqual(array.Length, actualCount);
            Assert.AreEqual(array.Max(), max);
        }

        [Test]
        public void GetMin_WhenHeapIsNotEmpty_ShouldReturnMaxButNotRemoveItFromHeap()
        {
            // Arrange
            var array = new [] { 1, 2, 3 };
            var heap = new MinHeap<int>(array, _intMaxComparer);

            // Act
            var actualCount = heap.Count;
            var max = heap.GetMin();

            // Assert
            Assert.AreEqual(array.Length, actualCount);
            Assert.AreEqual(array.Min(), max);
        }

        [Test]
        public void TryGetMax_WhenHeapIsEmpty_ShouldReturnDefaultElementAndFalse()
        {
            // Arrange
            var array = new int[0];
            var heap = new MaxHeap<int>(array, _intMaxComparer);

            // Act
            var success = heap.TryGetMax(out var maxElement);

            // Assert
            Assert.IsFalse(success);
            Assert.AreEqual(default(int), maxElement);
        }

        [Test]
        public void TryGetMin_WhenHeapIsEmpty_ShouldReturnDefaultElementAndFalse()
        {
            // Arrange
            var array = new int[0];
            var heap = new MinHeap<int>(array, _intMaxComparer);

            // Act
            var success = heap.TryGetMin(out var maxElement);

            // Assert
            Assert.IsFalse(success);
            Assert.AreEqual(default(int), maxElement);
        }

        [Test]
        public void TryGetMax_WhenHeapIsNotEmpty_ShouldReturnMaxElementAndTrueAndNotRemoveItFromHeap()
        {
            // Arrange
            var array = new [] {1, 2, 3};
            var heap = new MaxHeap<int>(array, _intMaxComparer);

            // Act
            var actualCount = heap.Count;
            var success = heap.TryGetMax(out var maxElemet);

            // Assert
            Assert.AreEqual(array.Length, actualCount);
            Assert.AreEqual(array.Max(), maxElemet);
            Assert.IsTrue(success);
        }

        [Test]
        public void TryGetMin_WhenHeapIsNotEmpty_ShouldReturnMaxElementAndTrueAndNotRemoveItFromHeap()
        {
            // Arrange
            var array = new [] {1, 2, 3};
            var heap = new MinHeap<int>(array, _intMaxComparer);

            // Act
            var actualCount = heap.Count;
            var success = heap.TryGetMin(out var maxElemet);

            // Assert
            Assert.AreEqual(array.Length, actualCount);
            Assert.AreEqual(array.Min(), maxElemet);
            Assert.IsTrue(success);
        }

        [Test]
        public void InternalDataStructureOfMaxHeap_WhenInputParameterIsChanged_ShouldNotBeCorrupted()
        {
            // Arrange
            var input = new[] { 2, 3 };

            // Act
            var heap = new MaxHeap<int>(input, _intMaxComparer);

            input[0] = 7;
            heap.Add(4);

            //By this point heap should have already been heapified meaning that
            //if internal data was corrupted it should give wrong result

            // Assert
            Assert.AreEqual(4, heap.GetMax());
        }

        [Test]
        public void InternalDataStructureOfMinHeap_WhenInputParameterIsChanged_ShouldNotBeCorrupted()
        {
            // Arrange
            var input = new[] { 2, 3 };

            // Act
            var heap = new MinHeap<int>(input, _intMaxComparer);

            input[0] = -1;
            heap.Add(0);

            //By this point heap should have already been heapified meaning that
            //if internal data was corrupted it should give wrong result

            // Assert
            Assert.AreEqual(0, heap.GetMin());
        }

        [Test]
        public void Sort_ShouldCopyInternalItemsAndReturnNewSortedAscendingArray()
        {
            // Arrange
            var input = new[] { 9, 8, 6, 7, 4, 5, 1, 2 };
            var heap = new MaxHeap<int>(input, _intMaxComparer);

            // Act
            var actualResult = heap.Sort();
            Array.Sort(input);

            // Assert
            Assert.IsTrue(HeapTestsHelper.IsMaxHeap(heap.ToArray()));
            CollectionAssert.AreEqual(input, actualResult);
        }
    }

    internal static class HeapTestsHelper
    {
        public static bool IsMaxHeap(int[] array)
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

        public static bool IsMinHeap(int[] array)
        {
            var n = array.Length;

            for (var i = 0; i <= n / 2 - 1; i++)
            {
                // If any internal node is smaller than either of its children
                // then array does not represent a max-heap, so return false.
                if (array[i] > array[2 * i + 1])
                    return false;

                // There is possibility that the last internal node has 
                // only left child, and no right child. So fist check if 
                // right child index falls in the array index range
                if (2 * i + 2 < n)
                    if (array[i] > array[2 * i + 2])
                        return false;
            }
            return true;
        }

        private static MaxHeap<T> InitMaxHeap<T>(T[] data, Comparison<T> criteriaValidator)
            => new MaxHeap<T>(data, criteriaValidator);

        private static MinHeap<T> InitMinHeap<T>(T[] data, Comparison<T> criteriaValidator)
            => new MinHeap<T>(data, criteriaValidator);

        public static IEnumerable<object> Build_ShouldBuildCorrectHeapSource
        {
            get
            {
                yield return new TestCaseData(new[] { 1 }, new HeapPropertyVerifier(IsMaxHeap), new HeapFactory(InitMaxHeap)).SetName($"{nameof(HeapTests.Build_ShouldBuildCorrectHeap)} [ 1 ] {nameof(InitMaxHeap)}");
                yield return new TestCaseData(new[] { 1 }, new HeapPropertyVerifier(IsMinHeap), new HeapFactory(InitMinHeap)).SetName($"{nameof(HeapTests.Build_ShouldBuildCorrectHeap)} [ 1 ] {nameof(InitMinHeap)}");
                yield return new TestCaseData(new[] { 10, 5, 8, 2, 14 }, new HeapPropertyVerifier(IsMaxHeap), new HeapFactory(InitMaxHeap)).SetName($"{nameof(HeapTests.Build_ShouldBuildCorrectHeap)} [ 10, 5, 8, 2, 14 ] {nameof(InitMaxHeap)}");
                yield return new TestCaseData(new[] { 10, 5, 8, 2, 14 }, new HeapPropertyVerifier(IsMinHeap), new HeapFactory(InitMinHeap)).SetName($"{nameof(HeapTests.Build_ShouldBuildCorrectHeap)} [ 10, 5, 8, 2, 14 ] {nameof(InitMinHeap)}");
                yield return new TestCaseData(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, new HeapPropertyVerifier(IsMaxHeap), new HeapFactory(InitMaxHeap)).SetName($"{nameof(HeapTests.Build_ShouldBuildCorrectHeap)} [ 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 ] {nameof(InitMaxHeap)}");
                yield return new TestCaseData(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, new HeapPropertyVerifier(IsMinHeap), new HeapFactory(InitMinHeap)).SetName($"{nameof(HeapTests.Build_ShouldBuildCorrectHeap)} [ 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 ] {nameof(InitMinHeap)}");
            }
        } 

        public static IEnumerable<object> Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeapSource
        {
            get
            {
                yield return new TestCaseData(new[] { 1 }, new HeapFactory(InitMaxHeap), new ExtractMethodVerifier(Enumerable.Max)).SetName($"{nameof(HeapTests.Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeap)} [ 1 ] {nameof(InitMaxHeap)}");
                yield return new TestCaseData(new[] { 1 }, new HeapFactory(InitMinHeap), new ExtractMethodVerifier(Enumerable.Min)).SetName($"{nameof(HeapTests.Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeap)} [ 1 ] {nameof(InitMinHeap)}");
                yield return new TestCaseData(new[] { 10, 5, 8, 2, 14 }, new HeapFactory(InitMaxHeap), new ExtractMethodVerifier(Enumerable.Max)).SetName($"{nameof(HeapTests.Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeap)} [ 10, 5, 8, 2, 14 ] {nameof(InitMaxHeap)}");
                yield return new TestCaseData(new[] { 10, 5, 8, 2, 14 }, new HeapFactory(InitMinHeap), new ExtractMethodVerifier(Enumerable.Min)).SetName($"{nameof(HeapTests.Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeap)} [ 10, 5, 8, 2, 14 ] {nameof(InitMinHeap)}");
                yield return new TestCaseData(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, new HeapFactory(InitMaxHeap), new ExtractMethodVerifier(Enumerable.Max)).SetName($"{nameof(HeapTests.Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeap)} [ 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 ] {nameof(InitMaxHeap)}");
                yield return new TestCaseData(new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 }, new HeapFactory(InitMinHeap), new ExtractMethodVerifier(Enumerable.Min)).SetName($"{nameof(HeapTests.Extract_ShouldExtractAlwaysElementWithBiggestPriorityFromHeap)} [ 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 ] {nameof(InitMinHeap)}");
            }
        } 

        public static IEnumerable<TestCaseData> Extract_ShouldThrowExceptionWhenExtractingFromEmptyHeapSource
        {
            get
            {
                yield return new TestCaseData(new HeapFactory(InitMinHeap)).SetName($"{nameof(HeapTests.Extract_ShouldThrowExceptionWhenExtractingFromEmptyHeap)} {nameof(InitMaxHeap)}");
                yield return new TestCaseData(new HeapFactory(InitMaxHeap)).SetName($"{nameof(HeapTests.Extract_ShouldThrowExceptionWhenExtractingFromEmptyHeap)} {nameof(InitMinHeap)}");
            }
        }

        public static IEnumerable<TestCaseData> Add_ShouldAddSuccessfullyElementsInTheHeapSource
        {
            get
            {
                yield return new TestCaseData(new[] { 1 }, new HeapFactory(InitMinHeap)).SetName($"{nameof(HeapTests.Add_ShouldAddSuccessfullyElementsInTheHeap)} {nameof(InitMaxHeap)}");
                yield return new TestCaseData(new[] { 3, 2, 1}, new HeapFactory(InitMaxHeap)).SetName($"{nameof(HeapTests.Add_ShouldAddSuccessfullyElementsInTheHeap)} {nameof(InitMinHeap)}");
            }
        }

        public static IEnumerable<TestCaseData> Remove_ShouldRemoveElementAtIndexAndThenFixTheHeapPropertySource
        {
            get
            {
                yield return new TestCaseData(new[] { 12, 15, 5, -17, 20, 20, 0, -1, -5 }, new HeapFactory(InitMinHeap), new HeapPropertyVerifier(IsMinHeap)).SetName($"{nameof(HeapTests.Remove_ShouldRemoveElementAtIndexAndThenFixTheHeapProperty)} [ 12, 15, 5, -17, 20, 20, 0, -1, -5 ] {nameof(InitMinHeap)}");
                yield return new TestCaseData(new[] { 12, 15, 5, -17, 20, 20, 0, -1, -5 }, new HeapFactory(InitMaxHeap), new HeapPropertyVerifier(IsMaxHeap)).SetName($"{nameof(HeapTests.Remove_ShouldRemoveElementAtIndexAndThenFixTheHeapProperty)} [ 12, 15, 5, -17, 20, 20, 0, -1, -5 ] {nameof(InitMaxHeap)}");
            }
        }

        public static IEnumerable<TestCaseData> IsEmpty_WhenHeapIsNotEmpty_ShouldReturnFalseSource
        {
            get
            {
                yield return new TestCaseData(new HeapFactory(InitMinHeap)).SetName($"{nameof(HeapTests.IsEmpty_WhenHeapIsEmpty_ShouldReturnTrue)} {nameof(InitMinHeap)}");
                yield return new TestCaseData(new HeapFactory(InitMaxHeap)).SetName($"{nameof(HeapTests.IsEmpty_WhenHeapIsEmpty_ShouldReturnTrue)} {nameof(InitMaxHeap)}");
            }
        }

        public static IEnumerable<TestCaseData> IsEmpty_WhenHeapIsEmpty_ShouldReturnTrueSource
        {
            get
            {
                yield return new TestCaseData(new HeapFactory(InitMinHeap)).SetName($"{nameof(HeapTests.IsEmpty_WhenHeapIsNotEmpty_ShouldReturnFalse)} {nameof(InitMinHeap)}");
                yield return new TestCaseData(new HeapFactory(InitMaxHeap)).SetName($"{nameof(HeapTests.IsEmpty_WhenHeapIsNotEmpty_ShouldReturnFalse)} {nameof(InitMaxHeap)}");
            }
        }

        public static IEnumerable<TestCaseData> TryExtract_WhenThereAreItemsInTheHeap_ShouldExtractTheItemAndReturnTrueSource
        {
            get
            {
                yield return new TestCaseData(new HeapFactory(InitMinHeap), new ExtractMethodVerifier(Enumerable.Min)).SetName($"{nameof(HeapTests.TryExtract_WhenThereAreItemsInTheHeap_ShouldExtractTheItemAndReturnTrue)} {nameof(InitMinHeap)}");
                yield return new TestCaseData(new HeapFactory(InitMaxHeap), new ExtractMethodVerifier(Enumerable.Max)).SetName($"{nameof(HeapTests.TryExtract_WhenThereAreItemsInTheHeap_ShouldExtractTheItemAndReturnTrue)} {nameof(InitMaxHeap)}");
            }
        }

        public static IEnumerable<TestCaseData> TryExtract_WhenThereAreNoItemsInTheHeap_ShouldReturnFalseAndDefaultElementSource
        {
            get
            {
                yield return new TestCaseData(new HeapFactory(InitMinHeap)).SetName($"{nameof(HeapTests.TryExtract_WhenThereAreNoItemsInTheHeap_ShouldReturnFalseAndDefaultElement)} {nameof(InitMinHeap)}");
                yield return new TestCaseData(new HeapFactory(InitMaxHeap)).SetName($"{nameof(HeapTests.TryExtract_WhenThereAreNoItemsInTheHeap_ShouldReturnFalseAndDefaultElement)} {nameof(InitMaxHeap)}");
            }
        }
    }   
}
