using NUnit.Framework;
using System;
namespace MatrixTask
{
    [TestFixture()]
    public class MakeTwoColumnsMethodTests
    {
        [Test()]
        public void StandardCase()
        {
            Matrix actualMatrix = new Matrix(new int[2][] { new int[] { 1, 1 }, new int[] { 1, 1 } });
            int[][] expectedMatrix = { new int[] { 2, 1 }, new int[] { 2, 1 } };
            bool answer = GetNewAnswer(actualMatrix, 0,1,expectedMatrix);
        }

        private bool GetNewAnswer(Matrix actualMatrix,int column1, int column2, int[][] expectedMatrix)
        {
            actualMatrix.MakeTwoColumnsSum(column1,column2);
            return actualMatrix.Equals(expectedMatrix);
        }
    }
}
