using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class Node
    {
        private State state;
        private Node parent;
        private List<Node> childArray;
        private bool expanded = false;
        private JArray prunedJson;

        public Node()
        {
            this.State = new State();
            this.State.Node = this;
        }

        public Node(State state)
        {
            this.State = state;
            this.State.Node = this;
        }

        public virtual State State
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


        public virtual Node Parent
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


        public virtual List<Node> ChildArray
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

        public virtual int CurrentDepth
        {
            get
            {
                return this.State.Game.Board.LastMoves.Count;
            }
        }

        public virtual bool Expanded
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
            rc += " VisitCount: " + this.State.VisitCount;
            rc += " UCT:" + UCT.uctValue(this);
            rc += " Move:" + GetLastMoves();
            rc += " Stats (N: " + this.State.Stats["N"] + ", W: " + this.State.Stats["W"] + ", Q: " + this.State.Stats["Q"] + ", P: " + this.State.Stats["P"] + ")";
            rc += " Depth:" + this.State.Depth;
            return rc;
        }

    }
}
