using System;
using System.Collections.Generic;

namespace MatrixCode
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[][] matrix = { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };
        
            var matrixCode = new MatrixCode(matrix);

            //matrixCode.ColsSum();
            //matrixCode.Decode();
            //matrixCode.Delete();
            //matrixCode.DiagSum();
            matrixCode.Insert(1,2,13);
            //matrixCode.MinList();
            //matrixCode.Transp();

            List<int> list = new List<int>();
        }
    }
}
