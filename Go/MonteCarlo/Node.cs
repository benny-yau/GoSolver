using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    [Serializable]
    public class Node
    {
        private State state;
        private Node parent;
        private List<Node> childArray;
        private bool expanded = false;
        private bool noPossibleStates = false;
        [NonSerialized]
        private JArray prunedJson;

        public Node()
        {
            this.State = new State();
        }

        public Node(State state)
        {
            this.State = state;
        }

        public Node(Node node)
        {
            Game g = new Game(node.State.Game);
            this.State = new State(g);
            this.State.ConfirmAlive = node.State.ConfirmAlive;
            this.State.WinOrLose = node.State.WinOrLose;
            this.State.SurviveOrKill = node.State.SurviveOrKill;

            this.expanded = node.expanded;
            this.noPossibleStates = node.noPossibleStates;
            if (node.prunedJson != null)
                this.prunedJson = new JArray(node.prunedJson);
        }

        public State State
        {
            get
            {
                return state;
            }
            set
            {
                this.state = value;
            }
        }


        public Node Parent
        {
            get
            {
                return parent;
            }
            set
            {
                this.parent = value;
            }
        }


        public List<Node> ChildArray
        {
            get
            {
                if (childArray == null)
                    childArray = new List<Node>();
                return childArray;
            }
            set
            {
                this.childArray = value;
            }
        }

        public int CurrentDepth
        {
            get
            {
                return this.State.Game.Board.LastMoves.Count;
            }
        }

        public bool Expanded
        {
            get
            {
                return expanded;
            }
            set
            {
                this.expanded = value;
            }
        }

        public bool NoPossibleStates
        {
            get
            {
                return noPossibleStates;
            }
            set
            {
                this.noPossibleStates = value;
            }
        }

        public JArray PrunedJson
        {
            get
            {
                if (prunedJson == null)
                    prunedJson = new JArray();
                return prunedJson;
            }
            set
            {
                prunedJson = value;
            }
        }

        public String GetLastMoves()
        {
            return this.State.Game.Board.GetLastMoves();
        }

        public override String ToString()
        {
            String rc = "Score: " + this.State.WinScore;
            rc += "\tVisitCount: " + this.State.VisitCount;
            rc += "\tWinScore: " + this.State.WinScore;
            rc += "\tUCT:" + UCT.UctValue(this).ToString("0.000");
            rc += "\tEValue:" + (1 - UCT.ExplorationValue(this)).ToString("0.000");
            rc += "\tMove:" + GetLastMoves();
            rc += "\tDepth: " + this.State.Depth;
            return rc;
        }

    }
}
