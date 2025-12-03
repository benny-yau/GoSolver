using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public static class Extensions
    {
        [DebuggerStepThroughAttribute()]
        public static Content Opposite(this Content c)
        {
            if (c == Content.Empty || c == Content.Unknown) 
                throw new Exception();
            return c == Content.Black ? Content.White : Content.Black;
        }

        [DebuggerStepThroughAttribute()]
        public static SurviveOrKill Opposite(this SurviveOrKill s)
        {
            return s == SurviveOrKill.Kill ? SurviveOrKill.Survive : SurviveOrKill.Kill;
        }

        [DebuggerStepThroughAttribute()]
        public static KoCheck Opposite(this KoCheck s)
        {
            return s == KoCheck.Kill ? KoCheck.Survive : KoCheck.Kill;
        }

        [DebuggerStepThroughAttribute()]
        public static PlayerOrComputer Opposite(this PlayerOrComputer s)
        {
            return s == PlayerOrComputer.Player ? PlayerOrComputer.Computer : PlayerOrComputer.Player;
        }

        [DebuggerStepThroughAttribute()]
        public static String GetConcatenatedString<T>(this IEnumerable<T> source)
        {
            String msg = "";
            foreach (var item in source)
            {
                if (msg != "") msg += ",";
                msg += item.ToString();
            }
            return msg;
        }

        [DebuggerStepThroughAttribute()]
        public static String GetDebugString<T>(this IEnumerable<T> source)
        {
            String msg = "";
            foreach (var item in source)
                msg += item.ToString() + "\n";
            return msg;
        }

        [DebuggerStepThroughAttribute()]
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

        [DebuggerStepThroughAttribute()]
        public static bool All<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool mustExist)
        {
            foreach (var e in source)
            {
                if (!predicate(e))
                    return false;
                mustExist = false;
            }
            return !mustExist;
        }
    }

}
