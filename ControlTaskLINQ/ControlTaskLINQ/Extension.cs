using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlTaskLINQ
{
    public static class Extension
    {
        public static IEnumerable<TResult> ExtensionMethod<TSource, TResult, TKey>
            (this IEnumerable<TSource> input, Func<TSource,TKey> predicate, Func<TSource,TSource, TResult> resultSelector) // 2
        {
            return
                from x in input
                join y in input
                on predicate(x) equals predicate(y)
                select resultSelector(x, y);
        }

        public static IEnumerable<T> DistinctBy<T,TKey>(this IEnumerable<T> seq, Func<T,TKey> selector) // 3
        {
            return
                seq.GroupBy(selector)
                   .Select(x => x.First());
                   //.SelectMany(x => x);
        }

        public static Tuple<IEnumerable<T>, IEnumerable<T>> Partition<T>(this IEnumerable<T> input, Func<T,bool> predicate) // 4
        {
            return Tuple.Create(input.Where(predicate), input.Where(x => !predicate(x)));
        }

        public static IEnumerable<TResult> Zip3<T1, T2, T3, TResult>(this IEnumerable<T1> seq1, IEnumerable<T2> seq2, IEnumerable<T3> seq3, Func<T1,T2,T3,TResult> selector) // 5
        {
            using (var it1=seq1.GetEnumerator())
            using (var it2=seq2.GetEnumerator())
            using (var it3=seq3.GetEnumerator())
            {
                while (it1.MoveNext() && it2.MoveNext() && it3.MoveNext()) 
                    yield return selector(it1.Current, it2.Current, it3.Current);
            }
        }
    }
}
