using System;
namespace Sorts
{
    public static class HeapSort
    {
        public static void Execute(this int[] array)
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
                    Helpers.SwapItems(array, currentIndex, heapDepth);
                    currentIndex = heapDepth;
                }
                else
                    return;
            }
        }
    }
}