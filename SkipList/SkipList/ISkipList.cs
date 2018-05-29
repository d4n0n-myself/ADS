using System;
using System.Collections.Generic;

namespace SkipList
{
    public interface ISkipList<T> where T : IComparable
    {
        void AddElement(T value);
        void DeleteElement(T value);
        bool Contains(T value);
        SkipList<T>.SkipListNode FindElement(T value);
    }
}
