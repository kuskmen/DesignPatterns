﻿using DataStructures;
using System;
using System.Collections.Generic;
using DataStructures.Heap;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new[] { 2, 7, 26, 25, 19, 17, 1, 90, 3, 36 };

            var heap = new Heap<int>(arr, HeapType.Max).Sort();
        }
    }
}
