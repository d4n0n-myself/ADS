﻿using NUnit.Framework;
using System;
namespace MatrixCode
{
    [TestFixture()]
    public class DeleteMethodTests
    {
        [Test()]
        public void CheckDeleteMethod_ToFirstElement_InMatrixBy3()
        {
            MatrixCode origMatrix = new MatrixCode(new int[3][] { new int[] { 1, 0, 0 }, new int[3], new int[3] });
            MatrixCode expMatrix = new MatrixCode(new int[3][] { new int[3], new int[3], new int[3] });
            bool answer = GetNewAnswer(origMatrix, 0, 0, expMatrix);
            Assert.AreEqual(true, answer);
        }

        [Test]
        public void CheckDeleteMethod_ToElementInSecondLine_InMatrixBy3()
        {
            MatrixCode original = new MatrixCode(new int[3][] { new int[3], new int[] {0,1,0}, new int[3] });
            MatrixCode expected = new MatrixCode(new int[3][] { new int[3], new int[3], new int[3] });
            bool answer = GetNewAnswer(original, 1, 1, expected);
            Assert.AreEqual(true,answer);
        }

        [Test]
        public void CheckDeleteMethod_ToFirstElementInSecondLine_InMatrixBy2()
        {
            MatrixCode original = new MatrixCode(new int[2][] {new int[2], new int[] {1,0}});
            MatrixCode expected = new MatrixCode(new int[2][] { new int[2], new int[2]});
            bool answer = GetNewAnswer(original, 1, 0, expected);
            Assert.AreEqual(true,answer);
        }

        private bool GetNewAnswer(MatrixCode original, int i, int j, MatrixCode expected)
        {
            original.Delete(i,j);
            return original.Equals(expected);
        }
    }
}