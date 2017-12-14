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
        private readonly int _heapSize;
        
        /// <summary>
        ///  Used to validate property of the heap. Either smallest 
        ///  element should be always root of each tree and sub-tree in a context of min heap
        ///  or largest when dealing with max heap.
        /// </summary>
        private readonly Comparison<T> _criteriaValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Heap{T}"/> class.
        /// </summary>
        /// <param name="heapSize"> Determines size of the heap. </param>
        /// <param name="criteriaValidator">Delegate which will determine if the heap property is according to given criteria. </param>
        public Heap(int heapSize, Comparison<T> criteriaValidator)
        {
            _heap = new T[heapSize];
            _heapSize = heapSize;
            _criteriaValidator = criteriaValidator;

            Count = 0;
        }

        /// <summary>
        ///  Gets total number of elements in the heap.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///   Tells if the heap is empty.
        /// </summary>
        public bool Empty => _heapSize > 0;

        /// <inheritdoc />
        /// <remarks> Takes O(nlogn) time complexity. </remarks>
        public void Add(T element)
        {
            _heap[Count] = element;
            Count++;

            Heapify(_heap, Count);
        }

        /// <remarks>
        ///  Maintains the heap property with the help of <see cref="_criteriaValidator"/> comparison delegate.
        ///  Takes O(logn) time complexity. 
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
                if(left < Count && _criteriaValidator.Invoke(array[left], array[index]) > 0)
                {
                    toSwap = left;
                }
                if(right < Count && _criteriaValidator.Invoke(array[right],array[toSwap]) < 0)
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

        /// <inheritdoc />
        /// <remarks> 
        ///  Takes the highest priority element from the heap.
        ///  Time complexity of this method is O(logn).
        /// </remarks>
        /// <exception cref="ArgumentException"> 
        ///  Throws <see cref="ArgumentException"/> when heap is empty.
        /// </exception>
        public T Extract()
        {
            if (_heapSize <= 0)
            {
                throw new ArgumentException("Can't extract elements from empty heap.");
            }

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
        ///  Takes the highest priority element from the heap safely.
        ///  Time complexity of this method is O(logn).
        /// </summary>
        /// <param name="element">Highest priority element.</param>
        /// <returns> If the operation is successfull.</returns>
        public bool TryExtract(out T element)
        {
            if (_heapSize <= 0)
            {
                element = default(T);
                return false;
            }
            element = Extract();
            return true;
        }

        /// <summary>
        ///  Sorts the heap without modifying it.
        /// </summary>
        /// <remarks> 
        ///  This method will sort the heap by priority. 
        ///  This has O(nlogn) time complexity.
        /// </remarks>
        /// <returns> Sorted enumerable in descending order.</returns>
        public IEnumerable<T> Sort()
        {
            var heap = this;
            while(heap.TryExtract(out var element))
            {
               yield return element;
            }
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
