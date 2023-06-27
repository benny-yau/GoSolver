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
        public static Boolean IsFirstRun;
        public static List<Node> RemovedNodes;
        public static List<Node> PrunedNodes;

        public FirstRunMCTS(Node rootNode, int mctsDepth = 0) : base(rootNode, mctsDepth)
        {
            if (rootNode.CurrentDepth == 0)
            {
                IsFirstRun = true;
                RemovedNodes = new List<Node>();
                PrunedNodes = new List<Node>();
            }
        }

        public override Node AnswerNode
        {
            get
            {
                return base.answerNode;
            }
            set
            {
                if (VerifyAnswerOnFirstRun(value))
                    base.answerNode = value;
            }
        }

        protected override void ExpandNode(Node node)
        {
            RemoveHalfOfNodes(node);
        }

        protected override void Pruning(Node prunedNode, Node verifyNode)
        {
            base.Pruning(prunedNode, verifyNode);
            PrunedNodes.Add(prunedNode);
        }

        protected void RestoreNodes()
        {
            RemovedNodes.ForEach(n => n.Parent.ChildArray.Add(n));
            RemovedNodes.Clear();
            PrunedNodes.ForEach(n => n.Parent.ChildArray.Add(n));
            PrunedNodes.ForEach(n => n.Parent.PrunedJson = null);
            PrunedNodes.Clear();
        }

        protected Boolean VerifyAnswerOnFirstRun(Node node)
        {
            if (IsFirstRun && this.tree.Root.CurrentDepth == 0)
            {
                IsFirstRun = false;
                if (RemovedNodes.Count > 0)
                {
                    RestoreNodes();
                    DebugHelper.DebugWriteWithTab("First run completed" + Environment.NewLine + "Verifying answer...");
                    MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(node.State.Game, node);
                    if (mcts.AnswerNode != null)
                    {
                        //first run not valid
                        Pruning(node, mcts.AnswerNode);
                        return false;
                    }
                }
                DebugHelper.DebugWriteWithTab("Answer move: " + node.State.Game.Board.Move);
            }
            return true;
        }

        protected void RemoveHalfOfNodes(Node node)
        {
            if (IsFirstRun && node.ChildArray.Count >= 6)
            {
                node.ChildArray = node.ChildArray.OrderByDescending(n => n.State.WinScore).ToList();
                int halfCount = Convert.ToInt32(Math.Ceiling(node.ChildArray.Count * 0.5));
                for (int i = node.ChildArray.Count - 1; i > halfCount - 1; i--)
                {
                    Node removedNode = node.ChildArray[i];
                    node.ChildArray.Remove(removedNode);
                    RemovedNodes.Add(removedNode);
                }
            }
        }

        protected override void PostProcess(Stopwatch watch)
        {
            if (IsFirstRun && tree.Root.CurrentDepth == 0 && tree.Root.ChildArray.Count == 0)
            {
                //no answer found on first run, rerun mcts
                IsFirstRun = false;
                RestoreNodes();
                DebugHelper.DebugWriteWithTab("First run completed" + Environment.NewLine + "No answer found");
                MonteCarloTreeSearch mcts = MonteCarloGame.InitializeMonteCarloComputerMove(tree.Root.State.Game, tree.Root);
                if (mcts.AnswerNode != null)
                {
                    this.AnswerNode = mcts.AnswerNode;
                    DebugHelper.DebugWriteWithTab("Answer move: " + AnswerNode.State.Game.Board.Move);
                }
            }
            base.PostProcess(watch);
        }
    }
}
