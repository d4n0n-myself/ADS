using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlTaskLINQ
{
    public static class Extension
    {
        public static void ExtensionMethod<T>(this IEnumerable<T> input, Func<T, bool> predicate, Func<T, T> resultSelector)
        {
            var news = input.Where(x => predicate(x));
            input = news;
        }
    }
}
