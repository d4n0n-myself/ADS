using NUnit.Framework;
using System;

namespace MatrixTask
{
    [TestFixture()]
    public class TransposeMethodTests
    {
        [Test()]
        public void CheckTranspose_InMatrixBy2()
        {
            Matrix actualMatrix = new Matrix(new int[2][] { new int[] { 1, 2 }, new int[] { 3, 4 } });
            var expected = new int[2][] { new int[] { 1, 3 }, new int[] { 2, 4 } };
            bool answer = GetNewAnswer(actualMatrix, expected);
            Assert.AreEqual(true, answer);
        }

        [Test]
        public void CheckTranspose_InMatrixBy3()
        {
            Matrix actualMatrix = new Matrix(new int[3][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } });
            var expected = new int[3][] { new int[] { 1, 4, 7 }, new int[] { 2, 5, 8 }, new int[] { 3, 6, 9 } };
            bool answer = GetNewAnswer(actualMatrix, expected);
            Assert.AreEqual(true, answer);
        }

        [Test]
        public void CheckTranspose_InUnsortedMatrixBy3()
        {
            Matrix actualMatrix = new Matrix(new int[3][] { new int[] { 3, 4, 7 }, new int[] { 1, 2, 8 }, new int[] { 5, 9, 6 } });
            var expected = new int[3][] { new int[] { 3, 1, 5 }, new int[] { 4, 2, 9 }, new int[] { 7, 8, 6 } };
            bool answer = GetNewAnswer(actualMatrix, expected);
            Assert.AreEqual(true, answer);
        }

        private bool GetNewAnswer(Matrix actual, int[][] expected)
        {
            actual.Transpose();
            return actual.Equals(expected);
        }
    }
}