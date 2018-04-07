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
                    iterationsCount++;

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
                        iterationsCount++;
                    } while (lngSubTreeSize != 1);

                    lngLeftRightTreeAddress = lngLeftRightTreeAddress + 1;
                    iterationsCount++;
                }

                lngOneBasedIndex = lngOneBasedIndex + 1;
                lngNodeIndex = lngNodeIndex + 1;
                iterationsCount += 2;
            }

            SmoothTrinkle(sourceArray, lngNodeIndex, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);

            while (lngOneBasedIndex != 1)
            {
                lngOneBasedIndex = lngOneBasedIndex - 1;
                iterationsCount++;
                if (lngSubTreeSize == 1)
                {
                    lngNodeIndex = lngNodeIndex - 1;
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress - 1;
                    iterationsCount += 2;

                    while (lngLeftRightTreeAddress % 2 == 0)
                    {
                        lngLeftRightTreeAddress = lngLeftRightTreeAddress / 2;
                        iterationsCount++;
                        SmoothUp(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                    }
                }
                else if (lngSubTreeSize >= 3)
                {
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress - 1;
                    lngNodeIndex = lngNodeIndex + lngLeftSubTreeSize - lngSubTreeSize;
                    iterationsCount++;

                    if (lngLeftRightTreeAddress != 0)
                        SmoothSemiTrinkle(sourceArray, lngNodeIndex, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);

                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);

                    lngLeftRightTreeAddress = lngLeftRightTreeAddress * 2 + 1;
                    lngNodeIndex = lngNodeIndex + lngLeftSubTreeSize;
                    iterationsCount += 2;

                    SmoothSemiTrinkle(sourceArray, lngNodeIndex, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);

                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress * 2 + 1;
                    iterationsCount++;
                }
            }
        }
        private static void SmoothUp(ref int lngSubTreeSize, ref int lngLeftSubTreeSize)
        {
            int temp = lngSubTreeSize + lngLeftSubTreeSize + 1;
            lngLeftSubTreeSize = lngSubTreeSize;
            lngSubTreeSize = temp;
            iterationsCount += 4;
        }
        private static void SmoothDown(ref int lngSubTreeSize, ref int lngLeftSubTreeSize)
        {
            int temp = lngSubTreeSize - lngLeftSubTreeSize - 1;
            lngSubTreeSize = lngLeftSubTreeSize;
            lngLeftSubTreeSize = temp;
            iterationsCount += 4;
        }
        private static void SmoothSift<T>(IList<T> sourceArray, int lngNodeIndex, int lngSubTreeSize, int lngLeftSubTreeSize)
        {
            iterationsCount++;
            int lngChildIndex;

            while (lngSubTreeSize >= 3)
            {
                lngChildIndex = lngNodeIndex - lngSubTreeSize + lngLeftSubTreeSize;

                if (CompareItems(sourceArray, lngChildIndex, lngNodeIndex - 1) < 0)
                {
                    lngChildIndex = lngNodeIndex - 1;
                    iterationsCount++;
                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                }

                if (CompareItems(sourceArray, lngNodeIndex, lngChildIndex) >= 0)
                {
                    lngSubTreeSize = 1;
                    iterationsCount++;
                }
                else
                {
                    SwapItems(sourceArray, lngNodeIndex, lngChildIndex);

                    lngNodeIndex = lngChildIndex;
                    iterationsCount++;
                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                }
            }
        }
        private static void SmoothTrinkle<T>(IList<T> sourceArray, int lngNodeIndex, int lngLeftRightTreeAddress, int lngSubTreeSize, int lngLeftSubTreeSize)
        {
            iterationsCount++;
            int lngChildIndex;
            int lngPreviousCompleteTreeIndex;

            while (lngLeftRightTreeAddress > 0)
            {
                while (lngLeftRightTreeAddress % 2 == 0)
                {
                    lngLeftRightTreeAddress = lngLeftRightTreeAddress / 2;
                    iterationsCount++;
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
                        SwapItems(sourceArray, lngNodeIndex, lngPreviousCompleteTreeIndex);
                        lngNodeIndex = lngPreviousCompleteTreeIndex;
                        iterationsCount++;
                    }
                    else if (lngSubTreeSize >= 3)
                    {
                        lngChildIndex = lngNodeIndex - lngSubTreeSize + lngLeftSubTreeSize;

                        if (CompareItems(sourceArray, lngChildIndex, lngNodeIndex - 1) < 0)
                        {
                            lngChildIndex = lngNodeIndex - 1;

                            SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                            lngLeftRightTreeAddress = lngLeftRightTreeAddress * 2;
                            iterationsCount += 2;
                        }

                        if (CompareItems(sourceArray, lngPreviousCompleteTreeIndex, lngChildIndex) >= 0)
                        {
                            SwapItems(sourceArray, lngNodeIndex, lngPreviousCompleteTreeIndex);
                            lngNodeIndex = lngPreviousCompleteTreeIndex;
                            iterationsCount += 2;
                        }
                        else
                        {
                            SwapItems(sourceArray, lngNodeIndex, lngChildIndex);

                            lngNodeIndex = lngChildIndex;
                            SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);

                            lngLeftRightTreeAddress = 0;
                            iterationsCount += 3;

                        }
                    }
                }
                iterationsCount++;
            }
            SmoothSift(sourceArray, lngNodeIndex, lngSubTreeSize, lngLeftSubTreeSize);
        }
        private static void SmoothSemiTrinkle<T>(IList<T> sourceArray, int lngNodeIndex, int lngLeftRightTreeAddress, int lngSubTreeSize, int lngLeftSubTreeSize)
        {
            int lngIndexTopPreviousCompleteHeap = lngNodeIndex - lngLeftSubTreeSize;
            iterationsCount += 2;

            if (CompareItems(sourceArray, lngIndexTopPreviousCompleteHeap, lngNodeIndex) > 0)
            {
                SwapItems(sourceArray, lngNodeIndex, lngIndexTopPreviousCompleteHeap);
                SmoothTrinkle(sourceArray, lngIndexTopPreviousCompleteHeap, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);
            }
        }
        private static void SwapItems<T>(IList<T> arrayToSort, int index1, int index2)
        {
            T temp = arrayToSort[index1];
            arrayToSort[index1] = arrayToSort[index2];
            arrayToSort[index2] = temp;
            iterationsCount += 4;
        }
        private static int CompareItems<T>(IList<T> arrayToSort, int index1, int index2)
        {
            iterationsCount += 2;
            return ((IComparable)arrayToSort[index1]).CompareTo(arrayToSort[index2]);
        }

        private static void Measure(int[] data)
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
    }
}