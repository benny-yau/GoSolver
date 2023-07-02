using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go
{
    public class NoExhaustiveSearchMCTS : MonteCarloTreeSearch
    {
        const int simulationCount = 300;
        static Boolean useFirstRunStrategy = true;

        public NoExhaustiveSearchMCTS(Node rootNode, int mctsDepth = 0) : base(rootNode, mctsDepth)
        {
        }

        protected override Node SelectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
            Boolean hitSimulationCount = rootNode.ChildArray.Any(n => n.State.VisitCount > simulationCount);
            while (node.ChildArray.Count != 0)
            {
                if (!hitSimulationCount)
                    node = SelectMoveByProbabilityDistribution(node);
                else
                    node = UCT.findBestNodeWithUCT(node);
                if (node.CurrentDepth == 1)
                    hitSimulationCount = (node.State.VisitCount > simulationCount);
            }
            return node;
        }

        private Node SelectMoveByProbabilityDistribution(Node node)
        {
            double sum = node.ChildArray.Sum(s => s.State.HeatValue);
            double r = random.NextDouble() * sum;
            Node n = node.ChildArray.First();
            for (int i = 0; i <= node.ChildArray.Count - 1; i++)
            {
                n = node.ChildArray[i];
                r -= n.State.HeatValue;
                if (r <= 0)
                    break;
            }
            return n;
        }

        protected override void VerifyOnDepthReached(Node promisingNode)
        {
            //formulate strategy here
            if (useFirstRunStrategy)
                FirstRunStrategy(promisingNode);
        }

        private void FirstRunStrategy(Node promisingNode)
        {
            Node firstNode = promisingNode.FirstNode();
            if (FirstRunMCTS.VerifyOnCondition(firstNode))
            {
                (Boolean verify, Node answerNode) = FirstRunMCTS.VerifyFirstNode(firstNode);
                if (verify)
                {
                    DebugHelper.DebugWriteWithTab("Answer on first run: " + firstNode.State.Game.Board.Move);
                    this.AnswerNode = firstNode;
                }
                else
                    Pruning(firstNode, answerNode);
            }
        }

    }
}
