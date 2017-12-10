using DataStructures;
using System;
using System.Collections.Generic;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 };

            var heap = new MaxHeap<int>(arr);
        }
    }
}
