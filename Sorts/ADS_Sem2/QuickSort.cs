using System;
using System.Diagnostics;
using System.IO;

namespace Sorts
{
    public static class QuickSort
    {
        public static int iterationsCount;

        public static void RunTests()
        {
            var reversedData = GetData("reversed_10_3.txt");
            var halfSortedData = GetData("halfsorted.txt");
            var smallData = GetData("data_10_2.txt");
            var midData = GetData("data_10_4.txt");
            var bigData = GetData("bigdata_10_7.txt");
            var randomData = Helpers.CreateRandomData();
            var midForStandartData = GetData("data_10_4.txt");

            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Time in ticks");

            timer.Start();
            Array.Sort(midForStandartData);
            timer.Stop();
            Console.WriteLine("Standard Sort: " + timer.ElapsedTicks);
            Console.WriteLine();
            timer.Reset();

            CollectAllStats(smallData, midData, bigData, reversedData, halfSortedData, randomData);
        }

        public static void Execute<T>(this T[] sourceArray, int leftBorder, int rightBorder) where T : IComparable
        {
            iterationsCount++;
            int leftIndex = leftBorder;
            int rightIndex = rightBorder;
            var middleElement = sourceArray[leftBorder + (rightBorder - leftBorder) / 2];

            while (leftIndex <= rightIndex)
            {
                while (sourceArray[leftIndex].CompareTo(middleElement) < 0)
                    leftIndex++;

                while (sourceArray[rightIndex].CompareTo(middleElement) > 0)
                    rightIndex--;

                if (leftIndex <= rightIndex)
                {
                    var tempElement = sourceArray[leftIndex];
                    sourceArray[leftIndex] = sourceArray[rightIndex];
                    sourceArray[rightIndex] = tempElement;
                    leftIndex++;
                    rightIndex--;
                    iterationsCount += 2;
                }
            }

            if (leftIndex < rightBorder)
                Execute(sourceArray, leftIndex, rightBorder);
            if (leftBorder < rightIndex)
                Execute(sourceArray, leftBorder, rightIndex);
        }

        private static void Measure<T>(T[] data) where T : IComparable
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Execute(data, 0, data.Length - 1);
            timer.Stop();
            Console.WriteLine("Time : " + timer.ElapsedTicks);
            Console.WriteLine("Iterations : " + iterationsCount);
            Console.WriteLine();
            iterationsCount = 0;
            timer.Reset();
        }

        private static void CollectAllStats<T>(params T[][] data) where T : IComparable
        {
            foreach (var e in data)
                Helpers.CollectStats(e, Measure);
        }

        private static double[] GetData(string path) => Array.ConvertAll(File.ReadAllText(path).Split(' '), x => Convert.ToDouble(x));
    }
}