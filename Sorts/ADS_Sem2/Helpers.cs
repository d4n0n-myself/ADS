using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Sorts
{
    public class Helpers
    {
        public static double[] CreateRandomData(int exponent) => CreateRandomData(exponent, 1000);
        public static double[] CreateRandomData(int exponent, int maxValue)
        {
            List<double> arr = new List<double>();
            Random rnd = new Random();
            for (var i = 0; i < Math.Pow(10, exponent); i++)
                arr.Add(rnd.Next(maxValue));
            return arr.ToArray();
        }

        public static void SwapItems<T>(IList<T> arrayToSort, int index1, int index2)
        {
            T temp = arrayToSort[index1];
            arrayToSort[index1] = arrayToSort[index2];
            arrayToSort[index2] = temp;
        }

        public static int CompareItems<T>(IList<T> arrayToSort, int index1, int index2) =>
            ((IComparable)arrayToSort[index1]).CompareTo(arrayToSort[index2]);

        public static void CollectAllStats<T>(Action<T[]> measure, params T[][] data) where T : IComparable
        {
            foreach (var e in data)
                CollectStats(e, measure);
        }

        public static void CollectAllStats(Action<MyLinkedList> measure, MyLinkedList data)
        {
            CollectStats(data, measure);
        }

        public static IEnumerable<double[]> GetData(params string[] paths)
        {
            foreach (var path in paths)
                yield return Array.ConvertAll(File.ReadAllText(path).Split(' '), x => Convert.ToDouble(x));
        }

        public static IEnumerable<double[]> GetRandomData(params int[] powers)
        {
            foreach (var power in powers)
                yield return CreateRandomData(power);
        }

        private static void CollectStats(MyLinkedList data, Action<MyLinkedList> action)
        {
            //Console.WriteLine(nameof(data));
            action(data);
        }

        private static void CollectStats<T>(T[] data, Action<T[]> action) where T : IComparable
        {
            //Console.WriteLine(nameof(data));
            action(data);
        }

        private static void CollectStats<T>(LinkedList<T> data, Action<LinkedList<T>> action) where T : IComparable
        {
            //Console.WriteLine(nameof(data));
            action(data);
        }
    }
}