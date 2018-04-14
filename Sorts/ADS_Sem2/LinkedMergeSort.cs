using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Sorts
{
    public static class LinkedMergeSort
    {
        public static int iterationsCount;

        public static void RunTests<T>(LinkedList<T> data) where T : IComparable
        {
            Console.WriteLine("Time in ticks");

            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //Array.Sort(midForStandartData);
            //timer.Stop();
            //Console.WriteLine("Standard Sort: " + timer.ElapsedTicks);
            //Console.WriteLine();
            //timer.Reset();

            Helpers.CollectAllStats(Measure, data);
        }

        public static void Print<T>(this LinkedList<T> list)
        {
            foreach (var e in list)
                Console.Write(e + " ");
        }

        public static LinkedList<T> Sort<T>(this LinkedList<T> input) where T : IComparable
        {
            LinkedList<T> result = new LinkedList<T>();
            LinkedList<T> left = new LinkedList<T>();
            LinkedList<T> right = new LinkedList<T>();

            if (input.Count <= 1)
                return input;

            int midpoint = input.Count / 2;

            foreach (var e in input.Take(midpoint))
                left.AddLast(e);
            foreach (var e in input.Skip(midpoint))
                right.AddLast(e);

            left = Sort(left);
            right = Sort(right);
            result = Merge(left, right);

            return result;
        }

        private static LinkedList<T> Merge<T>(LinkedList<T> left, LinkedList<T> right) where T : IComparable
        {
            var leftIndex = left.First;
            var rightIndex = right.First;
            var result = new LinkedList<T>();

            while (leftIndex != null && rightIndex != null)
            {
                if (leftIndex.Value.CompareTo(rightIndex.Value) < 0)
                {
                    result.AddLast(leftIndex.Value);
                    leftIndex = leftIndex.Next;
                }
                else
                {
                    result.AddLast(rightIndex.Value);
                    rightIndex = rightIndex.Next;
                }
            }

            while (leftIndex != null)
            {
                result.AddLast(leftIndex.Value);
                leftIndex = leftIndex.Next;
            }

            while (rightIndex != null)
            {
                result.AddLast(rightIndex.Value);
                rightIndex = rightIndex.Next;
            }

            return result;
        }

        private static void Measure<T>(LinkedList<T> data) where T : IComparable
        {
            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //Sort(data);
            //timer.Stop();
            //Console.WriteLine("Time : " + timer.ElapsedTicks);
            //Console.WriteLine("Iterations : " + iterationsCount);
            //Console.WriteLine();
            //iterationsCount = 0;
            //timer.Reset();

            Sort(data);
            data.Print();
        }

        private static void SwapValues<T>(LinkedListNode<T> leftIndex, LinkedListNode<T> rightIndex)
        {
            T temp = leftIndex.Value;
            leftIndex.Value = rightIndex.Value;
            rightIndex.Value = temp;
        }
    }
}