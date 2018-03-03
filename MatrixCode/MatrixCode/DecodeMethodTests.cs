using NUnit.Framework;
using System;
namespace MatrixCode
{
    [TestFixture()]
    public class DecodeMethodTests
    {
        [Test()]
        public void CheckDecodeMethod_InMatrixBy3()
        {
            int[][] originalMatrix = { new int[] { 1, 1, 1 }, new int[] { 0, 0, 0 }, new int[] { 1, 0, 1 } };
            MatrixCode matrix = new MatrixCode(originalMatrix);
            bool answer = GetNewAnswer(matrix, originalMatrix);
            Assert.AreEqual(true,answer);
        }

        [Test]
        public void CheckDecodeMethod_InMatrixBy2()
        {
            int[][] originalMatrix = { new int[] { 1, 2 }, new int[] { 3, 4 } };
            MatrixCode matrix = new MatrixCode(originalMatrix);
            bool answer = GetNewAnswer(matrix, originalMatrix);
            Assert.AreEqual(true,answer);
        }

        private bool GetNewAnswer(MatrixCode matrix,int[][] expected)
        {
            var actualMatrix = matrix.Decode();

            for (int i = 0; i < actualMatrix.Length;i++)
                for (int j = 0; j < actualMatrix[0].Length;j++)
                    if (actualMatrix[i][j] != expected[i][j]) return false;

            return true;
        }
    }
}