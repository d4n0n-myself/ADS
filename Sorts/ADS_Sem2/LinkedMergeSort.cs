using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Sorts
{
    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }

        public Node()
        {}

        public Node(int value)
        {
            this.Value = value;
        }
    }

    public class MyLinkedList
    {
        public MyLinkedList()
        {
            _first = null;
        }

        private Node _first;

        public static int iterationsCount { get; private set; }
        public Node First
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
            }
        }
        public Node Last
        {
            get
            {
                Node node = _first;
                while (node.Next != null) 
                    node = node.Next; 
                return node;
            }
        }

        public static void RunTests()
        {
            List<int[]> data = new List<int[]>();

            string[] paths = { "afsd10^6.txt" };
            foreach (var path in paths)
                data.Add(Array.ConvertAll(File.ReadAllText(path).Split(' '), x => Convert.ToInt32(x)));

            foreach (var e in data)
            {
                var linkedList = CreateLinkedList(e);
                Helpers.CollectAllStats(Measure, linkedList);
            }
        }


        public static MyLinkedList CreateLinkedList(int[] data)
        {
            var list = new MyLinkedList();
            foreach (var e in data)
                list.AddLast(e);

            return list;
        }

        public static Node Sort(Node start)
        {
            iterationsCount++;
            if (start == null || start.Next == null)
                return start;

            var midpoint = GetMiddle(start);
            var afterMiddle = midpoint.Next;
            midpoint.Next = null;

            return Merge(Sort(start), Sort(afterMiddle));
        }

        public bool IsEmpty
        {
            get
            {
                return _first == null;
            }
        }

        public void Print()
        {
            Node temp = _first;

            while (temp != null)
            {
                Console.Write(temp.Value + " ");
                temp = temp.Next;

            }
        }

        public void RemoveFirst()
        {
            Node temp = _first;
            if (_first != null)
                _first = _first.Next;

        }

        public void InsertAfter(Node link)
        {
            if (link == null)
                return;
            
            Node newLink = new Node();
            link.Next = newLink;
        }

        public void AddLast(int value)
        {
            Node temp = _first;

            if (temp == null)
            {
                temp = new Node(value);
                _first = temp;
                return;
            }

            while (temp.Next != null)
                temp = temp.Next;

            Node newNode = new Node(value);
            temp.Next = newNode;
        }

        private static Node Merge(Node left, Node right)
        {
            iterationsCount++;

            Node head = new Node();
            Node pointer = head;

            while (left != null && right != null)
            {
                if (left.Value < right.Value)
                {
                    pointer.Next = left;
                    pointer = left;
                    left = left.Next;
                    iterationsCount += 2;
                }
                else
                {
                    pointer.Next = right;
                    pointer = right;
                    right = right.Next;
                    iterationsCount += 2;
                }
            }

            if (right == null)
                pointer.Next = left;
            else
                pointer.Next = right;
            iterationsCount++;

            head = head.Next;
            return head;
        }

        private static Node GetMiddle(Node head)
        {
            iterationsCount++;
            if (head == null || head.Next == null)
                return head;

            Node slow, fast;
            slow = head;
            fast = head.Next;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            return slow;
        }

        private static void Measure(MyLinkedList data)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            data.First = Sort(data.First);
            timer.Stop();
            Console.WriteLine("Time : " + timer.ElapsedMilliseconds);
            Console.WriteLine("Iterations : " + iterationsCount);
            Console.WriteLine();
            iterationsCount = 0;
            timer.Reset();
        }
    }
}