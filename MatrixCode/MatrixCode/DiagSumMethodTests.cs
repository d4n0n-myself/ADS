﻿using NUnit.Framework;
using System;
namespace MatrixTask
{
    [TestFixture()]
    public class DiagSumMethodTests
    {
        [Test]
        public void CheckDiagSum_InMatrixBy3_Equals3()
        {
            Matrix matrix = new Matrix(new int[3][] { new int[] { 1, 0, 0 }, new int[] { 0, 1, 0 }, new int[] { 0, 0, 1 } });
            Assert.AreEqual(3,matrix.GetDiagonalElementsSum());
        }
        [Test()]
        public void CheckDiagSum_InMatrixBy4_Equals8()
        {
            Matrix matrix = new Matrix(new int[4][] { new int[] { 1, 0, 0, 1 }, new int[] { 0, 1, 1, 0 } , new int[] {0,1,1,0}, new int[] {1,0,0,1}});
            Assert.AreEqual(8, matrix.GetDiagonalElementsSum());
        }


    }
}
