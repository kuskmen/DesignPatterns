using DataStructures.Implementations;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new[] { 2, 7, 26, 25, -19, -17, 1, 90, 3, 36 };
            var heap = new MaxHeap<int>(arr, (x, y) => x > y ? 1 : y > x ? -1 : 0);

            heap.Remove(1);

        }
    }
}
