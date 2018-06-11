namespace DataStructures.Abstractions
{
    /// <summary>
    ///   Represents priority queue data structure.
    /// </summary>
    /// <typeparam name="T">Type of the parameters stored in the priority queue.</typeparam>
    public interface IPriorityQueue<T>
    {
        /// <summary>
        ///  Gets the number of items in the priority queue.
        /// </summary>
        int Count { get; }

        /// <summary>
        ///  Tells if the priority queue is empty or not.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        ///  Adds element to the priority queue.
        /// </summary>
        /// <param name="element">Element to add.</param>
        void Add(T element);

        /// <summary>
        ///  Removes item from the heap at position value of <paramref name="index"/>.
        /// </summary>
        /// <param name="index"> Index at which position element will be removed. </param>
        /// <returns> Removed element. </returns>
        T Remove(int index);
        
        /// <summary>
        ///  Extracts element from the priority queue.
        /// </summary>
        /// <returns> Extracted element.</returns>
        T Extract();

        /// <summary>
        ///  Safe extract method from the priority queue.
        /// </summary>
        /// <param name="element">Element to be extracted.</param>
        /// <returns>If the operation is successful or not.</returns>
        bool TryExtract(out T element);
    }
}
