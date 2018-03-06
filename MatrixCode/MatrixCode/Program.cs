using System;
using System.Collections.Generic;

namespace MatrixTask
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int[][] origMatrix = { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] {7,8,9} };
            Matrix matrix = new Matrix(origMatrix);
            Console.WriteLine("Original");
            PrintResult(matrix);

            matrix.Insert(1,1,4);
            Console.WriteLine("Insert");
            PrintResult(matrix);

            var diagonalElementsSum = matrix.GetDiagonalElementsSum();
            Console.WriteLine("Diagonal elements sum == {diagonalElementsSum}");

            matrix.Delete(0,0);
            Console.WriteLine("Delete");
            PrintResult(matrix);

            matrix.MakeTwoColumnsSum(0,1);
            Console.WriteLine("MakeTwoColumnsSum");
            PrintResult(matrix);

            var listOfMinimal = matrix.GetListOfMinimaInColumns();
            Console.WriteLine("List of minimal elements in columns");
            Console.WriteLine(string.Join(" ",listOfMinimal) + "\n");

            matrix.Transpose();
            Console.WriteLine("Transposed matrix");
            PrintResult(matrix);

            var decodedMatrix = matrix.GetMatrix(); //Decode
            Console.Write(string.Join(" ", (object)decodedMatrix));
        }

        private static void PrintResult(Matrix matrix)
        {
            matrix.PrintMatrix();
            Console.Write("\n\n");
        }
    }
}
