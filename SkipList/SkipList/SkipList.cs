using System;
using System.Collections.Generic;
using System.Linq;

namespace SkipList
{
    public class SkipList<T> : ISkipList<T> where T : IComparable
    {
        internal Header topHead;
        internal Header bottomHead;

        public int LevelsCount
        {
            get
            {
                var result = 0;
                var iterator = topHead;
                while (iterator != null)
                {
                    result++;
                    iterator = iterator.Below;
                }

                return result;
            }
        }

        public SkipList(List<T> originalList)
        {
            var iterator = new SkipListNode(originalList[0]);
            bottomHead = new Header { Next = iterator };
            topHead = bottomHead;

            foreach (var element in originalList.Skip(1))  // general lvl creation
            {
                var newElement = new SkipListNode(element);
                iterator.Next = newElement;
                newElement.Previous = iterator;
                iterator = iterator.Next;
            }

            while (topHead.LevelSize > 2) // Sub-levels building
            {
                var newHeader = new Header();
                newHeader.Below = topHead;
                topHead.Above = newHeader;
                SkipListNode upIterator = new SkipListNode();
                iterator = topHead.Next;
                bool first = true;

                for (var i = 1; iterator != null; i++) // prev list iteration
                {
                    if (i % 2 == 0)
                    {
                        if (first)
                        {
                            upIterator = new SkipListNode(iterator);
                            newHeader.Next = upIterator;
                            first = false;
                        }
                        else
                        {
                            var temp = new SkipListNode(iterator);
                            upIterator.Next = temp;
                            temp.Previous = upIterator;
                            upIterator = upIterator.Next;
                        }
                        upIterator.Below = iterator;
                        iterator.Above = upIterator;
                    }
                    iterator = iterator.Next;
                }

                topHead = newHeader;
            }
        }

        public void AddElement(T value)
        {
            var iterator = bottomHead.Next;
            bool firstChanged = false;

            while (true) // update bottom
            {
                if (value.CompareTo(iterator.Value) > 0 && iterator.Next != null)
                    iterator = iterator.Next;
                else
                {
                    var temp = new SkipListNode(value);

                    if (iterator.Previous == null)
                    {
                        bottomHead.Next = temp;
                        temp.Next = iterator;
                        firstChanged = true;
                    }
                    else if (iterator.Next == null)
                    {
                        temp.Next = iterator.Next;
                        iterator.Next = temp;
                        temp.Previous = iterator;
                    }
                    else
                    {
                        temp.Next = iterator;
                        temp.Previous = iterator.Previous;
                        iterator.Previous.Next = temp;
                        iterator.Previous = temp;
                    }
                    break;
                }
            }

            if (firstChanged)
            {
                var header = bottomHead.Above;
                while (header != null)
                {
                    var newFirst = new SkipListNode(value);
                    newFirst.Next = header.Next;
                    header.Next.Previous = newFirst;
                    header.Next = newFirst;
                }
            }
            else
            {
                var rnd = new Random();
                var previous = iterator.Next;

                if (iterator.Above == null)
                    while (iterator != null && iterator.Above == null)
                        iterator = iterator.Previous;

                iterator = iterator.Above;

                while (iterator != null)
                {
                    if (rnd.NextDouble() > 0)
                    {
                        var newElement = new SkipListNode(value);
                        newElement.Next = iterator.Next;
                        newElement.Previous = iterator;
                        iterator.Next = newElement;
                        newElement.Below = previous;
                        previous.Above = newElement;
                        previous = newElement;
                        iterator = iterator.Above;
                    }
                    else break;
                }
            }
        }

        public bool Contains(T value)
        {
            try
            {
                FindElement(value);
            }
            catch(ArgumentException)
            {
                return false;
            }
            return true;
        }

        public void DeleteElement(T value)
        {
            var elementToDeletion = FindElement(value);

            while (elementToDeletion != null)
            {
                if (elementToDeletion.Previous == null) // bc of implementation w/ headers
                {
                    var header = topHead;
                    while (header.Next != elementToDeletion)
                        header = header.Below;
                    header.Next = header.Next.Next;
                }
                else
                    elementToDeletion.Previous.Next = elementToDeletion.Next;
                elementToDeletion = elementToDeletion.Below;
            }
        }

        public SkipListNode FindElement(T value) // Accessibility ??? 
        {
            var searchedElement = new SkipListNode();
            var headerSearcher = topHead;

            //find starter lvl
            while (headerSearcher.Next.Value.CompareTo(value) > 0 && headerSearcher.Below != null)
                headerSearcher = headerSearcher.Below;

            searchedElement = headerSearcher.Next;

            // find element 
            while (searchedElement.Value.CompareTo(value) != 0)
            {
                if (searchedElement.Next != null &&
                    (searchedElement.Next.Value.CompareTo(value) < 0 || searchedElement.Next.Value.CompareTo(value) == 0))
                {
                    searchedElement = searchedElement.Next;
                }
                else
                {
                    if (searchedElement.Below != null)
                        searchedElement = searchedElement.Below;
                    else throw new ArgumentException();
                }
            }

            return searchedElement;
        }

        internal class Header
        {
            public SkipListNode Next;
            public Header Above;
            public Header Below;

            public int LevelSize
            {
                get
                {
                    var result = 0;
                    var iterator = Next;
                    while (iterator != null)
                    {
                        result++;
                        iterator = iterator.Next;
                    }
                    return result;
                }
            }
        }

        public class SkipListNode
        {
            public T Value { get; }

            public SkipListNode Next;
            public SkipListNode Previous;
            public SkipListNode Above;
            public SkipListNode Below;

            public SkipListNode()
            { }

            public SkipListNode(T value)
            {
                Value = value;
            }

            public SkipListNode(SkipListNode node)
            {
                Value = node.Value;
            }
        }
    }
}