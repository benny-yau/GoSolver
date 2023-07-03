using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public static class FirstRunMCTS
    {
        const int simulationCount = 3000;

        public static Boolean VerifyOnCondition(Node firstNode)
        {
            return firstNode.State.VisitCount > simulationCount;
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
            MonteCarloTreeSearch mcts = new MonteCarloTreeSearch(n);
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
            rootNode.ChildArray = rootNode.ChildArray.OrderByDescending(n => UCT.uctValue(n)).ToList();
            int halfCount = Convert.ToInt32(Math.Ceiling(rootNode.ChildArray.Count * 0.5));
            for (int i = 0; i <= halfCount - 1; i++)
            {
                Node childNode = rootNode.ChildArray[i];
                Node n = new Node(childNode);
                newNode.ChildArray.Add(n);
                n.Parent = newNode;
                DeepCopyHalfOfNodes(childNode, n);
                if (newNode.ChildArray.Count >= 5) break;
            }
        }

    }
}
