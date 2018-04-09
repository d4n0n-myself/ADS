using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorts
{
    public static class Sort
    {
        //#region<MyCubeSort>

        //public class Cube
        //{
        //    public  static int maxAxisSize { get; private set; } = 16;
        //    public xNode[] xNodes;
        //    public void AxisSetter(int value)
        //    {
        //        if (value > maxAxisSize)
        //            maxAxisSize = value;
        //    }
        //}

        //public class xNode
        //{
        //    public yNode[] yNodes = new yNode[Cube.maxAxisSize];
        //    //public int key = yNodes[0].key;
        //}
        //public class yNode
        //{
        //    public static numberNode[] array;
        //    public int key;
        //}
        //public class numberNode
        //{
        //    public int[] array;
        //}

        //static void InsertNewValue(Cube cube, int value)
        //{
        //    int xIndex = FindXIndex(cube, 0, cube.xNodes.Length, value);
        //    int yIndex = FindYIndex(cube,xIndex, 0, cube.xNodes[xIndex].yNodes.Length, value);
        //}

        ////static int FindXIndex(Cube cube, int start, int end, int value)
        ////{
        ////    //int mid = start + (end - start) / 2;
        ////    //if (value > cube.xNodes[mid].key)
        ////    //    mid = FindXIndex(cube, mid, end, value);
        ////    //else
        ////    //    mid = FindXIndex(cube, start, mid, value);
        ////    //return mid;
        ////}

        //static int FindYIndex(Cube cube, int xIndex, int start, int end, int value)
        //{
        //    int mid = start + (end - start) / 2;
        //    if (value > cube.xNodes[xIndex].yNodes[mid].key)
        //        mid = FindXIndex(cube, mid, end, value);
        //    else
        //        mid = FindXIndex(cube, start, mid, value);
        //    return mid;
        //}

        //static void CubeSort(this int[] sourceArray)
        //{
        //    Cube cube = new Cube();

        //    foreach (var item in sourceArray)
        //        InsertNewValue(cube, item);

        //}
        //#endregion
    }
}