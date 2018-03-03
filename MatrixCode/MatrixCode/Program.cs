using System;
using System.Collections.Generic;

namespace MatrixCode
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[][] origMatrix = { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };
        
            Matrix matrix = new Matrix(origMatrix);

            matrix.Insert(1, 2, 3);
        }
    }
}
