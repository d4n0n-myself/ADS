using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Sorts
{
    public static class QuickSort
    {
        public static int iterationsCount;

        public static void RunTests()
        {
            var randomData = Helpers.GetRandomData(new[] { 2, 4, 6 });
            var allData = Helpers.GetData("reversed_10_3.txt", "halfsorted.txt", "data_10_2.txt", "data_10_4.txt", "bigdata_10_7.txt", "data_10_4.txt");

            //Stopwatch timer = new Stopwatch();
            //Console.WriteLine("Time in ticks");

            //timer.Start();
            //Array.Sort(midForStandartData);
            //timer.Stop();
            //Console.WriteLine("Standard Sort: " + timer.ElapsedTicks);
            //Console.WriteLine();
            //timer.Reset();

            Helpers.CollectAllStats(Measure, allData.ToArray());
        }

        public static void Sort<T>(this T[] sourceArray, int leftBorder, int rightBorder) where T : IComparable
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
                Sort(sourceArray, leftIndex, rightBorder);
            if (leftBorder < rightIndex)
                Sort(sourceArray, leftBorder, rightIndex);
        }

        private static void Measure<T>(T[] data) where T : IComparable
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Sort(data, 0, data.Length - 1);
            timer.Stop();
            Console.WriteLine("Time : " + timer.ElapsedTicks);
            Console.WriteLine("Iterations : " + iterationsCount);
            Console.WriteLine();
            iterationsCount = 0;
            timer.Reset();
        }
    }
}