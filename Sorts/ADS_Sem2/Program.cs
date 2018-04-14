using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorts
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("QuickSort");
            //QuickSort.RunTests();

            //Console.WriteLine("SmoothSort");
            //SmoothSort.RunTests();

            Console.WriteLine("MergeSort by LinkedLists");
            var array = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            foreach (var e in array)
                Console.Write(e + " ");
            
            var linkedList = new LinkedList<double>();

            foreach(var e in array)
                linkedList.AddLast(e);

            Console.WriteLine();
            linkedList.Print();

            linkedList = linkedList.Sort();

            Console.WriteLine();
            linkedList.Print();
        }
    }
}