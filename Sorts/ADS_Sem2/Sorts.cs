using System;
using System.Collections.Generic;

namespace Sorts
{
    public static class Sort
    {
        #region<Quicksort>
        public static void QuickSort(this int[] sourceArray, int leftBorder, int rightBorder)
        {
            int leftIndex = leftBorder;
            int rightIndex = rightBorder;
            int middleElement = sourceArray[leftBorder + (rightBorder - leftBorder) / 2];

            while (leftIndex <= rightIndex)
            {
                while (sourceArray[leftIndex] < middleElement)
                    leftIndex++;

                while (sourceArray[rightIndex] > middleElement)
                    rightIndex--;

                if (leftIndex <= rightIndex)
                {
                    int tempElement = sourceArray[leftIndex];
                    sourceArray[leftIndex] = sourceArray[rightIndex];
                    sourceArray[rightIndex] = tempElement;
                    leftIndex++;
                    rightIndex--;
                }
            }

            if (leftIndex < rightBorder)
                QuickSort(sourceArray, leftIndex, rightBorder);
            if (leftBorder < rightIndex)
                QuickSort(sourceArray, leftBorder, rightIndex);
        }
        #endregion

        public static void CubeSort(this int[] sourceArray)
        {
            
        }

        #region<HeapSort>
        public static void HeapSort(this int[] array)
        {
            for (int i = array.Length / 2 - 1; i >= 0; i--)
                array.Preparation(i, array.Length);

            for (int i = array.Length - 1; i >= 1; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                array.Preparation(0, i - 1);
            }
        }

        private static void Preparation(this int[] array, int currentIndex, int size)
        {
            int heapDepth;
            var newIndex = currentIndex * 2;

            while (newIndex <= size)
            {
                if (newIndex == size)
                    heapDepth = newIndex;
                else if (array[newIndex] > array[newIndex + 1])
                    heapDepth = newIndex;
                else
                    heapDepth = newIndex + 1;

                if (array[currentIndex] < array[heapDepth])
                {
                    array.Change(currentIndex, heapDepth);
                    currentIndex = heapDepth;
                }
                else
                    return;
            }
        }

        private static void Change(this int[] array, int oldIndex, int newIndex)
        {
            int temp = array[oldIndex];
            array[oldIndex] = array[newIndex];
            array[newIndex] = temp;
        }
        #endregion
    }
}