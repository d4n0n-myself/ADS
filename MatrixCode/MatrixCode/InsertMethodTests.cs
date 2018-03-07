using NUnit.Framework;
using System;

namespace MatrixTask
{
    [TestFixture()]
    public class InsertMethodTests
    {
        [Test()]
        public void CheckInsert_InFirstLine_SecondPlace()
        {
            Matrix original = new Matrix(new int[3][] { new int[] { 1, 0, 0 }, new int[3], new int[3] });
            var expected = new int[3][] { new int[] { 1, 0, 4 }, new int[3], new int[3] };
            bool answer = GetNewAnswer(original, 0, 2, 4, expected);
            Assert.AreEqual(true, answer);
        }

        [Test]
        public void CheckInsert_InSecondLine_SecondPlace()
        {
            Matrix original = new Matrix(new int[3][] { new int[3], new int[3], new int[3] });
            var expected = new int[3][] { new int[3], new int[] { 0, 1, 0 }, new int[3] };
            bool answer = GetNewAnswer(original, 1, 1, 1, expected);
            Assert.AreEqual(true, answer);
        }

        [Test]
        public void CheckInsert_InThirdLine_ThirdPlace()
        {
            Matrix original = new Matrix(new int[3][] { new int[3], new int[3], new int[3] });
            var expected = new int[3][] { new int[3], new int[3], new int[] { 0, 0, 1 } };
            bool answer = GetNewAnswer(original, 2, 2, 1, expected);
            Assert.AreEqual(true, answer);
        }

        private bool GetNewAnswer(Matrix original, int i, int j, int value, int[][] expected)
        {
            original.Insert(i, j, value);
            return original.Equals(expected);
        }
    }
}