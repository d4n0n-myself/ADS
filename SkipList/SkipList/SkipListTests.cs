using NUnit.Framework;
using System;
using System.Linq;

namespace SkipList
{
    [TestFixture()]
    public class SkipListTests
    {
        [Test()]
        public void TestCase()
        {
            var mySkipList = new SkipList<int>(Enumerable.Range(1, 5).ToList());

            Assert.AreEqual(2, mySkipList.LevelsCount);
            Assert.AreEqual(5, mySkipList.bottomHead.LevelSize);
            Assert.AreEqual(2, mySkipList.topHead.LevelSize);
        }
    }
}