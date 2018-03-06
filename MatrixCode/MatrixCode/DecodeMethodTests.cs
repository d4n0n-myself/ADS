using NUnit.Framework;
using System;

namespace MatrixTask
{
    [TestFixture()]
    public class DecodeMethodTests
    {
        [Test()]
        public void CheckDecodeMethod_InMatrixBy3()
        {
            int[][] originalMatrix = { new int[] { 1, 1, 1 }, new int[] { 0, 0, 0 }, new int[] { 1, 0, 1 } };
            Matrix matrix = new Matrix(originalMatrix);
            bool answer = GetNewAnswer(matrix, originalMatrix);
            Assert.AreEqual(true,answer);
        }

        [Test]
        public void CheckDecodeMethod_InMatrixBy2()
        {
            int[][] originalMatrix = { new int[] { 1, 2 }, new int[] { 3, 4 } };
            Matrix matrix = new Matrix(originalMatrix);
            bool answer = GetNewAnswer(matrix, originalMatrix);
            Assert.AreEqual(true,answer);
        }

        private bool GetNewAnswer(Matrix matrix, int[][] expected) => matrix.Equals(expected);
    }
}