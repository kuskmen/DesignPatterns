namespace DataStructures.Implementations
{
    using System;
    using System.Runtime.CompilerServices;
    using DataStructures.Abstractions;
    using DataStructures.Extensions;

    /// <summary>
    ///  Represents heap data struture.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the heap.</typeparam>
    public class MaxHeap<T> : IPriorityQueue<T>, IMaxHeap<T>
    {
        private T[] _items;

        /// <summary>
        ///  Used to validate property of the heap. Either smallest
        ///  element should be always root of each tree and sub-tree in a context of min heap
        ///  or largest when dealing with max heap.
        /// </summary>
        private readonly Comparison<T> _criteriaValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxHeap{T}"/> class.
        /// </summary>
        /// <param name="array"> Array to create heap from. </param>
        /// <param name="criteriaValidator">Delegate which will determine if the heap property is according to given criteria. </param>
        public MaxHeap(T[] array, Comparison<T> criteriaValidator)
        {
            _items = new T[array.Length];
            _criteriaValidator = criteriaValidator;

            Array.Copy(array, _items, array.Length);
            Count = _items.Length;

            for (var i = _items.Length / 2; i >= 0; i--)
            {
                Heapify(_items, _items.Length, i);
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///  Gets total number of elements in the heap.
        /// </summary>
        public int Count { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///   Tells if the heap is empty.
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        ///  Sorts array using heapsort.
        /// </summary>
        /// <returns> Ascending sorted array of items from the heap. </returns>
        public T[] Sort()
        {
            var arr = new T[_items.Length];
            Array.Copy(_items, arr, _items.Length);

            for (var i = arr.Length / 2 - 1; i >= 0; i--)
            {
                Heapify(arr, arr.Length, i);
            }

            for (var i = arr.Length - 1; i >= 0; i--)
            {
                arr.Swap(0, i);
                Heapify(arr, i, 0);
            }

            return arr;
        }

        /// <inheritdoc />
        /// <remarks> Takes O(logn) time complexity. </remarks>
        public void Add(T element)
        {
            if (_items.Length == Count)
            {
                // TODO: Research if this is better than using List<T> internally.
                Array.Resize(ref _items, _items.Length + 1);
            }

            var parent = Parent(Count);
            var child = Count;
            _items[Count] = element;
            Count++;

            while (parent >= 0 && _criteriaValidator.Invoke(_items[child], _items[parent]) > 0)
            {
                _items.Swap(parent, child);
                child = parent;
                parent = Parent(parent);
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
            if (_items.Length <= 0)
            {
                throw new ArgumentException("Can't extract elements from empty heap.");
            }

            // first element is always max element.
            var result = _items[0];

            // swap last element with first one and reduce the count, as we will
            // remove last element afterwards.
            _items[0] = _items[--Count];
            _items = _items.RemoveAt(Count);

            // heapify the heap in case we broke the property.
            Heapify(_items, _items.Length, 0);
            return result;
        }
        
        /// <inheritdoc />
        /// <summary>
        ///  Takes the highest priority element from the heap safely.
        ///  Time complexity of this method is O(logn).
        /// </summary>
        /// <param name="element">Highest priority element.</param>
        /// <returns> If the operation is successfull.</returns>
        public bool TryExtract(out T element)
        {
            if (_items.Length <= 0)
            {
                element = default;
                return false;
            }
            element = Extract();
            return true;
        }

        /// <inheritdoc />
        /// <exception cref="T:System.ArgumentException">Throws exception if heap is empty.</exception>
        public T GetMax()
        {
            if (_items.Length <= 0)
            {
                throw new ArgumentException("Can't extract elements from empty heap.");
            }

            return _items[0];
        }

        /// <inheritdoc />
        public bool TryGetMax(out T maxElement)
        {
            if (_items.Length <= 0)
            {
                maxElement = default;
                return false;
            }

            maxElement = GetMax();
            return true;
        }

        /// <inheritdoc />
        public T Remove(int index)
        {
            var element = _items[index];
            _items = _items.RemoveAt(index);
            Count--;
            for (var i = _items.Length / 2; i >= 0; i--)
            {
                Heapify(_items, _items.Length, i);
            }
            return element;
        }

        /// <summary>
        ///  Creates new instance of array of T.
        /// </summary>
        /// <returns> Array of T. </returns>
        public T[] ToArray()
        {
            var result = new T[Count];
            Array.Copy(_items, result, Count);

            return result;
        }

        /// <summary>
        ///  Gets the index of parent of given index.
        /// </summary>
        /// <param name="index">Index to find his parent index.</param>
        /// <returns> Parent index of <paramref name="index"/></returns>
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
            => (index << 1) + 1;

        /// <summary>
        ///  Gets the index of right child of given index.
        /// </summary>
        /// <param name="index">Index to found right child index to.</param>
        /// <returns> Index of the right child of the element with given index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Right(int index)
            => (index << 1) + 2;

        /// <summary>
        ///  Maintains the heap property with the help of <see cref="_criteriaValidator"/> comparison delegate.
        ///  Takes O(logn) time complexity.
        /// </summary>
        private void Heapify(T[] array, int size, int index)
        {
#if DEBUG
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
#endif
            while (index < size)
            {
                var leftChild = Left(index);
                var rightChild = Right(index);
                var bigger = index;

                if (leftChild < size && _criteriaValidator.Invoke(array[leftChild], array[index]) > 0)
                {
                    bigger = leftChild;
                }

                if (rightChild < size && _criteriaValidator.Invoke(array[rightChild], array[bigger]) > 0)
                {
                    bigger = rightChild;
                }

                if (bigger == index) return;

                array.Swap(bigger, index);

                index = bigger;
            }
        }
    }
}
