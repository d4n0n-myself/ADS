using System;

namespace Sorts
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("QuickSort");
            QuickSort.RunTests();

            Console.WriteLine("SmoothSort");
            SmoothSort.RunTests();
        }
    }
}