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

    }
}
