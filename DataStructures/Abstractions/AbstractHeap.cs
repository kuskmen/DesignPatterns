namespace DataStructures.Abstractions
{
    using System;
    using System.Runtime.CompilerServices;
    using DataStructures.Extensions;

    public abstract class AbstractHeap<T> : IPriorityQueue<T>
    {
        protected T[] _items;
        
        /// <summary>
        ///  Used to validate property of the heap. Either smallest
        ///  element should be always root of each tree and sub-tree in a context of min heap
        ///  or largest when dealing with max heap.
        /// </summary>
        protected readonly Comparison<T> _criteriaValidator;

        protected AbstractHeap(T[] array, Comparison<T> criteriaValidator)
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

        protected T GetTop()
        {
            if (_items.Length <= 0)
            {
                throw new ArgumentException("Can't extract elements from empty heap.");
            }

            return _items[0];
        }

        /// <inheritdoc />
        /// <summary>
        ///  Gets total number of elements in the heap.
        /// </summary>
        public int Count { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        ///   Tells if the heap is empty.
        /// </summary>
        public bool IsEmpty => Count == 0;

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
        protected static int Parent(int index)
            => index % 2 == 0 ? index / 2 - 1 : index / 2;

        /// <summary>
        ///  Gets the index of left child of given index.
        /// </summary>
        /// <param name="index">Index to found left child index to.</param>
        /// <returns> Index of the left child of the element with given index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static int Left(int index)
            => (index << 1) + 1;

        /// <summary>
        ///  Gets the index of right child of given index.
        /// </summary>
        /// <param name="index">Index to found right child index to.</param>
        /// <returns> Index of the right child of the element with given index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static int Right(int index)
            => (index << 1) + 2;

        /// <summary>
        ///  Maintains the heap property with the help of <see cref="_criteriaValidator"/> comparison delegate.
        ///  Takes O(logn) time complexity.
        public abstract void Heapify(T[] array, int size, int index);

        /// <inheritdoc />
        /// <remarks> Takes O(logn) time complexity. </remarks>
        public abstract void Add(T element);
    }
}
