using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Sorts
{
    public class SmoothSort
    {
        public static int iterationsCount;

        public static void RunTests()
        {
            var midForStandartData = Array.ConvertAll(File.ReadAllText("data_10_4.txt").Split(' '), x => Convert.ToDouble(x));
            var reversedData = Array.ConvertAll(File.ReadAllText("reversed_10_3.txt").Split(' '), x => Convert.ToDouble(x));
            var halfSortedData = Array.ConvertAll(File.ReadAllText("halfsorted.txt").Split(' '), x => Convert.ToDouble(x));
            var smallData = Array.ConvertAll(File.ReadAllText("data_10_2.txt").Split(' '), x => Convert.ToDouble(x));
            var midData = Array.ConvertAll(File.ReadAllText("data_10_4.txt").Split(' '), x => Convert.ToDouble(x));
            var bigData = Array.ConvertAll(File.ReadAllText("bigdata_10_7.txt").Split(' '), x => Convert.ToDouble(x));
            var randomData = Helpers.CreateRandomData();

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

        public static void Execute<T>(T[] sourceArray)
        {
            int lngOneBasedIndex = 1;
            int lngNodeIndex = 0;
            int lngLeftRightTreeAddress = 1;
            int lngSubTreeSize = 1;
            int lngLeftSubTreeSize = 1;

            while (lngOneBasedIndex != sourceArray.Length)
            {
                if (lngLeftRightTreeAddress % 8 == 3)
                {
                    SmoothSift(sourceArray, lngNodeIndex, lngSubTreeSize, lngLeftSubTreeSize);
                    lngLeftRightTreeAddress = (lngLeftRightTreeAddress + 1) / 4;


                    SmoothUp(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                    SmoothUp(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                }
                else if (lngLeftRightTreeAddress % 4 == 1)
                {
                    if (lngOneBasedIndex + lngLeftSubTreeSize < sourceArray.Length)
                        SmoothSift(sourceArray, lngNodeIndex, lngSubTreeSize, lngLeftSubTreeSize);
                    else
                        SmoothTrinkle(sourceArray, lngNodeIndex, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);

                    do
                    {
                        SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                        lngLeftRightTreeAddress = lngLeftRightTreeAddress * 2;

                    } while (lngSubTreeSize != 1);

                    lngLeftRightTreeAddress = lngLeftRightTreeAddress + 1;

                }

                lngOneBasedIndex = lngOneBasedIndex + 1;
                lngNodeIndex = lngNodeIndex + 1;
            }

            SmoothTrinkle(sourceArray, lngNodeIndex, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);

            while (lngOneBasedIndex != 1)
            {
                lngOneBasedIndex = lngOneBasedIndex - 1;

                if (lngSubTreeSize == 1)
                {
                    lngNodeIndex = lngNodeIndex - 1;
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress - 1;

                    while (lngLeftRightTreeAddress % 2 == 0)
                    {
                        lngLeftRightTreeAddress = lngLeftRightTreeAddress / 2;

                        SmoothUp(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                    }
                }
                else if (lngSubTreeSize >= 3)
                {
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress - 1;
                    lngNodeIndex = lngNodeIndex + lngLeftSubTreeSize - lngSubTreeSize;


                    if (lngLeftRightTreeAddress != 0)
                        SmoothSemiTrinkle(sourceArray, lngNodeIndex, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);

                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);

                    lngLeftRightTreeAddress = lngLeftRightTreeAddress * 2 + 1;
                    lngNodeIndex = lngNodeIndex + lngLeftSubTreeSize;

                    SmoothSemiTrinkle(sourceArray, lngNodeIndex, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);

                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress * 2 + 1;

                }
            }
        }

        private static void SmoothUp(ref int lngSubTreeSize, ref int lngLeftSubTreeSize)
        {
            int temp = lngSubTreeSize + lngLeftSubTreeSize + 1;
            lngLeftSubTreeSize = lngSubTreeSize;
            lngSubTreeSize = temp;
        }
        private static void SmoothDown(ref int lngSubTreeSize, ref int lngLeftSubTreeSize)
        {
            int temp = lngSubTreeSize - lngLeftSubTreeSize - 1;
            lngSubTreeSize = lngLeftSubTreeSize;
            lngLeftSubTreeSize = temp;
        }
        private static void SmoothSift<T>(IList<T> sourceArray, int lngNodeIndex, int lngSubTreeSize, int lngLeftSubTreeSize)
        {

            int lngChildIndex;

            while (lngSubTreeSize >= 3)
            {
                lngChildIndex = lngNodeIndex - lngSubTreeSize + lngLeftSubTreeSize;

                if (CompareItems(sourceArray, lngChildIndex, lngNodeIndex - 1) < 0)
                {
                    lngChildIndex = lngNodeIndex - 1;

                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                }

                if (CompareItems(sourceArray, lngNodeIndex, lngChildIndex) >= 0)
                {
                    lngSubTreeSize = 1;

                }
                else
                {
                    Helpers.SwapItems(sourceArray, lngNodeIndex, lngChildIndex);
                    iterationsCount += 2;

                    lngNodeIndex = lngChildIndex;

                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                }
            }
        }

        private static void SmoothTrinkle<T>(IList<T> sourceArray, int lngNodeIndex, int lngLeftRightTreeAddress, int lngSubTreeSize, int lngLeftSubTreeSize)
        {

            int lngChildIndex;
            int lngPreviousCompleteTreeIndex;

            while (lngLeftRightTreeAddress > 0)
            {
                while (lngLeftRightTreeAddress % 2 == 0)
                {
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress / 2;

                    SmoothUp(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                }

                lngPreviousCompleteTreeIndex = lngNodeIndex - lngSubTreeSize;

                if (lngLeftRightTreeAddress == 1)
                {
                    lngLeftRightTreeAddress = 0;
                }
                else if (CompareItems(sourceArray, lngPreviousCompleteTreeIndex, lngNodeIndex) <= 0)
                {
                    lngLeftRightTreeAddress = 0;
                }
                else
                {
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress - 1;

                    if (lngSubTreeSize == 1)
                    {
                        Helpers.SwapItems(sourceArray, lngNodeIndex, lngPreviousCompleteTreeIndex);
                        iterationsCount += 2;
                        lngNodeIndex = lngPreviousCompleteTreeIndex;

                    }
                    else if (lngSubTreeSize >= 3)
                    {
                        lngChildIndex = lngNodeIndex - lngSubTreeSize + lngLeftSubTreeSize;

                        if (CompareItems(sourceArray, lngChildIndex, lngNodeIndex - 1) < 0)
                        {
                            lngChildIndex = lngNodeIndex - 1;

                            SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                            lngLeftRightTreeAddress = lngLeftRightTreeAddress * 2;
                        }

                        if (CompareItems(sourceArray, lngPreviousCompleteTreeIndex, lngChildIndex) >= 0)
                        {
                            Helpers.SwapItems(sourceArray, lngNodeIndex, lngPreviousCompleteTreeIndex);
                            iterationsCount += 2;
                            lngNodeIndex = lngPreviousCompleteTreeIndex;
                        }
                        else
                        {
                            Helpers.SwapItems(sourceArray, lngNodeIndex, lngChildIndex);
                            iterationsCount += 2;

                            lngNodeIndex = lngChildIndex;
                            SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);

                            lngLeftRightTreeAddress = 0;

                        }
                    }
                }

            }
            SmoothSift(sourceArray, lngNodeIndex, lngSubTreeSize, lngLeftSubTreeSize);
        }

        private static void SmoothSemiTrinkle<T>(IList<T> sourceArray, int lngNodeIndex, int lngLeftRightTreeAddress, int lngSubTreeSize, int lngLeftSubTreeSize)
        {
            int lngIndexTopPreviousCompleteHeap = lngNodeIndex - lngLeftSubTreeSize;
            iterationsCount += 2;

            if (CompareItems(sourceArray, lngIndexTopPreviousCompleteHeap, lngNodeIndex) > 0)
            {
                Helpers.SwapItems(sourceArray, lngNodeIndex, lngIndexTopPreviousCompleteHeap);
                iterationsCount += 2;
                SmoothTrinkle(sourceArray, lngIndexTopPreviousCompleteHeap, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);
            }
        }



        private static int CompareItems<T>(IList<T> arrayToSort, int index1, int index2)
        {
            iterationsCount++;
            return ((IComparable)arrayToSort[index1]).CompareTo(arrayToSort[index2]);
        }

        private static void Measure<T>(T[] data)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Execute(data);
            timer.Stop();
            Console.WriteLine("Time : " + timer.ElapsedTicks);
            Console.WriteLine("Iterations : " + iterationsCount);
            Console.WriteLine();
            iterationsCount = 0;
            timer.Reset();
        }

        private static void CollectStats<T>(T[] data)
        {
            Console.WriteLine(nameof(data));
            Measure(data);
        }

        private static void CollectAllStats<T>(params T[][] data) where T : IComparable
        {
            foreach (var e in data)
                Helpers.CollectStats(e, Measure);
        }
    }
}