using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Go
{
    public class Tree
    {
        private Node root;
        public Boolean HitDepthToVerify = false;

        public Tree()
        {
            root = new Node();
        }

        /// <summary>
        /// Starting node of current mcts.
        /// </summary>
        public virtual Node Root
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
        /// Starting node of initial mcts.
        /// </summary>
        public virtual Node AbsoluteRoot
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
