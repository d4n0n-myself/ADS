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
            var midForStandartData = Array.ConvertAll(File.ReadAllText("data_10_4.txt").Split(' '), x => Convert.ToInt32(x));
            var reversedData = Array.ConvertAll(File.ReadAllText("reversed_10_3.txt").Split(' '), x => Convert.ToInt32(x));
            var halfSortedData = Array.ConvertAll(File.ReadAllText("halfsorted.txt").Split(' '), x => Convert.ToInt32(x));
            var smallData = Array.ConvertAll(File.ReadAllText("data_10_2.txt").Split(' '), x => Convert.ToInt32(x));
            var midData = Array.ConvertAll(File.ReadAllText("data_10_4.txt").Split(' '), x => Convert.ToInt32(x));
            var bigData = Array.ConvertAll(File.ReadAllText("bigdata_10_7.txt").Split(' '), x => Convert.ToInt32(x));

            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Time in ticks");

            timer.Start();
            Array.Sort(midForStandartData);
            timer.Stop();
            Console.WriteLine("Standard Sort: " + timer.ElapsedTicks);
            Console.WriteLine();
            timer.Reset();

            Console.WriteLine(nameof(smallData));
            Measure(smallData);
            Console.WriteLine(nameof(midData));
            Measure(midData);
            Console.WriteLine(nameof(bigData));
            Measure(bigData);
            Console.WriteLine(nameof(reversedData));
            Measure(reversedData);
            Console.WriteLine(nameof(halfSortedData));
            Measure(halfSortedData);
        }

        public static void Execute(this int[] sourceArray, int leftBorder, int rightBorder)
        {
            iterationsCount++;
            rightBorder -= 1;
            int leftIndex = leftBorder;
            int rightIndex = rightBorder;
            int middleElement = sourceArray[leftBorder + (rightBorder - leftBorder) / 2];

            while (leftIndex <= rightIndex)
            {
                while (sourceArray[leftIndex] < middleElement)
                {
                    leftIndex++;
                    iterationsCount++;
                }

                while (sourceArray[rightIndex] > middleElement)
                {
                    rightIndex--;
                    iterationsCount++;
                }

                if (leftIndex <= rightIndex)
                {
                    int tempElement = sourceArray[leftIndex];
                    sourceArray[leftIndex] = sourceArray[rightIndex];
                    sourceArray[rightIndex] = tempElement;
                    leftIndex++;
                    rightIndex--;
                    iterationsCount += 5;
                }
            }

            if (leftIndex < rightBorder)
                Execute(sourceArray, leftIndex, rightBorder);
            if (leftBorder < rightIndex)
                Execute(sourceArray, leftBorder, rightIndex);
        }

        private static void Measure(int[] data)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Execute(data, 0, data.Length);
            timer.Stop();
            Console.WriteLine("Time : " + timer.ElapsedTicks);
            Console.WriteLine("Iterations : " + iterationsCount);
            Console.WriteLine();
            iterationsCount = 0;
            timer.Reset();
        }

    }
}
