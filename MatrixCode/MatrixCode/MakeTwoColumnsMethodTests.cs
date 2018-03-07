using NUnit.Framework;
using System;
namespace MatrixTask
{
    [TestFixture()]
    public class MakeTwoColumnsMethodTests
    {
        [Test()]
        public void CheckAddition_InMatrixBy2()
        {
            Matrix actualMatrix = new Matrix(new int[2][] { new int[] { 1, 1 }, new int[] { 1, 1 } });
            int[][] expectedMatrix = { new int[] { 2, 1 }, new int[] { 2, 1 } };
            bool answer = GetNewAnswer(actualMatrix, 0, 1, expectedMatrix);
        }

        [Test()]
        public void CheckAddition_InMatrixBy3()
        {
            Matrix actualMatrix = new Matrix(new int[3][] { new int[] { 1, 1,1 }, new int[] { 1, 1,1 }, new int[] {2,1,1} });
            int[][] expectedMatrix = { new int[] { 2, 1, 1 }, new int[] { 2, 1, 1 }, new int[] { 3, 1, 1 } };
            bool answer = GetNewAnswer(actualMatrix, 0, 1, expectedMatrix);
        }

        [Test]
        public void CheckAddition_InUnsortedMatrixBy3()
        {
            Matrix actualMatrix = new Matrix(new int[3][] { new int[] { 3,1,5 }, new int[] { 4,2,9 }, new int[] { 7,8,6 } });
            int[][] expectedMatrix = { new int[] { 3,1,6 }, new int[] { 4,2,11 }, new int[] { 7,8,14 } };
            bool answer = GetNewAnswer(actualMatrix, 2, 1, expectedMatrix);
        }

        private bool GetNewAnswer(Matrix actualMatrix, int column1, int column2, int[][] expectedMatrix)
        {
            actualMatrix.MakeTwoColumnsSum(column1, column2);
            return actualMatrix.Equals(expectedMatrix);
        }
    }
}