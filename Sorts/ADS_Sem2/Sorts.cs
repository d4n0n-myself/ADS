using System;
using System.Collections.Generic;
//using System.Linq;

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

        #region<CubeSort>
        const int standartOuterSize = 128;
        const int standartMiddleSize = 64;
        const int standartInnerSize = 64;

        public class Cube
        {
            public int[] OuterFloor;//= new int[standartOuterSize];
            public OuterNode[] outers;// = new OuterNode[standartOuterSize];
            public int[] MiddlesSize;// = new int[standartMiddleSize].Select(x => x = standartMiddleSize).ToArray();
            public int OutersSize;
        }

        public class OuterNode //w_node
        {
            public int[] MiddleFloor;// = new int[standartOuterSize];
            public MiddleNode[] middles;// = new MiddleNode[standartOuterSize];
            public int[] innersSize;// = new int[standartInnerSize].Select(x => x = standartInnerSize).ToArray();
        }

        public class MiddleNode // x_node
        {
            public int[] InnerFloor;// = new int[standartMiddleSize];
            public InnerNode[] inners;// = new InnerNode[standartMiddleSize];
            public int[] innerSizes;// = new int[standartMiddleSize].Select(x => x = standartInnerSize).ToArray();
        }

        public class InnerNode // y_node
        {
            public int[] innerArray;// = new int[standartInnerSize];
        }

        static void InsertInnerNode(Cube cube, int key)
        {
            int mid, w, x, y, z;

            if (cube.OutersSize != 0)
                goto alert;

            cube.OuterFloor = new int[standartMiddleSize];
            cube.outers = new OuterNode[standartMiddleSize];
            cube.MiddlesSize = new int[standartMiddleSize];

            OuterNode outer = cube.outers[0] = new OuterNode();

            MiddleNode middler = outer.middles[0] = new MiddleNode();

            InnerNode inner = middler.inners[0] = new InnerNode();

            middler.innerSizes[0] = 0;

            cube.OutersSize = cube.MiddlesSize[0] = outer.innersSize[0] = 1;

            w = x = y = z = 0;

            cube.OuterFloor[0] = outer.MiddleFloor[0] = middler.InnerFloor[0] = key;

            goto insert;

        alert:


            //alert:
            //if (cube.OutersSize == 0)
            //{

            //}

            if (key < cube.OuterFloor[0])
            {
                outer = cube.outers[0];
                middler = outer.middles[0];
                inner = middler.inners[0];

                w = x = y = z = 0;

                cube.OuterFloor[0] = outer.MiddleFloor[0] = middler.InnerFloor[0] = key;

                goto insert;
            }

            //find outer by w

            mid = w = cube.OutersSize - 1;
            while (mid > 7)
            {
                mid /= 2;

                if (key < cube.OuterFloor[w - mid]) w -= mid;
            }
            while (key < cube.OuterFloor[w]) --w;
            outer = cube.outers[w] ?? new OuterNode();

            //find middler by x

            mid = x = (cube.MiddlesSize[w] - 1);

            while (mid > 7)
            {
                mid /= 2;
                if (key < outer.MiddleFloor[x - mid])
                    x -= mid;
            }
            while (key < outer.MiddleFloor[x])
                x--;

            middler = outer.middles[x] ?? new MiddleNode();

            //find inner by y

            mid = y = mid == 0 ? 0 : outer.innersSize[x] - 1;

            while (mid > 7)
            {
                mid /= 4;

                if (key < middler.InnerFloor[y - mid])
                {
                    y -= mid;
                    if (key < middler.InnerFloor[y - mid])
                    {
                        y -= mid;
                        if (key < middler.InnerFloor[y - mid])
                        {
                            y -= mid;
                        }
                    }
                }
            }
            while (key < middler.InnerFloor[y])
                y--;

            inner = middler.inners[y] ?? new InnerNode();

            // z ? 

            mid = z = (middler.innerSizes[y] - 1);

            while (mid > 7)
            {
                mid /= 4;

                if (key < inner.innerArray[z - mid])
                {
                    z -= mid;

                    if (key < inner.innerArray[z - mid])
                    {
                        z -= mid;

                        if (key < inner.innerArray[z - mid])
                            z -= mid;
                    }
                }
            }
            while (key < inner.innerArray[z])
                z--;




            //  Uncomment to filter duplicates
            /*
            if (key == inner.innerArray[z])
            {
                return;
            }*/

            //z++;


            insert:
            middler.innerSizes[y]++;

            //if (z + 1 != middler.innerSizes[y])
            //{
            //    memmove(&y_node->z_keys[z + 1], &y_node->z_keys[z], (middler.innerSizes[y] - z - 1) * sizeof(int));
            //}

            inner.innerArray[z] = key;

            if (middler.innerSizes[y] == standartInnerSize)
            {
                split_y_node(outer, x, y);

                if (outer.innersSize[x] == standartMiddleSize)
                {
                    split_x_node(cube, w, x);

                    if (cube.MiddlesSize[w] == standartOuterSize)
                        SplitOuterNode(cube, w);
                }
            }
        }

        static void InsertOuterNode(Cube cube, int w)
        {
            cube.OutersSize++;

            //if (cube->w_size % BSC_M == 0)
            //{
            //    cube->w_floor = (int*) realloc(cube->w_floor, (cube->w_size + BSC_M) * sizeof(int));
            //    cube->w_axis = (struct w_node **) realloc(cube->w_axis, (cube->w_size + BSC_M) * sizeof(struct w_node *));
            //    cube->x_size = (unsigned char*) realloc(cube->x_size, (cube->w_size + BSC_M) * sizeof(unsigned char));
            //}

            //if (w + 1 != cube->w_size)
            //{
            //    memmove(&cube->w_floor[w + 1], &cube->w_floor[w], (cube->w_size - w - 1) * sizeof(int));
            //    memmove(&cube->w_axis[w + 1], &cube->w_axis[w], (cube->w_size - w - 1) * sizeof(struct w_node *));
            //    memmove(&cube->x_size[w + 1], &cube->x_size[w], (cube->w_size - w - 1) * sizeof(unsigned char));
            //}

            //cube->w_axis[w] = (struct w_node *) malloc(sizeof(struct w_node));
        }

        static void SplitOuterNode(Cube cube, int w)
        {
            //struct w_node * w_node1, * w_node2;
            OuterNode outer1, outer2;

            InsertOuterNode(cube, w + 1);
            //insert_w_node(cube, w + 1);

            outer1 = cube.outers[w];
            outer2 = cube.outers[w + 1];
            //    w_node1 = cube->w_axis[w];
            //w_node2 = cube->w_axis[w + 1];

            cube.MiddlesSize[w + 1] = (byte)(cube.MiddlesSize[w] / 2);
            cube.MiddlesSize[w] -= cube.MiddlesSize[w + 1];
            //cube->x_size[w + 1] = cube->x_size[w] / 2;
            //cube->x_size[w] -= cube->x_size[w + 1];

            //memcpy(&w_node2->x_floor[0], &w_node1->x_floor[cube->x_size[w]], cube->x_size[w + 1] * sizeof(int));
            //    memcpy(&w_node2->x_axis[0], &w_node1->x_axis[cube->x_size[w]], cube->x_size[w + 1] * sizeof(struct x_node *));
            //memcpy(&w_node2->y_size[0], &w_node1->y_size[cube->x_size[w]], cube->x_size[w + 1] * sizeof(unsigned char));

            //cube->w_floor[w + 1] = w_node2->x_floor[0];
            cube.OuterFloor[w + 1] = outer2.MiddleFloor[0];
        }

        static void insert_x_node(Cube cube, int w, int x)
        {
            //struct w_node * w_node = cube->w_axis[w];
            OuterNode outer = cube.outers[w];



            //short x_size = ++cube->x_size[w];


            //if (x_size != x + 1)
            //{
            //    memmove(&w_node->x_floor[x + 1], &w_node->x_floor[x], (x_size - x - 1) * sizeof(int));
            //    memmove(&w_node->x_axis[x + 1], &w_node->x_axis[x], (x_size - x - 1) * sizeof(struct x_node *));
            //    memmove(&w_node->y_size[x + 1], &w_node->y_size[x], (x_size - x - 1) * sizeof(unsigned char));
            //}

            //w_node->x_axis[x] = (struct x_node *) malloc(sizeof(struct x_node));
        }

        static void split_x_node(Cube cube, int w, int x)
        {
            OuterNode outer = cube.outers[w];
            MiddleNode middler1, middler2;
            //    struct w_node * w_node = cube->w_axis[w];
            //struct x_node * x_node1, * x_node2;

            insert_x_node(cube, w, (x + 1));

            middler1 = outer.middles[x];
            middler2 = outer.middles[x + 1];
            //;x_node1 = w_node->x_axis[x];
            //x_node2 = w_node->x_axis[x + 1];
            outer.innersSize[x + 1] = (byte)(outer.innersSize[x] / 2);
            outer.innersSize[x] = outer.innersSize[x + 1];
            //w_node->y_size[x + 1] = w_node->y_size[x] / 2;
            //w_node->y_size[x] -= w_node->y_size[x + 1];

            //    memcpy(&x_node2->y_floor[0], &x_node1->y_floor[w_node->y_size[x]], w_node->y_size[x + 1] * sizeof(int));
            //memcpy(&x_node2->y_axis[0], &x_node1->y_axis[w_node->y_size[x]], w_node->y_size[x + 1] * sizeof(struct y_node *));
            //memcpy(&x_node2->z_size[0], &x_node1->z_size[w_node->y_size[x]], w_node->y_size[x + 1] * sizeof(unsigned char));

            outer.MiddleFloor[x + 1] = middler2.InnerFloor[0];
            //w_node->x_floor[x + 1] = x_node2->y_floor[0];
        }

        static void insert_y_node(OuterNode outer, int x, int y)
        {
            MiddleNode middler = outer.middles[x];
            //struct x_node * x_node = w_node->x_axis[x];

            int y_size = outer.innersSize[x]++;

            //if (y_size != y + 1)
            //{
            //    memmove(&x_node->y_floor[y + 1], &x_node->y_floor[y], (y_size - y - 1) * sizeof(int));
            //    memmove(&x_node->y_axis[y + 1], &x_node->y_axis[y], (y_size - y - 1) * sizeof(struct y_node *));
            //    memmove(&x_node->z_size[y + 1], &x_node->z_size[y], (y_size - y - 1) * sizeof(unsigned char));
            //}

            //x_node->y_axis[y] = (struct y_node *) malloc(sizeof(struct y_node));
        }

        static void split_y_node(OuterNode outer, int x, int y)
        {
            MiddleNode middler = outer.middles[x];
            InnerNode inner1, inner2;
            //    struct x_node * x_node = w_node->x_axis[x];
            //struct y_node * y_node1, * y_node2;

            insert_y_node(outer, x, (y + 1));

            inner1 = middler.inners[y] ?? new InnerNode();
            inner2 = middler.inners[y + 1] ?? new InnerNode();
            //y_node1 = x_node->y_axis[y];
            //y_node2 = x_node->y_axis[y + 1];

            middler.innerSizes[y + 1] = (byte)(middler.innerSizes[y] / 2);
            middler.innerSizes[y] -= middler.innerSizes[y + 1];
            //x_node->z_size[y + 1] = x_node->z_size[y] / 2;
            //x_node->z_size[y] -= x_node->z_size[y + 1];

            //memcpy(&y_node2->z_keys[0], &y_node1->z_keys[x_node->z_size[y]], x_node->z_size[y + 1] * sizeof(int));
            middler.InnerFloor[y + 1] = inner2.innerArray[0];
        }

        public static void CubeSort(this int[] sourceArray, int startIndex, int size)
        {
            Cube cube = new Cube();
            int count;

            if (size > 1000000)
            {
                for (count = 100000; count + 100000 < size; count += 100000)
                    CubeSort(sourceArray, count, 100000);

                CubeSort(sourceArray, count, size - count);
            }

            for (count = 0; count < size; count++)
                InsertInnerNode(cube, count);


            //destroy_cube(cube, array);
        }
        #endregion

        //#region<cubesort>

        //public class Cube<T>
        //{
        //    public Cube()
        //    {
        //        //lookupTable = new Node<T>[16, 16];
        //    }

        //    public readonly int maxAxisSize = 16;
        //    //public Node<T>[,] lookupTable;
        //}

        //public class OuterNode<T>
        //{
        //    public OuterNode()
        //    {

        //    }
        //    public T key;
        //}
        //public class MiddleNode<T>
        //{
        //    public MiddleNode()
        //    {
        //        array = new T[16];
        //        key = array[0];
        //    }
        //    public T[] array;
        //    public T key;
        //}
        //public class InnerNode<T>
        //{
        //    public InnerNode()
        //    {
        //        array = new T[16];
        //        key = array[0];
        //    }
        //    public T[] array;
        //    public T key;
        //}

        //static Cube<T> CreateCube<T>(this T[] array)
        //{
        //    Cube<T> cube = new Cube<T>();

        //    return cube;
        //}

        //static void CubeSort<T>(this T[] sourceArray)
        //{
        //    var array = new int[16];

        //}
        //#endregion

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

        static void Preparation(this int[] array, int currentIndex, int size)
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

        static void Change(this int[] array, int oldIndex, int newIndex)
        {
            int temp = array[oldIndex];
            array[oldIndex] = array[newIndex];
            array[newIndex] = temp;
        }
        #endregion


    }
}