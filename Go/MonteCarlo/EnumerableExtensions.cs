using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    static class EnumerableExtensions
    {
        public static String GetDebugString<T>(this IEnumerable<T> source)
        {
            String debugString = "";
            foreach (var item in source)
            {
                debugString += item.ToString() + "\n";
            }
            return debugString;
        }


        public static T MaxObject<T, U>(this IEnumerable<T> source, Func<T, U> selector)
            where U : IComparable<U>
        {
            if (source == null)
                return default(T);
            bool first = true;
            T maxObj = default(T);
            U maxKey = default(U);
            foreach (var item in source)
            {
                if (first)
                {
                    maxObj = item;
                    maxKey = selector(maxObj);
                    first = false;
                }
                else
                {
                    U currentKey = selector(item);
                    if (currentKey.CompareTo(maxKey) > 0)
                    {
                        maxKey = currentKey;
                        maxObj = item;
                    }
                }
            }
            return maxObj;
        }
    }
}
