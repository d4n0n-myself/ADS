using NUnit.Framework;
using System;
namespace MatrixCode
{
    [TestFixture()]
    public class InsertTests
    {
        [Test()]
        public void StandardCase()
        {
            int[][] matrix = 
            { 
                new int[] { 1, 0, 0 }, 
                new int[] { 0, 0, 0 }, 
                new int[] { 0, 0, 0 } 
            };
            MatrixCode original = new MatrixCode(matrix);

            int[][] correctMatrix = 
            {
                new int[] { 1, 0, 4 }, 
                new int[] { 0, 0, 0 }, 
                new int[] { 0, 0, 0 } 
            };
            MatrixCode expected = new MatrixCode(correctMatrix);

            CheckInsertMethod(original, expected, 0, 2, 4);
        }

        private void CheckInsertMethod(MatrixCode original, MatrixCode expected, int line, int column, int value)
        {
            original.Insert(line, column, value);
            bool answer = original.Equals(expected);
            Assert.AreEqual(true, answer);
        }
    }
}