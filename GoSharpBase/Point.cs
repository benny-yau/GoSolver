using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Go
{
    /// <summary>
    /// Represents a pair of board coordinates - x and y.
    /// </summary>
    [Serializable]
    public struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public int NotEmpty { get; set; }

        [DebuggerStepThroughAttribute()]
        public Point(int xx, int yy) : this()
        {
            x = xx;
            y = yy;
            NotEmpty = 1;
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

        public override string ToString()
        {
            return "(" + x + "," + y + ")";
        }
    }

    public class LinkedPoint<T>
    {
        public T Move { get; set; }
        public object CheckMove { get; set; }

        public LinkedPoint(T move, object checkMove)
        {
            this.Move = move;
            this.CheckMove = checkMove;
        }

        public Boolean EqualLink(LinkedPoint<T> linkedPoint)
        {
            if (linkedPoint.Move.Equals((T)CheckMove) && ((T)linkedPoint.CheckMove).Equals(Move))
                return true;
            return false;
        }

        public override string ToString()
        {
            return "Move: " + Move.ToString() + " CheckMove: " + CheckMove;
        }
    }
}
