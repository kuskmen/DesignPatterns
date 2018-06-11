namespace DataStructures.Abstractions
{
    public interface IMaxHeap<T>
    {
        /// <summary>
        ///  Returns the highest priority element without removing it.
        ///  Time complexity of this method is O(1).
        /// </summary>
        /// <returns> Element of type <typeparamref name="T"/></returns>
        /// 
        T GetMax();

        /// <summary>
        ///  Returns the highest priority element without removing it safely.
        ///  Time complexity of this method is O(1).
        /// </summary>
        /// <param name="maxElement"> Highest priority element.</param>
        /// <returns> If the opration is successfull.</returns>
        bool TryGetMax(out T maxElement);
    }
}
