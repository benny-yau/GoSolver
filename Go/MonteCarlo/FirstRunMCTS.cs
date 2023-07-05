using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class FirstRunMCTS : MonteCarloTreeSearch
    {
        const int simulationCount = 3000;

        public FirstRunMCTS(Node rootNode, int mctsDepth = 0) : base(rootNode, mctsDepth)
        {
        }

        public static Boolean VerifyOnCondition(Node firstNode)
        {
            return firstNode.State.VisitCount > simulationCount;
        }

        protected override MonteCarloTreeSearch InitializeMCTSWithSiblingNode(Node siblingNode)
        {
            FirstRunMCTS mcts = new FirstRunMCTS(siblingNode, mctsDepth + 1);
            return mcts;
        }

        protected override void ExpandNode(Node node)
        {
            NodeExpansion(node);
            node.ChildArray = GetHalfOfNodes(node).ToList();
        }

        /// <summary>
        /// Verify first node. Set depth to verify to higher value for difficult scenarios.
        /// </summary>
        public static (Boolean, Node) VerifyFirstNode(Node firstNode)
        {
            Node n = new Node(firstNode);
            FirstRunMCTS.DeepCopyHalfOfNodes(firstNode, n);

            //start mcts
            Debug.WriteLine("Verifying node " + firstNode.State.Game.Board.Move + " on first run...");
            FirstRunMCTS mcts = new FirstRunMCTS(n);
            mcts.FindNextMove();
            if (mcts.AnswerNode != null)
                return (false, mcts.AnswerNode);
            else
                return (true, null);
        }

        /// <summary>
        /// Deep copy half of nodes.
        /// </summary>
        public static void DeepCopyHalfOfNodes(Node rootNode, Node newNode)
        {
            foreach (Node node in GetHalfOfNodes(rootNode))
            {
                Node n = new Node(node);
                newNode.ChildArray.Add(n);
                n.Parent = newNode;
                DeepCopyHalfOfNodes(node, n);
            }
        }

        /// <summary>
        /// Get half of nodes.
        /// </summary>
        public static IEnumerable<Node> GetHalfOfNodes(Node rootNode)
        {
            rootNode.ChildArray = rootNode.ChildArray.OrderByDescending(n => UCT.uctValue(n)).ToList();
            int halfCount = Convert.ToInt32(Math.Ceiling(rootNode.ChildArray.Count * 0.5));
            for (int i = 0; i <= rootNode.ChildArray.Count - 1; i++)
            {
                if (rootNode.CurrentDepth <= 3)
                {
                    //set minimum count
                    if (halfCount >= 3 && i > halfCount) break;
                    //set maximum count
                    if (i > 5) break;
                }
                yield return rootNode.ChildArray[i];
            }
        }

    }
}
