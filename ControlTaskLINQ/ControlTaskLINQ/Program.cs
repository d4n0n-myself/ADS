using System;
using System.Collections.Generic;

namespace ControlTaskLINQ
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Method");
            LinqBegin.LinqBeginMethod();
            Console.WriteLine("---------");
            Console.WriteLine("Query");
            LinqBegin.LinqBeginQuery();
            Console.WriteLine("---------");
            Console.WriteLine("Extension");
            IEnumerable<int> sequence = new int[] { 1, 2, 3, 4 };
            sequence.ExtensionMethod(x => x % 2 == 0, x => x);
            foreach (var item in sequence) Console.WriteLine(item);
        }
    }
}
