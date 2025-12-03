using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Go
{
    [Serializable]
    public struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public Boolean notEmpty { get; set; }

        [DebuggerStepThroughAttribute()]
        public Point(int xx, int yy) : this()
        {
            x = xx;
            y = yy;
            notEmpty = true;
        }

        [DebuggerStepThroughAttribute()]
        public Point LeftPoint()
        {
            return new Point(x - 1, y);
        }

        [DebuggerStepThroughAttribute()]
        public Point RightPoint()
        {
            return new Point(x + 1, y);
        }

        [DebuggerStepThroughAttribute()]
        public Point UpPoint()
        {
            return new Point(x, y - 1);
        }

        [DebuggerStepThroughAttribute()]
        public Point DownPoint()
        {
            return new Point(x, y + 1);
        }

        public Boolean IsEmpty()
        {
            return !notEmpty;
        }

        public override string ToString()
        {
            return "(" + x + "," + y + ")";
        }
    }
}
