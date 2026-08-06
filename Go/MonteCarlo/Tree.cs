using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Go
{
    public class Tree
    {
        private Node root;

        public Tree()
        {
            root = new Node();
        }

        /// <summary>
        /// Root node of current mcts.
        /// </summary>
        public Node Root
        {
            get
            {
                return root;
            }
            set
            {
                this.root = value;
            }
        }

        /// <summary>
        /// Root node of initial mcts.
        /// </summary>
        public Node AbsoluteRoot
        {
            get
            {
                Node r = root;
                while (r.Parent != null)
                    r = r.Parent;
                return r;
            }
        }

        /// <summary>
        /// Top node of tree.
        /// </summary>
        public Boolean TopNodeOfTree(Node node)
        {
            return node.CurrentDepth == this.Root.CurrentDepth + 1;
        }

        /// <summary>
        /// Game info.
        /// </summary>
        public GameInfo GameInfo
        {
            get
            {
                return Root.State.Game.GameInfo;
            }
        }
    }
}
