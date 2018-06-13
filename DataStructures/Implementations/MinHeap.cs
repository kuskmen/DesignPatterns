namespace DataStructures.Implementations
{
    using System;
    using DataStructures.Abstractions;
    using DataStructures.Extensions;

    public class MinHeap<T> : AbstractHeap<T>, IPriorityQueue<T>, IMinHeap<T>
    {
        public MinHeap(T[] data, Comparison<T> criteriaValidator) : base(data, criteriaValidator)
        {
        }

        public T GetMin() => GetTop();

        public bool TryGetMin(out T minElement)
        {
            if (_items.Length <= 0)
            {
                minElement = default;
                return false;
            }

            minElement = GetMin();
            return true;
        }
        
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

                if (leftChild < size && _criteriaValidator.Invoke(array[leftChild], array[index]) < 0)
                {
                    bigger = leftChild;
                }

                if (rightChild < size && _criteriaValidator.Invoke(array[rightChild], array[bigger]) < 0)
                {
                    bigger = rightChild;
                }

                if (bigger == index) return;

                array.Swap(bigger, index);

                index = bigger;
            }
        }

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

            while (parent >= 0 && _criteriaValidator.Invoke(_items[child], _items[parent]) < 0)
            {
                _items.Swap(parent, child);
                child = parent;
                parent = Parent(parent);
            }
        }
    }
}
