namespace DataStructures.Heap
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///  Represents heap data struture.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the heap.</typeparam>
    public class Heap<T> : IPriorityQueue<T>
    {
        private T[] _heap;
        private readonly HeapType _heapType;
        
        /// <summary>
        ///  Used to validate property of the heap. Either smallest 
        ///  element should be always root of each tree and sub-tree in a context of min heap
        ///  or largest when dealing with max heap.
        /// </summary>
        private readonly Func<int, int, T[], bool> _heapPropertyValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Heap{T}"/> class.
        /// </summary>
        /// <param name="heap"> Array of type <typeparamref name="T"/></param>
        /// <param name="heapType"> Determines what kind of heap this instance will be. It can be any tye of <see cref="HeapType"/>.</param>
        public Heap(T[] heap, HeapType heapType)
        {
            _heap = heap;
            _heapType = heapType; 
            Count = heap.Length;

            switch (heapType)
            {
                case HeapType.Max:
                    _heapPropertyValidator =
                        (comparingIndex, comparingIndex2, array) =>
                            comparingIndex < Count && Comparer<T>.Default.Compare(array[comparingIndex], array[comparingIndex2]) > 0;
                    break;
                case HeapType.Min:
                    _heapPropertyValidator =
                        (comparingIndex, comparingIndex2, array) =>
                            comparingIndex < Count && Comparer<T>.Default.Compare(array[comparingIndex], array[comparingIndex2]) < 0;
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException(nameof(heapType), heapType, null);
#endif
            }

            Build(_heap);
        }

        /// <summary>
        ///  Gets total number of elements in the heap.
        /// </summary>
        public int Count { get; private set; }

        /// <inheritdoc />
        /// <remarks> Takes O(nlogn) time complexity. </remarks>
        public void Add(T element)
        {
#if DEBUG
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }
#endif
            _heap[Count] = element;
            Count++;

            Heapify(_heap, Count);
        }

        /// <remarks>
        ///  Maintains the heap property with the help of <see cref="_heapPropertyValidator"/> function.
        ///  Takes O(nlogn) time complexity. 
        /// </remarks>
        private void Heapify(T[] array, int index)
        {
#if DEBUG
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
#endif
            while (true)
            {
                var left = Left(index);
                var right = Right(index);
                var toSwap = index;

                if(_heapPropertyValidator.Invoke(left, index, array))
                {
                    toSwap = left;
                }
                if(_heapPropertyValidator.Invoke(right, toSwap, array))
                {
                    toSwap = right;
                }
                if (toSwap != index)
                {
                    array.Swap(index, toSwap);
                    index = toSwap;
                    continue;
                }
                break;
            }
        }

        ///<inheritdoc />
        /// <remarks> Takes O(n) time complexity. </remarks>
        public void Build(T[] array)
        {
#if DEBUG
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
#endif
            for (var i = Count / 2; i >= 0; i--)
            {
                Heapify(array, i);
            }
        }

        /// <inheritdoc />
        /// <remarks> 
        ///  Takes the highest priority element from the heap.
        ///  Time complexity of this method is O(logn).
        /// </remarks>
        public T Extract()
        {
            // first element is always max element.
            var result = _heap[0];

            // swap last element with first one and reduce the count, as we will
            // remove last element afterwards.
            _heap[0] = _heap[--Count];
            _heap = _heap.RemoveAt(Count);

            // heapify the heap in case we broke the property.
            Heapify(_heap, 0);
            return result;
        }

        /// <summary>
        ///  Sorts the heap without modifying it.
        /// </summary>
        /// <remarks> 
        ///  This method will sort the heap in asc(when the heap is of type min) and 
        ///  desc(when the heap is of type max). This has O(nlogn) time complexity.
        /// </remarks>
        /// <returns> Sorted enumerable in descending order.</returns>
        public IEnumerable<T> Sort()
        {
            // TODO: Check if move semantics are available in C#
            var heap = new Heap<T>(_heap, _heapType);
            var result = new T[Count];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = heap.Extract();
            }
            return result;
        }

        /// <summary>
        ///  Gets the index of left child of given index.
        /// </summary>
        /// <param name="index">Index to found left child index to.</param>
        /// <returns> Index of the left child of the element with given index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Left(int index)
            => (index << 1) + 1;

        /// <summary>
        ///  Gets the index of right child of given index.
        /// </summary>
        /// <param name="index">Index to found right child index to.</param>
        /// <returns> Index of the right child of the element with given index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Right(int index)
            => (index << 1) + 2;
    }
}
