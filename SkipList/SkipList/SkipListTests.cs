using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SkipList
{
    [TestFixture()]
    public class SkipListTests
    {
        SkipList<int> simpleIntSkipList = new SkipList<int>(Enumerable.Range(1, 5).ToList());

        [Test()]
        public void Check_Creation_InConstructor()
        {
            Assert.AreEqual(2, simpleIntSkipList.LevelsCount);
            Assert.AreEqual(5, simpleIntSkipList.bottomHead.LevelSize);
            Assert.AreEqual(2, simpleIntSkipList.topHead.LevelSize);

            CheckBottomComplexity(simpleIntSkipList, new List<int> { 1, 2, 3, 4, 5 });
        }

        [TestCase(1)]
        [TestCase(4)]
        public void Check_Deletion(int value)
        {
            var mySkipList = new SkipList<int>(Enumerable.Range(1, 5).ToList());
            mySkipList.DeleteElement(value);
            Assert.AreEqual(4, mySkipList.bottomHead.LevelSize);
        }

        [Test]
        public void Check_Find_OnExistedElements()
        {
            CheckBottomComplexity(new SkipList<int>(Enumerable.Range(1, 5).ToList()), new List<int> { 1, 2, 3, 4, 5 });
        }

        [TestCase(-1)]
        [TestCase(6)]
        public void Check_Find_OnNonExistedElements(int value)
        {
            Assert.False(TestFind(simpleIntSkipList, value));
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void Check_Addition_OnLastElement(int value)
        {
            var list = new SkipList<int>(Enumerable.Range(1, 3).ToList());
            list.AddElement(value);
            Assert.AreEqual(4, list.bottomHead.LevelSize);
        }

        [Test]
        public void Check_Addition_OnMiddleElement()
        {
            var list = new SkipList<int>(new List<int>{ 1, 2, 4, 5 });
            list.AddElement(3);
            Assert.AreEqual(5, list.bottomHead.LevelSize);
        }

        [Test]
        public void Check_Addition_OnFirstElement()
        {
            var list = new SkipList<int>(Enumerable.Range(2, 2).ToList());
            list.AddElement(1);
            Assert.AreEqual(3, list.bottomHead.LevelSize);
        }

        [TestCase(1,true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(5, true)]
        [TestCase(0,false)]
        [TestCase(-1, false)]
        public void Check_Contains(int value, bool answer)
        {
            Assert.AreEqual(answer, simpleIntSkipList.Contains(value));
        }

        private bool TestFind<T>(SkipList<T> list, T value) where T : IComparable
        {
            try
            {
                var searchedElement = list.FindElement(value);

                if (searchedElement.Value.CompareTo(value) == 0)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void CheckBottomComplexity<T>(SkipList<T> list, IEnumerable<T> expectedCollection) where T : IComparable
        {
            var iterator = list.bottomHead.Next;

            foreach(var element in expectedCollection)
            {
                Assert.AreEqual(element, iterator.Value);
                iterator = iterator.Next;
            }
        }
    }
}