using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class NoExhaustiveSearchMCTS : MonteCarloTreeSearch
    {
        public NoExhaustiveSearchMCTS(Node rootNode, int mctsDepth = 0) : base(rootNode, mctsDepth)
        {
        }

        protected override void VerifyOnDepthReached(Node promisingNode)
        {
            return;
        }

        protected override Node SelectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
            while (node.ChildArray.Count != 0)
            {
                if (node.Parent != null)
                {
                    Node n = node.Parent.ChildArray.OrderBy(s => s.State.VisitCount).First();
                    if (n.State.VisitCount <= (50 * (2 - n.CurrentDepth))) return n;
                }
                node = UCT.findBestNodeWithUCT(node);
            }
            return node;
        }

    }
}
