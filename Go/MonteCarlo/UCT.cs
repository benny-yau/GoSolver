using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Go
{
    class UCT
    {
        public static Node node = null;
        public static double uctValue(Node node)
        {
            if (node.State.VisitCount == 0)
            {
                return int.MaxValue;
            }
            int totalVisitCount = 0;
            node.Parent.ChildArray.ForEach(m => totalVisitCount += m.State.VisitCount);

            return (node.State.WinScore / (double)node.State.VisitCount) + 1.41 * Math.Sqrt(Math.Log(totalVisitCount) / (double)node.State.VisitCount);// 0.44 or 1.41
        }

        /// <summary>
        /// Find best child node using uct value.
        /// </summary>
        internal static Node findBestNodeWithUCT(Node node)
        {
            Node bestNode = node.ChildArray.MaxObject(m => uctValue(m));
            return bestNode;

        }
    }
}
