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
            var res = sequence.ExtensionMethod(x=>x%2==0,(x,y)=>Tuple.Create(x,y));
            foreach (var item in res) Console.WriteLine(item);
        }
    }
}
