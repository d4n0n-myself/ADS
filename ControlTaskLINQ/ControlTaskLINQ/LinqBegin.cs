using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlTaskLINQ
{
    public static class LinqBegin
    {
        public static void LinqBeginMethod()
        {
            IEnumerable<int> a = new int[] { 10101, 101101, 11 };
            IEnumerable<int> b = new int[] { 1111, 11011, 101 };

            var result1 = a.Select(x => x.ToString())
                           .GroupJoin(b.Select(x => x.ToString()),
                                      FindEvenCount,
                                      FindEvenCount,
                                      (x, y) => y.DefaultIfEmpty("0")
                                      .Select(z => x + ':' + z))
                           .SelectMany(x => x)
                           .Select(x => x.Split(':'))
                           .OrderBy(x => int.Parse(x[0]))
                           .ThenByDescending(x => int.Parse(x[1]))
                           .Select(x => string.Format("{0}:{1}", x[0], x[1]));

            foreach (var e in result1)
                Console.WriteLine(e);
        }

        public static void LinqBeginQuery()
        {
            IEnumerable<int> aSequence = new int[] { 10101, 101101, 11 };
            IEnumerable<int> bSequence = new int[] { 1111, 11011, 101 };

            var newAS = from a in aSequence
                        select a.ToString();
            var newBS = from b in bSequence
                        select b.ToString();
            
            var result1 =
                from a in newAS
                join b in newBS
                on FindEvenCount(a) equals FindEvenCount(b)
                select a + ":" + b
                into newSeq
                select newSeq.Split(':')
                             into newSS
                             orderby int.Parse(newSS[0]), int.Parse(newSS[1]) descending
                             select string.Join(":",newSS);

            foreach (var e in result1)
                Console.WriteLine(e);
        }

        private static int FindEvenCount(string number)
        {
            return number.Count(x => Convert.ToInt32(x) % 2 == 1);
        }
    }
}