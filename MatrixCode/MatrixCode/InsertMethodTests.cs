using NUnit.Framework;
using System;
namespace MatrixCode
{
    [TestFixture()]
    public class InsertMethodTests
    {
        [Test()]
        public void StandardCase()
        {
            MatrixCode original = new MatrixCode(new int[3][] { new int[] { 1, 0, 0 }, new int[3], new int[3] });
            MatrixCode expected = new MatrixCode(new int[3][] { new int[] { 1, 0, 4 }, new int[3], new int[3] });
            bool answer = GetNewAnswer(original, 0, 2, 4, expected);
            Assert.AreEqual(true, answer);
        }

        [Test]
        public void SecondLineElementCheckCase()
        {
            MatrixCode original = new MatrixCode(new int[3][] { new int[3], new int[3], new int[3] });
            MatrixCode expected = new MatrixCode(new int[3][] { new int[3], new int[] { 0, 1, 0 }, new int[3] });
            bool answer = GetNewAnswer(original, 1, 1, 1, expected);
            Assert.AreEqual(true,answer);
        }

        private bool GetNewAnswer(MatrixCode original, int i, int j, int value, MatrixCode expected)
        {
            original.Insert(i, j, value);
            return original.Equals(expected);
        }
    }
}