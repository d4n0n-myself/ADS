using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Sorts
{
    public class SmoothSort
    {
        public static int iterationsCount;

        public static void RunTests()
        {
            var allData = Helpers.GetData("reversed_10_3.txt", "halfsorted.txt", "data_10_2.txt", "data_10_4.txt", "bigdata_10_7.txt", "data_10_4.txt");
            Helpers.CollectAllStats(Measure, allData.ToArray());
        }

        public static void Sort<T>(T[] sourceArray)
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

                if (Helpers.CompareItems(sourceArray, lngChildIndex, lngNodeIndex - 1) < 0)
                {
                    lngChildIndex = lngNodeIndex - 1;

                    SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                }
                iterationsCount++;

                if (Helpers.CompareItems(sourceArray, lngNodeIndex, lngChildIndex) >= 0)
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
                iterationsCount++;
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
                else if (Helpers.CompareItems(sourceArray, lngPreviousCompleteTreeIndex, lngNodeIndex) <= 0)
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

                        if (Helpers.CompareItems(sourceArray, lngChildIndex, lngNodeIndex - 1) < 0)
                        {
                            lngChildIndex = lngNodeIndex - 1;

                            SmoothDown(ref lngSubTreeSize, ref lngLeftSubTreeSize);
                            lngLeftRightTreeAddress = lngLeftRightTreeAddress * 2;
                        }

                        if (Helpers.CompareItems(sourceArray, lngPreviousCompleteTreeIndex, lngChildIndex) >= 0)
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
                        iterationsCount += 3;
                    }
                }

            }
            SmoothSift(sourceArray, lngNodeIndex, lngSubTreeSize, lngLeftSubTreeSize);
        }

        private static void SmoothSemiTrinkle<T>(IList<T> sourceArray, int lngNodeIndex, int lngLeftRightTreeAddress, int lngSubTreeSize, int lngLeftSubTreeSize)
        {
            int lngIndexTopPreviousCompleteHeap = lngNodeIndex - lngLeftSubTreeSize;
            iterationsCount += 2;

            if (Helpers.CompareItems(sourceArray, lngIndexTopPreviousCompleteHeap, lngNodeIndex) > 0)
            {
                Helpers.SwapItems(sourceArray, lngNodeIndex, lngIndexTopPreviousCompleteHeap);
                iterationsCount += 2;
                SmoothTrinkle(sourceArray, lngIndexTopPreviousCompleteHeap, lngLeftRightTreeAddress, lngSubTreeSize, lngLeftSubTreeSize);
            }
            iterationsCount++;
        }

        private static void Measure<T>(T[] data) where T : IComparable
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Sort(data);
            timer.Stop();
            Console.WriteLine("Time : " + timer.ElapsedMilliseconds);
            Console.WriteLine("Iterations : " + iterationsCount);
            Console.WriteLine();
            iterationsCount = 0;
            timer.Reset();
        }

        private static IEnumerable<double[]> GetData(params string[] paths)
        {
            foreach (var path in paths)
                yield return Array.ConvertAll(File.ReadAllText(path).Split(' '), x => Convert.ToDouble(x));
        }
    }
}