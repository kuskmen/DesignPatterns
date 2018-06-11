namespace DataStructures.Implementations
{
    using System;
    using DataStructures.Abstractions;

    public class MinHeap<T> : AbstractHeap<T>, IPriorityQueue<T>, IMinHeap<T>
    {
        public MinHeap(T[] data, Comparison<T> criteriaValidator) : base(data, criteriaValidator)
        {

        }

        public T GetMin()
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetMin(out T minElement)
        {
            throw new System.NotImplementedException();
        }

        public override void Heapify(T[] array, int size, int index)
        {
            throw new NotImplementedException();
        }

        public override void Add(T element)
        {
            throw new NotImplementedException();
        }
    }
}
