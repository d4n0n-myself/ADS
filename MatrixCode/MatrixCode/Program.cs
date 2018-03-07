using System;
using System.Collections.Generic;

namespace MatrixTask
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[][] origMatrix = { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } };

            Matrix matrix = new Matrix(origMatrix);

            Console.WriteLine("Decode");
            PrintResult(matrix);

            matrix.Insert(1, 1, 4);
            Console.WriteLine("Insert [1,1]");
            PrintResult(matrix);

            var diagonalElementsSum = matrix.GetDiagonalElementsSum();
            Console.WriteLine("Diagonal elements sum == {0}\n\n", diagonalElementsSum);

            matrix.Delete(2, 2);
            Console.WriteLine("Delete [2,2]");
            PrintResult(matrix);

            matrix.MakeTwoColumnsSum(0, 1);
            Console.WriteLine("MakeTwoColumnsSum with column1 == 0 & column2 == 1");
            PrintResult(matrix);

            var listOfMinimal = matrix.GetListOfMinimalInColumns();
            Console.WriteLine("List of minimal elements in columns");
            Console.WriteLine(string.Join(" ", listOfMinimal) + "\n");

            matrix.Transpose();
            Console.WriteLine("Transposed matrix");
            PrintResult(matrix);
        }

        private static void PrintResult(Matrix matrix)
        {
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    var exec = matrix[i, j];
                    Console.Write(exec + " ");
                }
                Console.Write("\n");
            }

            Console.Write("\n\n");
        }
    }
}