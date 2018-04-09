using System;
using System.Collections.Generic;

namespace Sorts
{
    public class Helpers
    {
        public static double[] CreateRandomData()
        {
            List<double> arr = new List<double>();
            Random rnd = new Random();
            for (var i = 0; i < Math.Pow(10, 2); i++)
                arr.Add(rnd.Next());
            return arr.ToArray();
        }

        public static void CollectStats<T>(T[] data, Action<T[]> action) where T : IComparable
        {
            Console.WriteLine(nameof(data));
            action(data);
        }

        public static void SwapItems<T>(IList<T> arrayToSort, int index1, int index2)
        {
            T temp = arrayToSort[index1];
            arrayToSort[index1] = arrayToSort[index2];
            arrayToSort[index2] = temp;
            //iterationsCount += 2;
        }
    }
}