using NUnit.Framework;
using System.Collections.Generic;

namespace MatrixTask
{
    [TestFixture()]
    public class ListOfMinimaOfColumnsMethodTests
    {
        [Test()]
        public void CheckList_InMatrixBy2()
        {
            Matrix actual = new Matrix(new int[2][] { new int[] { 1, 2 }, new int[] { 2, 1 } });
            List<int> expected = new List<int>();
            expected.AddRange(new int[] { 1, 1 });
            bool answer = GetNewAnswer(actual, expected);
            Assert.AreEqual(true, answer);
        }

        [Test()]
        public void CheckList_InMatrixBy3()
        {
            Matrix actual = new Matrix(new int[3][] { new int[] { 1, 3, 5 }, new int[] { 1, 6, 20 }, new int[] { 4, 65, 2 } });
            List<int> expected = new List<int>();
            expected.AddRange(new int[] { 1, 3, 2 });
            bool answer = GetNewAnswer(actual, expected);
            Assert.AreEqual(true, answer);
        }

        [Test()]
        public void CheckList_InMatrixBy4()
        {
            Matrix actual = new Matrix(new int[4][] { new int[] { 13, 3, 5, 9 }, new int[] { 1, 60, 20, 100 }, new int[] { 423414141, 21235, 2231, 456787 }, new int[] { 133, 22323, 3111111, 453535 } });
            List<int> expected = new List<int>();
            expected.AddRange(new int[] { 1, 3, 5, 9 });
            bool answer = GetNewAnswer(actual, expected);
            Assert.AreEqual(true, answer);
        }

        private bool GetNewAnswer(Matrix actual, List<int> expected)
        {
            var result = actual.GetListOfMinimaOfColumns();

            for (int i = 0; i < expected.Count; i++)
                if (result[i] != expected[i])
                    return false;

            return true;
        }
    }
}