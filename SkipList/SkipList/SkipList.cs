﻿using System;
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

            foreach(var element in originalList.Skip(1))  // general lvl creation
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
            throw new NotImplementedException();
        }

        public void DeleteElement(T value)
        {
            var elementToDeletion = FindElement(value);

            while (elementToDeletion != null)
            {
                elementToDeletion.Previous = elementToDeletion.Next;
                elementToDeletion = elementToDeletion.Below;
            }

        }

        public SkipListNode FindElement(T value) // Accessibility ??? 
        {
            var searchedElement = new SkipListNode();
            var headerSearcher = topHead;

            //find starter lvl
            while (headerSearcher.Next.Value.CompareTo(value) > 0)
                headerSearcher = headerSearcher.Below;

            searchedElement = headerSearcher.Next;

            // find element 
            while (searchedElement.Value.CompareTo(value) != 0)
            {
                if (searchedElement.Value.CompareTo(value) > 0)
                {
                    if (searchedElement.Below == null) // if list doesn't contain element, throw exception
                        throw new ArgumentException();
                    searchedElement = searchedElement.Below;
                }
                else
                    searchedElement = searchedElement.Next;
            }

            return searchedElement;
        }

        internal class Header
        {
            public SkipListNode Next;
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