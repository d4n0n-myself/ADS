using NUnit.Framework;
using System;

namespace MatrixTask
{
    [TestFixture()]
    public class DeleteMethodTests
    {
        [Test()]
        public void CheckDeleteMethod_ToFirstElement_InMatrixBy3()
        {
            Matrix origMatrix = new Matrix(new int[3][] { new int[] { 1, 0, 0 }, new int[3], new int[3] });
            Matrix expMatrix = new Matrix(new int[3][] { new int[3], new int[3], new int[3] });
            bool answer = GetNewAnswer(origMatrix, 0, 0, expMatrix);
            Assert.AreEqual(true, answer);
        }

        [Test]
        public void CheckDeleteMethod_ToElementInSecondLine_InMatrixBy3()
        {
            Matrix original = new Matrix(new int[3][] { new int[3], new int[] {0,1,0}, new int[3] });
            Matrix expected = new Matrix(new int[3][] { new int[3], new int[3], new int[3] });
            bool answer = GetNewAnswer(original, 1, 1, expected);
            Assert.AreEqual(true,answer);
        }

        [Test]
        public void CheckDeleteMethod_ToFirstElementInSecondLine_InMatrixBy2()
        {
            Matrix original = new Matrix(new int[2][] {new int[2], new int[] {1,0}});
            Matrix expected = new Matrix(new int[2][] { new int[2], new int[2]});
            bool answer = GetNewAnswer(original, 1, 0, expected);
            Assert.AreEqual(true,answer);
        }

        private bool GetNewAnswer(Matrix original, int i, int j, Matrix expected)
        {
            original.DeleteElementFromMatrix(i,j);
            return original.Equals(expected);
        }
    }
}