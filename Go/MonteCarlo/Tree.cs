using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                Node absoluteRoot = root;
                while (absoluteRoot.Parent != null)
                {
                    absoluteRoot = absoluteRoot.Parent;
                }
                return absoluteRoot;
            }
        }

    }
}
