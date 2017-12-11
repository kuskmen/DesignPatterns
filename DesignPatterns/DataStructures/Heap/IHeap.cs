namespace DataStructures.Heap
{
    using System.Collections.Generic;

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