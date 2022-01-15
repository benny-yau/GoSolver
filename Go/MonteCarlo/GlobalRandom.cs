using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    internal static class GlobalRandom
    {
        private static System.Random randomInstance = null;

        public static double NextDouble
        {
            get
            {
                if (randomInstance == null)
                    randomInstance = new System.Random();

                return randomInstance.NextDouble();
            }
        }

        public static int NextRange(int start, int end)
        {
            if (randomInstance == null)
                randomInstance = new System.Random();

            return randomInstance.Next(start, end);
        }
    }
}
