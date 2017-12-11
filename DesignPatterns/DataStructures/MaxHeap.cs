namespace DataStructures
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///  Represents max heap data struture.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the heap.</typeparam>
    public class MaxHeap<T> : IHeap<T>
    {
        private T[] _heap;

        /// <summary>
        ///  Gets total number of elements in the heap.
        /// </summary>
        public int Count => _heap.Length;
        /// <summary>
        ///  Initializes instance of type <see cref="MaxHeap{T}"/> when provided with array of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="heap"> Array of type <typeparamref name="T"/></param>
        public MaxHeap(T[] heap)
        {
            _heap = heap;
            Build(_heap);
        }
        public T this[int index]
        {
            get => _heap[index];
            set
            {
                _heap[index] = value;
            }
        }

        /// <inheritdoc />
        /// <remarks> Takes O(nlogn) time complexity. </remarks>
        public void Add(T element)
        {
            _heap[_heap.Length] = element;
            Heapify(_heap, _heap.Length);
        }
        /// <inheritdoc />
        /// <remarks> Takes O(nlogn) time complexity. </remarks>
        public void Heapify(T[] array, int index = 0)
        {
            while (true)
            {
                var left = Left(index);
                var right = Right(index);
                var largest = index;

                if (left < array.Length && Comparer<T>.Default.Compare(array[left], array[index]) > 0)
                {
                    largest = left;
                }
                if (right < array.Length && Comparer<T>.Default.Compare(array[right], array[largest]) > 0)
                {
                    largest = right;
                }
                if (largest != index)
                {
                    array.Swap(index, largest);
                    index = largest;
                    continue;
                }
                break;
            }
        }
        ///<inheritdoc />
        /// <remarks> Takes O(n) time complexity. </remarks>
        public void Build(T[] array)
        {
            for (var i = array.Length / 2; i >= 0; i--)
            {
                Heapify(array, i);
            }
        }
        /// <inheritdoc />
        /// <remarks> This method extracts max element of the heap.</remarks>
        public T Extract()
        {
            var result = _heap[0];
            _heap[0] = _heap[_heap.Length - 1];
            // TODO: Research how to do the deletion more elegantly.
            _heap = _heap.RemoveAt(_heap.Length - 1);
            Heapify(_heap);
            return result;
        }
        /// <summary>
        /// Sorts the heap without modifying it.
        /// </summary>
        /// <remarks> This has O(nlogn) time complexity. </remarks>
        /// <returns> Sorted enumerable in descending order.</returns>
        public IEnumerable<T> Sort()
        {
            // TODO: Check if move semantics are available in C#
            var heap = new MaxHeap<T>(_heap);
            var result = new T[_heap.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = heap.Extract();
            }
            return result;
        }

        /// <summary>
        ///  Gets the parent index of the <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Index to which parent index will be searched for.</param>
        /// <returns> Parent index. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Parent(int index)
            => index % 2 == 0 ? index / 2 - 1 : index / 2;

        /// <summary>
        ///  Gets the index of left child of given index.
        /// </summary>
        /// <param name="index">Index to found left child index to.</param>
        /// <returns> Index of the left child of the element with given index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Left(int index)
            => 2 * index + 1;

        /// <summary>
        ///  Gets the index of right child of given index.
        /// </summary>
        /// <param name="index">Index to found right child index to.</param>
        /// <returns> Index of the right child of the element with given index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Right(int index)
            => 2 * index + 2;

        #region IEnumerable
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_heap).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }

    /// <summary>
    ///   Represents heap data structure.
    /// </summary>
    /// <typeparam name="T">Type of the parameters stored in the heap.</typeparam>
    public interface IHeap<T> : IEnumerable<T>
    {
        /// <summary>
        ///  Builds heap out of array.
        /// </summary>
        /// <param name="array">Array to build heap from.</param>
        void Build(T[] array);
        /// <summary>
        ///  Adds element to the heap.
        /// </summary>
        /// <param name="element">Element to add.</param>
        void Add(T element);
        /// <summary>
        ///  Transforms array into heap.
        /// </summary>
        /// <param name="array">Array to be transformed.</param>
        /// <param name="index"> Index of the root of the subtree to heapify. When it is zero this means heapify whole heap.</param>
        void Heapify(T[] array, int index = 0);
        /// <summary>
        ///  Extracts element from the heap.
        /// </summary>
        /// <returns> Extracted element.</returns>
        T Extract();
    }
}
