using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Go
{
    public class UCT
    {
        /// <summary>
        /// UCT value.
        /// </summary>
        public static double UctValue(Node node)
        {
            if (node.State.VisitCount == 0)
                return int.MaxValue;
            int totalVisitCount = node.Parent.ChildArray.Sum(n => n.State.VisitCount);

            return (node.State.WinScore / (double)node.State.VisitCount) + 1.1 * Math.Sqrt(Math.Log(totalVisitCount) / (double)node.State.VisitCount);
        }

        /// <summary>
        /// Find best child node using uct value.
        /// </summary>
        public static Node FindBestNodeWithUCT(Node node)
        {
            return node.ChildArray.MaxObject(m => UctValue(m));

        }
    }
}
