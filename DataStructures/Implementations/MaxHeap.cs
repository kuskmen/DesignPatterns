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
    public class MaxHeap<T> : AbstractHeap<T>, IPriorityQueue<T>, IMaxHeap<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxHeap{T}"/> class.
        /// </summary>
        /// <param name="array"> Array to create heap from. </param>
        /// <param name="criteriaValidator">Delegate which will determine if the heap property is according to given criteria. </param>
        public MaxHeap(T[] array, Comparison<T> criteriaValidator) : base(array, criteriaValidator)
        {
        }

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
        public override void Add(T element)
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
        /// <exception cref="T:System.ArgumentException">Throws exception if heap is empty.</exception>
        public T GetMax() => GetTop();

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
        public override void Heapify(T[] array, int size, int index)
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
