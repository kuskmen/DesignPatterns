namespace DataStructures.Abstractions
{
    public interface IMinHeap<T>
    {

        /// <summary>
        ///  Returns the highest priority element without removing it.
        ///  Time complexity of this method is O(1).
        /// </summary>
        /// <returns> Element of type <typeparamref name="T"/></returns>
        T GetMin();
        
        /// <summary>
        ///  Returns the highest priority element without removing it safely.
        ///  Time complexity of this method is O(1).
        /// </summary>
        /// <param name="minElement"> Highest priority element.</param>
        bool TryGetMin(out T minElement);
    }
}
